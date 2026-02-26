using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PitcherCtrl : MonoBehaviour
{
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


    public float PitchSpeed = 10f;
    public Vector3 TargetPosition;
    public bool hasTarget = false;


    //탄착점 로직 오브젝트 -> UI로 변경
    [SerializeField]
    private RectTransform pitcherPitchZoneUI;
    [SerializeField]
    private Transform targetUI_Cam;
    void Start()
    {
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
