using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool is_throw = true;

    public float PitchSpeed = 10f;
    public Vector3 TargetPosition;
    public bool hasTarget = false;

    void Start()
    {
        
    }
    void Update()
    {
        Pitch();
        Debug.Log("PitcherCtrl_ Update");
        //추후 인풋 시스템으로 변경 예정
        if (Input.GetMouseButtonDown(0))
        {
            is_throw = false;
            ballScript.gameObject.SetActive(true);
            ballScript.ThrowBall();
        }

    }

    void Pitch()
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
    }
    public void BallToTarget()
    {
        ballScript.gameObject.SetActive(false);
        is_throw = true;
    }
}
