using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitterTimingGaugeUI : UIBase
{
    [SerializeField]
    private RectTransform cursor;
    [SerializeField]
    private GameObject gauge;
    private RectTransform rt;
    private float gaugeWidth = 0;
    private float gaugeLeftEdge = 0;
    private float gaugeRightEdge = 0;
    private float cursorWidth = 0;

    private float ballTimer = 0; //공이 던져져서 탄착지에 도달하는 전체 시간
    private float timingRatio = 0; //타자가 배트를 휘두르는 타이밍/전체시간 비율
    [SerializeField]
    private float cursorSpeed = 0;

    private Vector2 cursorStartPos;
    private Vector2 cursorEndPos;

    private bool isInitHitterTiming = false;

    private Coroutine timerCoroutine;

    //타자 배팅타이밍 결과 넘겨주기용 이벤트
    public Action<float> OnTimingFinished;
    public override void OnOpened(object[] param)
    {
        rt = gauge.GetComponent<RectTransform>();
        gaugeWidth = rt.sizeDelta.x;
        float gaugeCenter = rt.anchoredPosition.x;
        gaugeLeftEdge = gaugeCenter - (gaugeWidth / 2);
        gaugeRightEdge = gaugeCenter + (gaugeWidth / 2);
        cursorWidth = cursor.sizeDelta.x;

        //구속 변수 설정
        ballTimer = (float)param[0];
        StartHittingTimer();
    }

    public void StartHittingTimer()
    {
        if (!isInitHitterTiming)
        {
            InitCursor();
            isInitHitterTiming = true;
        }
        timerCoroutine = StartCoroutine(MoveCursor());
    }
    void InitCursor() //로직 초기화
    {
        // 커서위치 초기화
        float posX = gaugeLeftEdge + (cursorWidth / 2);
        float endPosX = gaugeRightEdge - (cursorWidth / 2);
        cursorStartPos = new Vector2(posX, cursor.anchoredPosition.y);
        cursorEndPos = new Vector2(endPosX, cursor.anchoredPosition.y);
        cursor.anchoredPosition = cursorStartPos;
    }

    IEnumerator MoveCursor() //커서를 움직이는 코루틴
    {
        float elapsed = 0f;
        while (elapsed < ballTimer)
        {
            elapsed += Time.deltaTime;
            float time = elapsed / ballTimer;
            Vector2 pos = Vector2.Lerp(cursorStartPos, cursorEndPos, time);
            cursor.anchoredPosition = pos;
            yield return null;
        }        
    }

    IEnumerator HitterTimingResult()
    {
        isInitHitterTiming = false;
        float cursorX = cursor.anchoredPosition.x;
        timingRatio = (cursorX - gaugeLeftEdge) / gaugeWidth;
        //타자 배팅타이밍 결과 넘겨주기용 이벤트
        OnTimingFinished?.Invoke(timingRatio);
        yield return null;
    }

    //타격버튼 클릭시 코루틴 정지
    public void StopHitterCoroutine()
    {
        StartCoroutine(HitterTimingResult());
        StopCoroutine(timerCoroutine);
    }
}
