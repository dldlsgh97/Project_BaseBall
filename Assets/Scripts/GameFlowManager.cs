using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    [SerializeField]
    private PitcherFlowManager pitcherFlow;
    [SerializeField]
    private HitterFlowManager hitterFlow;

    private void OnEnable()//이벤트 구독
    {
        pitcherFlow.OnStartHittingTimer += StartHitterTimingLogic;
        pitcherFlow.PitchEnd += EndPitch;
    }
    private void OnDisable()//이벤트 구독 해제
    {
        pitcherFlow.OnStartHittingTimer -= StartHitterTimingLogic;
        pitcherFlow.PitchEnd -= EndPitch;
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
        //타자 UI끄기
        hitterFlow.HitterLogicEnd();
    }
}
