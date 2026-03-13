using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [Header("타자 정확도 계산")]
    private HitterJudge hitterJudge;

    //최종 판단 표시 텍스트
    [SerializeField]
    private TextMeshProUGUI finalJudgeText;

    public Action<HitterTimingResult> OnHitterAccuracyResult;
    private void Start()
    {
        timingUI = uiMan.Get<HitterTimingGaugeUI>();
        //타자 타이밍 데이터 생성
        hitterAccData = new HitterAccuracyConfig();
        hitterJudge = new HitterJudge(hitterAccData);
        SetEvent();
    }
    //이벤트 구독
    void SetEvent()
    {
        timingUI.OnTimingFinished += SetHitterJudge;
    }
    
    //이벤트 구독 해제
    private void OnDisable()
    {
        timingUI.OnTimingFinished -= SetHitterJudge;
    }
    public void HitterLogicStart(float duration)
    {
        //정확도 데이터와 구속변수 넘겨주기
        uiMan.Show<HitterTimingGaugeUI>(new object[] { duration, hitterAccData });
    }

    public void HitterLogicEnd()
    {
        //uiMan.Hide<HitterTimingGaugeUI>();       
    }

    //타자 정확도 판정
    void SetHitterJudge(float ratio)
    {
        //타자 타이밍 판단
        var hitterResult = 
        hitterJudge.HitterTimingJudge(ratio);
        OnHitterAccuracyResult.Invoke(hitterResult);
    }

    //최종 판단 UI에 표시
    public void ShowJudgeResult(FinalJudgeResult result)
    {
        finalJudgeText.gameObject.SetActive(true);
        finalJudgeText.text = result.ToString();
    }

    public void HideJudgeResult()
    {
        finalJudgeText.text = "";
        finalJudgeText.gameObject.SetActive(false);
    }
}
