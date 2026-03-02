using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitterFlowManager : MonoBehaviour
{
    [Header("UIManager")]
    [SerializeField]
    private UIManager uiMan;

    [Header("공 스크립트")]
    [SerializeField]
    private Ball ball;

    [Header("타자 로직 UI")]
    [SerializeField]
    private HitterTimingGaugeUI timingUI;


    private void Start()
    {
        timingUI = uiMan.Get<HitterTimingGaugeUI>();
    }
    public void HitterLogicStart(float duration)
    {
        uiMan.Show<HitterTimingGaugeUI>(duration);
    }

    public void HitterLogicEnd()
    {
        uiMan.Hide<HitterTimingGaugeUI>();
    }
}
