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


    public PitchState State;

    //탄착점 로직 오브젝트 -> UI로 변경
    [SerializeField]
    private RectTransform pitcherPitchZoneUI;
    [SerializeField]
    private Transform targetUI_Cam;
    void Start()
    {
        gm = GameManager.instance;
    }
    
    void Update()
    {
        switch (State)
        {
            /*case (PitchState.SelectingPitchType):
                gm.ui.Show<BallChoiceUI>(); //구종선택 UI
                break;*/
            /*case (PitchState.SelectingTarget):
                Pitch();
                break;
            case (PitchState.SetAccuracy):
                //정확도 미니게임 로직 실행
                gm.ui.Show<AccuracyMiniGameUI>();
                accUI.StartAccuaryMiniGame(OnAccuracyResult);
                break;*/
            case (PitchState.Throwing):
                ThrowBall();
                State = PitchState.Idle;
                break;
        }
    }        

    void ThrowBall()//확정된 탄착지로 공 던지기
    {
        is_throw = false;
        ballScript.gameObject.SetActive(true);
        //ballScript.ThrowBall(pitchUI.pitchType,accuracyResult);
    }
    public void BallToTarget()//도착한 공 비활성화
    {
        ballScript.gameObject.SetActive(false);
        ballScript.Offset_Target.SetActive(false);
        is_throw = true;
    }

    //자동 투수로직에 필요한 함수
    /*public void RequestPitch(PitchRequest request)
    {
        PitchType type = request.PitchType;
        switch (request.Accuracy)
        {
            case (AccuracyResult.Perfect):
                accuracyResult = perfectResult;
                break;
            case (AccuracyResult.VeryGood):
                accuracyResult = veryGoodResult;
                break;
            case (AccuracyResult.Good):
                accuracyResult = goodResult;
                break;
            case (AccuracyResult.Bad):
                accuracyResult = badResult;
                break;
            case (AccuracyResult.Miss):
                accuracyResult = missResult;
                break;
        }
        float accuracy = accuracyResult;
        Vector3 targetPos = request.TargetPos;
        ballScript.gameObject.SetActive(true);
        ballScript.ThrowBall(type, accuracy, targetPos);

    }*/
}
