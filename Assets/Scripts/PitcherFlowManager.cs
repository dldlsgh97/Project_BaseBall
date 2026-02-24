using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitcherFlowManager : MonoBehaviour
{
    [SerializeField]
    private UIManager uiMan;

    [SerializeField]
    private BallChoiceUI ballChoiceUI;
    [SerializeField]
    private PitcherPitchZoneUI pitchZoneUI;

    private PitchType pitchtype;
    private Vector3 targetPos;
    private void Start()
    {
        ballChoiceUI = uiMan.Get<BallChoiceUI>();
        pitchZoneUI = uiMan.Get<PitcherPitchZoneUI>();
    }
    //투구 로직 실행
    void StartPitchFlow()
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
    }

}
