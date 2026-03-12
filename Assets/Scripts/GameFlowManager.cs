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
    private ZoneResult pitcherAccResult;
    private bool isPitcherResult = false;

    //타자 판단값
    private HitterTimingResult hitterAccResult;
    private bool isHitterResult = false;

    //최종 판단 계산용 스크립트
    private FinalJudge finalJudge;
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
    void GetPitcherJudge(ZoneResult result)
    {
        pitcherAccResult = result;
        isPitcherResult = true;
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
            finalJudge.CalculateFinalJudge(pitcherAccResult, hitterAccResult);
        }
    }

    
}
