using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    [SerializeField]
    private PitcherFlowManager pitcherFlow;
    [SerializeField]
    private HitterFlowManager hitterFlow;
    [SerializeField]
    private UIManager uiMan;

    [SerializeField]
    private PitchZoneUI pitchZoneUI;

    //탄착점 z값을 위한 Zone 변수
    [SerializeField]
    private Transform zonePos;

    //투수 판단값
    private PitchLocation pitcherAccResult;
    private bool isPitcherResult = false;

    //타자 판단값
    private HitterTimingResult hitterAccResult;
    private bool isHitterResult = false;

    //최종 판단 계산용 스크립트
    private FinalJudge finalJudge;

    //최종 판단후 타격 로직 변수
    private Vector3 pitcherPos;
    [SerializeField]
    private GameObject hitBallPrefab;
    private HitBall hitBall;
    private HitterStatConfig hitterPowerData;
    private void Start()
    {
        pitchZoneUI = uiMan.Get<PitchZoneUI>();
        finalJudge = new FinalJudge();
        //Zone 데이터 가져오기
        Rect pitchRect = pitchZoneUI.GetPitchZoneWorldRect();
        Rect strikeRect = pitchZoneUI.GetStrikeZoneWorldRect();

        //탄착점 z값
        float targetZ = zonePos.position.z;
        //Zone 데이터 넘겨주기
        pitcherFlow.Initialize(pitchRect, strikeRect,targetZ);

        hitBall = hitBallPrefab.GetComponent<HitBall>();
        hitterPowerData = new HitterStatConfig();
    }

    private void OnEnable()//이벤트 구독
    {
        pitcherFlow.OnStartHittingTimer += StartHitterTimingLogic;
        pitcherFlow.PitchEnd += EndPitch;
        //투수, 타자 판정 받아오기 이벤트 구독
        pitcherFlow.OnPitcherJudgeResult += GetPitcherJudge;
        hitterFlow.OnHitterAccuracyResult += GetHitterJudge;
    }
    private void OnDisable()//이벤트 구독 해제
    {
        pitcherFlow.OnStartHittingTimer -= StartHitterTimingLogic;
        pitcherFlow.PitchEnd -= EndPitch;

        pitcherFlow.OnPitcherJudgeResult -= GetPitcherJudge;
        hitterFlow.OnHitterAccuracyResult -= GetHitterJudge;
    }

    #region 테스트용 로직
    public void OnClickPitcherLogicStartBtn() //테스트용 버튼 로직
    {
        pitcherFlow.StartPitchFlow();
    }
    public void OnClickAIPitcherBtn() //자동 투수 테스트 버튼로직
    {
        pitcherFlow.AIPitch();
    }
    #endregion

    //타자 타이밍 UI 시작 트리거
    void StartHitterTimingLogic(float duration)
    {
        hitterFlow.HitterLogicStart(duration);
    }
    void EndPitch()
    {
        //투구 종료 로직
        //judgeManager.JudgeStrikeLogic();
        //타자 UI끄기
        hitterFlow.HitterLogicEnd();
    }

    //투수 판단값 가져오고 최종 판단계산로직으로 넘김
    //타격 최종결과 이후 로직을 위해 투수가 던진공의 탄착지점도 같이 받아옴
    void GetPitcherJudge(PitchLocation result,Vector3 pos)
    {
        pitcherAccResult = result;
        isPitcherResult = true;
        pitcherPos = pos;
        TryFinalJudge();
    }

    //타자 판단값 가져오고 최종 판단계산로직으로 넘김
    void GetHitterJudge(HitterTimingResult result)
    {
        hitterAccResult = result;
        isHitterResult = true;
        TryFinalJudge();
    }

    //최종 판단 계산 스크립트 호출
    void TryFinalJudge()
    {
        if(isHitterResult && isPitcherResult)
        {
            FinalHitResult result = 
            finalJudge.CalculateFinalJudge(pitcherAccResult, hitterAccResult);
            Debug.Log("FinalJudge" + result);
            StartHitBall(result, pitcherPos);
            pitcherFlow.ShowJudgeResult(result);
            hitterFlow.ShowJudgeResult(result);
            StartCoroutine(ResetJudgeCoroutine());
        }

    }
    
    //리셋 메서드
    void ResetJudge()
    {
        pitcherFlow.HideJudgeResult();
        hitterFlow.HideJudgeResult();
        isHitterResult = false;
        isPitcherResult = false;
    }

    //로직 종료 코루틴
    IEnumerator ResetJudgeCoroutine()
    {
        yield return new WaitForSeconds(1f);
        //초기화
        ResetJudge();
    }

    void StartHitBall(FinalHitResult hitResult, Vector3 pos)
    {
        //SetActive로 변경
        hitBall.gameObject.SetActive(true);
        hitBall.Init(hitResult, pos, hitterPowerData);       
    }
}
