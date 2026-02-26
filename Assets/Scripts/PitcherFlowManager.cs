using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitcherFlowManager : MonoBehaviour
{
    [Header("UIManager")]
    [SerializeField]
    private UIManager uiMan;

    [Header("투수 로직 UI")]
    [SerializeField]
    private BallChoiceUI ballChoiceUI;
    [SerializeField]
    private PitcherPitchZoneUI pitchZoneUI;
    [SerializeField]
    private AccuracyMiniGameUI accUI;

    [Header("투수 공던지기 실행 스크립트")]
    [SerializeField]
    private PitcherExecutor executor;

    [Header("투수 로직 변수")]
    private PitchType pitchtype;
    private Vector3 targetPos;
    private float accuracyResult;


    [Header("정확도 미니게임 변수")]
    private AccuracyConfig accData;
    private AccuracyCalculator calculator;

    [Header("AI투수 로직 스크립트")]
    [SerializeField]
    private AIPitcher aIPitcher;

    private void Start()
    {
        ballChoiceUI = uiMan.Get<BallChoiceUI>();
        pitchZoneUI = uiMan.Get<PitcherPitchZoneUI>();
        accUI = uiMan.Get<AccuracyMiniGameUI>();
    }
    //투구 로직 실행
    public void StartPitchFlow()
    {
        uiMan.Show<BallChoiceUI>();
        ballChoiceUI.OnPitchTypeSelected += SetPitchType;
    }
    //구종선택 데이터 넘겨받기후 다음로직 실행
    void SetPitchType(PitchType type)
    {
        pitchtype = type;
        ballChoiceUI.OnPitchTypeSelected -= SetPitchType;
        uiMan.Hide<BallChoiceUI>();
        //다음로직 실행함수
        StartSelctingTarget();
    }

    void StartSelctingTarget()
    {
        uiMan.Show<PitcherPitchZoneUI>(pitchtype);
        pitchZoneUI.OnPitchZoneSelected += SelectingTarget;
    }
    void SelectingTarget(Vector3 pos)
    {
        targetPos = pos;
        pitchZoneUI.OnPitchZoneSelected -= SelectingTarget;
        uiMan.Hide<PitcherPitchZoneUI>();
        //다음로직 실행함수
        StartAccuracyFlow();
    }
    //정확도 미니게임 시작
    void StartAccuracyFlow()
    {
        accData = new AccuracyConfig();
        calculator = new AccuracyCalculator(accData);
        uiMan.Show<AccuracyMiniGameUI>(accData); 
        accUI.OnAccuracyMinigameRatio += CalculateAccuracy;
        accUI.OnResultUIFinished += EndAccuracyFlow;
    }
    //정확도 계산후 결과표시 
    void CalculateAccuracy(float ratio)
    {
        AccuracyResult result = calculator.Calculate(ratio);
        accuracyResult = calculator.CheckResult(result);
        accUI.StartResultLogic(result);
    }
    //코루틴실행 메서드 호출
    void EndAccuracyFlow()
    {
        accUI.OnAccuracyMinigameRatio -= CalculateAccuracy;
        accUI.OnResultUIFinished -= EndAccuracyFlow;
        uiMan.Hide<AccuracyMiniGameUI>();
        //다음로직 실행함수
        StartPitch();
    }

    //공 던지기 로직
    void StartPitch()
    {
        //투구 로직에서 정한 미니게임 구조체로 병합
        PitchRequest request = new PitchRequest
        {
            PitchType = pitchtype,
            TargetPos = targetPos,
            Accuracy = accuracyResult
        };
        //일반 투구 로직
        executor.OnPitchFinished += EndPitch;
        executor.ExecutePitch(request);
        
    }

    void EndPitch()
    {
        executor.OnPitchFinished -= EndPitch;
        //투구 종료 이후 로직
    }

    public void AIPitch()
    {        
        PitchRequest aiRequest = aIPitcher.SetAIPitcher();
        executor.OnPitchFinished += EndPitch;
        executor.ExecutePitch(aiRequest);
    }
}
