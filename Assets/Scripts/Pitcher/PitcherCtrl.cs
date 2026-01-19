using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PitcherCtrl : MonoBehaviour
{
    [SerializeField]
    private GameManager gm;
    [SerializeField]
    private Ball ballScript;
    [SerializeField]
    private Transform ballSpawnPoint;
    [SerializeField]
    private Camera pitcherCam;
    [SerializeField]
    private GameObject targetObj;

    private bool is_throw = true;

    public float PitchSpeed = 10f;
    public Vector3 TargetPosition;
    public bool hasTarget = false;

    //추후 구조변경 필요
    [SerializeField]
    private BallChoiceUI pitchUI;

    public PitchState State;

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
    private float perfectStartRatio = 0.8f;
    private float perfectEndRatio = 1;

    private float goodStartRatio = 0.6f;
    private float goodEndRatio = 0.8f;

    private float badStartRatio = 0.3f;
    private float badEndRatio = 0.6f;

    [SerializeField]
    private RectTransform perfectGauge;
    [SerializeField]
    private RectTransform goodGauge;
    [SerializeField]
    private RectTransform badGauge;
    void Start()
    {
        gm = GameManager.instance;
        rt = gauge.GetComponent<RectTransform>();
        gaugeWidth = rt.sizeDelta.x;
        float gaugeCenter = rt.anchoredPosition.x;

        gaugeLeftEdge = gaugeCenter - (gaugeWidth / 2);
        gaugeRightEdge = gaugeCenter + (gaugeWidth / 2);
        arrowWidth = arrow.sizeDelta.x;
        arrowRange = (gaugeRightEdge - gaugeLeftEdge) - arrowWidth;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 임시 테스트용
        {
            //State = PitchState.SelectingPitchType;
            State = PitchState.SetAccuracy;
            isInitAccuaracy = false;
        }

        switch (State)
        {
            case (PitchState.SelectingPitchType):
                gm.ui.Show<BallChoiceUI>(); //구종선택 UI
                break;
            case (PitchState.SelectingTarget):
                Pitch();
                break;
            case (PitchState.SetAccuracy):
                StartAccuaryMiniGame();
                break;
            case (PitchState.Throwing):
                ThrowBall();
                State = PitchState.Idle;
                break;
        }
    }
    void StartAccuaryMiniGame() // 정확도 미니게임 로직 및 기능추가
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
            State = PitchState.Throwing;
        }

        //화살표 이동로직
        if (isMove)
        {
            float posX = Mathf.PingPong(timer * arrowSpeed, arrowRange) + gaugeLeftEdge + (arrowWidth / 2);
            arrow.anchoredPosition = new Vector2(posX, arrow.anchoredPosition.y);
        }
        
    }
    void InitAccuracyMiniGame() //정확도 미니게임 화살표 초기화 및 타이머변수 초기화 로직
    {
        // 초기화
        timer = 0;
        SetZoneUISize(perfectGauge, perfectStartRatio, perfectEndRatio);
        SetZoneUISize(goodGauge, goodStartRatio, goodEndRatio);
        SetZoneUISize(badGauge, badStartRatio, badEndRatio);
        float posX = gaugeLeftEdge + (arrowWidth / 2);
        arrow.anchoredPosition = new Vector2(posX, arrow.anchoredPosition.y);
    }
    void SetZoneUISize(RectTransform zoneRT, float startRatio, float endRatio)
    {
        float zoneWidth = gaugeWidth * (endRatio - startRatio);
        float center = (startRatio + endRatio) / 2;
        float centerX = gaugeWidth * center - (gaugeWidth / 2);
        zoneRT.sizeDelta = new Vector2(zoneWidth, zoneRT.sizeDelta.y);
        zoneRT.anchoredPosition = new Vector2(centerX, zoneRT.anchoredPosition.y);
    }

    void CheckAccuracy(float arrowX)//정확도 미니게임 구간체크 로직(추후 로직 이동예정)
    {
        if (arrowX > perfectStartRatio && arrowX <= perfectEndRatio)
        {
            Debug.Log("Perfect!");
        }
        else if (arrowX > goodStartRatio && arrowX <= goodEndRatio)
        {
            Debug.Log("Good!");
        }
        else if (arrowX > badStartRatio && arrowX <= badEndRatio)
        {
            Debug.Log("bad!");
        }
        else
        {
            Debug.Log("Miss!");
        }
    }




    void Pitch() // 탄착점 지정함수
    {
        if (is_throw)
        {
            Ray ray = pitcherCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, LayerMask.GetMask("PitchArea")))
            {
                TargetPosition = hit.point;
                hasTarget = true;
                targetObj.transform.position = TargetPosition;
            }
        }
        if (Input.GetMouseButtonDown(0))//커서로 지정후 클릭시 확정
        {
            State = PitchState.SetAccuracy;
        }
    }

    void ThrowBall()//확정된 탅착지로 공 던지기
    {
        is_throw = false;
        ballScript.gameObject.SetActive(true);
        ballScript.ThrowBall(pitchUI.pitchType);
    }
    public void BallToTarget()//도착한 공 비활성화
    {
        ballScript.gameObject.SetActive(false);
        is_throw = true;
    }
}
