using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccuracyMiniGameUI : UIBase
{
    private Action<AccuracyResult> onComplete;//콜백 저장용 변수

    [Header("정확도 관련")]
    [SerializeField]
    private RectTransform arrow;
    [SerializeField]
    private GameObject gauge;

    private float arrowSpeed = 100.0f;
    private float arrowRange = 0;
    private float arrowWidth = 0;
    RectTransform rt;
    private float gaugeLeftEdge = 0;
    private float gaugeRightEdge = 0;
    private bool isMove = false;
    private float gaugeWidth = 0;
    float timer = 0f;
    private bool isInitAccuaracy = false;

    [Header("정확도 구간")]
    private float perfectStartRatio;
    private float perfectEndRatio;

    private float goodStartRatio;
    private float goodEndRatio;

    private float veryGoodStartRatio;
    private float veryGoodEndRatio;

    private float badStartRatio = 0;
    private float badEndRatio;

    private float perfectRatio = 0.1f;
    private float veryGoodRatio = 0.3f;
    private float goodRatio = 0.5f;
    private float badRatio = 0.1f;

    [SerializeField]
    private RectTransform perfectZone;
    [SerializeField]
    private RectTransform veryGoodZone;
    [SerializeField]
    private RectTransform goodZone;
    [SerializeField]
    private RectTransform badZone;
    [SerializeField]
    private TextMeshProUGUI AccuaracyResultText;
    public override void OnOpened(object[] param)
    {

    }
    void Awake()
    {
        rt = gauge.GetComponent<RectTransform>();
        gaugeWidth = rt.sizeDelta.x;
        float gaugeCenter = rt.anchoredPosition.x;

        gaugeLeftEdge = gaugeCenter - (gaugeWidth / 2);
        gaugeRightEdge = gaugeCenter + (gaugeWidth / 2);
        arrowWidth = arrow.sizeDelta.x;
        arrowRange = (gaugeRightEdge - gaugeLeftEdge) - arrowWidth;
        Debug.Log("정확도 미니게임 Awake");
        timer = 0;
        SetAccuaryRatio();
    }
    /*public void StartAccuaryMiniGame() // 정확도 미니게임 로직 및 기능추가
    {
        Debug.Log("정확도 미니게임 로직");
        if (!isInitAccuaracy)
        {
            InitAccuracyMiniGame();
            isInitAccuaracy = true;
        }


        timer += Time.deltaTime;
        isMove = true;
        float arrowX = 0;

        if (Input.GetMouseButtonDown(0))
        {
            isMove = false;
            arrowX = arrow.anchoredPosition.x;
            float ratio = (arrowX - gaugeLeftEdge) / gaugeWidth;
            Debug.Log($"X : {ratio}");
            CheckAccuracy(ratio);
            //State = PitchState.Throwing;
        }

        //화살표 이동로직
        if (isMove)
        {
            float posX = Mathf.PingPong(timer * arrowSpeed, arrowRange) + gaugeLeftEdge + (arrowWidth / 2);
            arrow.anchoredPosition = new Vector2(posX, arrow.anchoredPosition.y);
        }

    }*/

    
    public void StartAccuaryMiniGame(Action<AccuracyResult> callback)//정확도 미니게임 콜백함수로 변경
    {
        onComplete = callback;
        Debug.Log("정확도 미니게임 시작");
        if (!isInitAccuaracy)
        {
            InitAccuracyMiniGame();
            isInitAccuaracy = true;
            isMove = true;
        }
        //커서 안멈추는거 아마 여기일듯
        
        //여기임
    }

    private void Update() //기존 StartAccuaryMiniGame의 움직임 로직 분리
    {
        if (!isMove)
        {
            Debug.Log("Update()에서 isMove == false 상태");
            return;
        }

        timer += Time.deltaTime;

        //화살표 이동 로직
        float posX = Mathf.PingPong(timer * arrowSpeed, arrowRange) + gaugeLeftEdge + (arrowWidth / 2);
        arrow.anchoredPosition = new Vector2(posX, arrow.anchoredPosition.y);


        if (Input.GetMouseButtonDown(0))
        {
            isMove = false;
            Debug.Log("isMove = false 설정됨");
            float arrowX = arrow.anchoredPosition.x;
            //arrow.anchoredPosition = new Vector2(arrowX, arrow.anchoredPosition.y);
            float ratio = (arrowX - gaugeLeftEdge) / gaugeWidth;

            var result = CheckAccuracy(ratio); //정확도 판단
            StartCoroutine(ShowResultAndClose(result));
           
        }   
    }
    void InitAccuracyMiniGame() //정확도 미니게임 화살표 초기화 및 타이머변수 초기화 로직
    {
        Debug.Log("정확도 미니게임 초기화");        
        // 초기화
        timer = 0;
        SetZoneUISize(perfectZone, perfectStartRatio, perfectEndRatio);
        SetZoneUISize(veryGoodZone, veryGoodStartRatio, veryGoodEndRatio);
        SetZoneUISize(goodZone, goodStartRatio, goodEndRatio);
        SetZoneUISize(badZone, badStartRatio, badEndRatio);
        float posX = gaugeLeftEdge + (arrowWidth / 2);
        arrow.anchoredPosition = new Vector2(posX, arrow.anchoredPosition.y);
        AccuaracyResultText.text = "";
    }
    void SetZoneUISize(RectTransform zoneRT, float startRatio, float endRatio) //정확도 구간 UI크기 조정
    {
        Debug.Log($"{zoneRT}정확도 UI사이즈 조정");
        float zoneWidth = gaugeWidth * (endRatio - startRatio);
        float center = (startRatio + endRatio) / 2;
        float centerX = gaugeWidth * center - (gaugeWidth / 2);
        zoneRT.sizeDelta = new Vector2(zoneWidth, zoneRT.sizeDelta.y);
        zoneRT.anchoredPosition = new Vector2(centerX, zoneRT.anchoredPosition.y);
    }
    void SetAccuaryRatio() //정확도 구간 비율에 따른 구간 시작점과 끝점 설정
    {
        badEndRatio = badStartRatio + badRatio;
        goodStartRatio = badEndRatio;
        goodEndRatio = goodStartRatio + goodRatio;
        veryGoodStartRatio = goodEndRatio;
        veryGoodEndRatio = veryGoodStartRatio + veryGoodRatio;
        perfectStartRatio = veryGoodEndRatio;
        perfectEndRatio = perfectStartRatio + perfectRatio;
    }
    AccuracyResult CheckAccuracy(float arrowX)//정확도 미니게임 구간체크 로직
    {
        if (arrowX > perfectStartRatio && arrowX <= perfectEndRatio)
        {
            AccuaracyResultText.text = "Perfect!";
            return AccuracyResult.Perfect;
        }
        else if (arrowX > veryGoodStartRatio && arrowX <= veryGoodEndRatio)
        {
            AccuaracyResultText.text = "Very Good";
            return AccuracyResult.VeryGood;
        }
        else if (arrowX > goodStartRatio && arrowX <= goodEndRatio)
        {
            AccuaracyResultText.text = "Good!";
            return AccuracyResult.Good;
        }
        else if (arrowX > badStartRatio && arrowX <= badEndRatio)
        {
            AccuaracyResultText.text = "Bad!";
            return AccuracyResult.Bad;
        }
        else
        {
            AccuaracyResultText.text = "Miss!";
            return AccuracyResult.Miss;
        }
    }

    private IEnumerator ShowResultAndClose(AccuracyResult result) //UI텍스트 표시딜레이용 코루틴
    {
        yield return new WaitForSeconds(0.5f);

        onComplete?.Invoke(result); //콜백으로 결과 전달
        isInitAccuaracy = false;
        uiMan.Hide<AccuracyMiniGameUI>();//미니게임 창 닫기
    }
}
