using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccuracyMiniGameUI : UIBase
{
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

    //따로 만들어놓은 정확도 비율 스크립트
    private AccuracyConfig accData;

    private float maxDuration = 2f; //커서 왕복 시간

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

    //정확도 미니게임 결과 넘겨주는 이벤트
    public Action<float> OnAccuracyMinigameRatio;

    //정확도 UI에 표시로직 끝났는지 확인하는 이벤트
    public Action OnResultUIFinished;

    public override void OnOpened(object[] param)
    {
        Debug.Log("정확도 OnOpened");
        accData = (AccuracyConfig)param[0];
        StartUI();
        StartAccuaryMiniGame();
        timer = 0;
    }

    void StartUI()
    {
        Debug.Log("정확도 미니게임 OnEnable");
        rt = gauge.GetComponent<RectTransform>();
        gaugeWidth = rt.sizeDelta.x;
        float gaugeCenter = rt.anchoredPosition.x;

        gaugeLeftEdge = gaugeCenter - (gaugeWidth / 2);
        gaugeRightEdge = gaugeCenter + (gaugeWidth / 2);
        arrowWidth = arrow.sizeDelta.x;
        arrowRange = (gaugeRightEdge - gaugeLeftEdge) - arrowWidth;
    }
    public void StartAccuaryMiniGame()
    {
        Debug.Log("정확도 미니게임 시작");
        if (!isInitAccuaracy)
        {
            InitAccuracyMiniGame();
            isInitAccuaracy = true;
            isMove = true;
        }
    }

    private void Update() //기존 StartAccuaryMiniGame의 움직임 로직 분리
    {
        if (!isMove) return;

        timer += Time.deltaTime;
        float halfDuration = maxDuration / 2;

        //화살표 이동 로직 (시간 기반으로 한번만 왕복)
        float durationRatio = Mathf.PingPong(timer, halfDuration) / halfDuration;
        float posX = Mathf.Lerp(gaugeLeftEdge + (arrowWidth / 2), gaugeLeftEdge + arrowRange + (arrowWidth / 2), durationRatio);             
        arrow.anchoredPosition = new Vector2(posX, arrow.anchoredPosition.y);

        //시간초과 자동 실패
        if(timer >= maxDuration)
        {
            isMove = false;
            Debug.Log("시간 초과 실패");
            //시간초과 실패 -> ratio = 0
            OnAccuracyMinigameRatio?.Invoke(0);
        }

        //클릭시 정확도 판정
        if (Input.GetMouseButtonDown(0))
        {
            isMove = false;
            float arrowX = arrow.anchoredPosition.x;
            float ratio = (arrowX - gaugeLeftEdge) / gaugeWidth;

            //정확도 비율만 이벤트로 뿌려줌(정확도 판단 X)
            OnAccuracyMinigameRatio?.Invoke(ratio);
        }   
    }
    void InitAccuracyMiniGame() //정확도 미니게임 화살표 초기화 및 타이머변수 초기화 로직
    {
        Debug.Log("정확도 미니게임 초기화");        
        // 초기화
        timer = 0;
        SetZoneUISize(perfectZone, accData.perfectStartRatio, accData.perfectEndRatio);
        SetZoneUISize(veryGoodZone, accData.veryGoodStartRatio, accData.veryGoodEndRatio);
        SetZoneUISize(goodZone, accData.goodStartRatio, accData.goodEndRatio);
        SetZoneUISize(badZone, accData.badStartRatio, accData.badEndRatio);
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

    public void StartResultLogic(AccuracyResult result)
    {
        StartCoroutine(ShowResultAndClose(result));
    }
    private IEnumerator ShowResultAndClose(AccuracyResult result) //UI텍스트 표시딜레이용 코루틴
    {
        AccuaracyResultText.text = result.ToString();
        yield return new WaitForSeconds(0.5f);        
        isInitAccuaracy = false;
        OnResultUIFinished?.Invoke();        
    }

}
