using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    [SerializeField]
    private PitcherFlowManager pitcherFlow;


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

}
