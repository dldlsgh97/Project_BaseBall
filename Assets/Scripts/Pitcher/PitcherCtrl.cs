using System;
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
    [SerializeField]
    private GameObject offsetTargetObj;

    private bool is_throw = true;

    public float PitchSpeed = 10f;
    public Vector3 TargetPosition;
    public bool hasTarget = false;

    //UI 스크립트
    [SerializeField]
    private BallChoiceUI pitchUI;
    [SerializeField]
    private AccuracyMiniGameUI accUI;
    [SerializeField]
    private PitcherPitchZoneUI pitchZoneUI;

    public PitchState State;
    private float accuracyResult = 0;

    //탄착점 로직 오브젝트 -> UI로 변경
    [SerializeField]
    private RectTransform pitcherPitchZoneUI;
    [SerializeField]
    private Transform targetUI_Cam;
    void Start()
    {
        gm = GameManager.instance;
        pitchUI = gm.ui.Get<BallChoiceUI>();
        accUI = gm.ui.Get<AccuracyMiniGameUI>();
        pitchZoneUI = gm.ui.Get<PitcherPitchZoneUI>();
    }
    
    void Update()
    {
        switch (State)
        {
            case (PitchState.SelectingPitchType):
                gm.ui.Show<BallChoiceUI>(); //구종선택 UI
                break;
            case (PitchState.SelectingTarget):
                Pitch();
                break;
            case (PitchState.SetAccuracy):
                //정확도 미니게임 로직 실행
                gm.ui.Show<AccuracyMiniGameUI>();
                accUI.StartAccuaryMiniGame(OnAccuracyResult);
                break;
            case (PitchState.Throwing):
                ThrowBall();
                State = PitchState.Idle;
                break;
        }
    }
    
    public void OnClickPitcherLogicStartBtn() //테스트용 버튼 로직
    {
        State = PitchState.SelectingPitchType;
    }
    void Pitch() // 탄착점 지정함수
    {

        #region 기존 오브젝트로 진행한 탄착점 지정 로직 (수정 전)

        /*    if (is_throw)
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
            }*/
        #endregion

        gm.ui.Show<PitcherPitchZoneUI>();
        PitchType type = pitchUI.pitchType;
        pitchZoneUI.StartPitch(Check, type);
    }
    void Check(bool result)
    {
        if (result) //true
        {
            gm.ui.Hide<PitcherPitchZoneUI>();
            State = PitchState.SetAccuracy;
        }
    }

    void ThrowBall()//확정된 탄착지로 공 던지기
    {
        is_throw = false;
        ballScript.gameObject.SetActive(true);
        ballScript.ThrowBall(pitchUI.pitchType,accuracyResult);
    }
    public void BallToTarget()//도착한 공 비활성화
    {
        ballScript.gameObject.SetActive(false);
        ballScript.Offset_Target_Position.SetActive(false);
        is_throw = true;
    }

    void OnAccuracyResult(AccuracyResult result) //정확도 미니게임결과이후 실행함수
    {
        Debug.Log("정확도 결과" + result);
        switch (result)
        {
            case (AccuracyResult.Perfect):
                accuracyResult = 0.1f;
                break;
            case (AccuracyResult.VeryGood):
                accuracyResult = 0.3f;
                break;
            case (AccuracyResult.Good):
                accuracyResult = 0.5f;
                break;
            case (AccuracyResult.Bad):
                accuracyResult = 0.7f;
                break;
            case (AccuracyResult.Miss):
                accuracyResult = 1.0f;
                break;
        }
        State = PitchState.Throwing;
    }

    public void RequestPitch(PitchRequest request)
    {
        targetObj.transform.position = request.TargetPos;
        //pitchType = request.pitchType;
    }
}
