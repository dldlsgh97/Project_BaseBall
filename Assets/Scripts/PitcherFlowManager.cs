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

    [Header("투수 로직 변수")]
    private PitchType pitchtype;
    private Vector3 targetPos;

    [Header("정확도 미니게임 변수")]
    private AccuracyConfig accData;
    private AccuracyCalculator calculator;

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

    void StartAccuracyFlow()
    {
        accData = new AccuracyConfig();
        calculator = new AccuracyCalculator(accData);
        uiMan.Show<AccuracyMiniGameUI>(accData); 
        accUI.OnAccuracyMinigameRatio += CalculateAccuracy;
        accUI.OnResultUIFinished += EndAccuracyFlow;
    }
    //정확도 계산후 결과표시 코루틴실행 메서드 호출
    void CalculateAccuracy(float ratio)
    {
        AccuracyResult result = calculator.Calculate(ratio);
        accUI.StartResultLogic(result);
    }
    void EndAccuracyFlow()
    {
        accUI.OnAccuracyMinigameRatio -= CalculateAccuracy;
        accUI.OnResultUIFinished -= EndAccuracyFlow;
        uiMan.Hide<AccuracyMiniGameUI>();
    }
}
