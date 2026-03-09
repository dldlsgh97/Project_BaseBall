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

    [Header("타자 정확도 변수")]
    private HitterAccuracyConfig hitterAccData;

    private void Start()
    {
        timingUI = uiMan.Get<HitterTimingGaugeUI>();
        //타자 타이밍 데이터 생성
        hitterAccData = new HitterAccuracyConfig();
    }
    public void HitterLogicStart(float duration)
    {
        //정확도 데이터와 구속변수 넘겨주기
        uiMan.Show<HitterTimingGaugeUI>(new object[] { duration, hitterAccData });
    }

    public void HitterLogicEnd()
    {
        uiMan.Hide<HitterTimingGaugeUI>();
    }

    //타자 정확도 판정
    void SetHitterJudge()
    {

    }
}
