using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Start()
    {
        gm = GameManager.instance;
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 임시 테스트용
        {
            State = PitchState.SelectingPitchType;
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
        Debug.Log("정확도 로직");
        if (Input.GetMouseButtonDown(0))
        {
            State = PitchState.Throwing;
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
