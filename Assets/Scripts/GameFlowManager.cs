using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    [SerializeField]
    private PitcherFlowManager pitcherFlow;

    public void OnClickPitcherLogicStartBtn() //테스트용 버튼 로직
    {
        pitcherFlow.StartPitchFlow();
    }
}
