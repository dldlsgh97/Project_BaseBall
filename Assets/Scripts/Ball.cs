using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Transform StartPosition;
    [SerializeField]
    private Transform TargetPosition; //정확도 오차없는 탄착점
    [SerializeField]
    private PitcherCtrl pitcherCtrl;
    [SerializeField]
    private float ballSpeed;
    [SerializeField]
    private Vector3 midPosition;

    private float elapsed;

    private float curveDuration = 1.0f;
    public float curveAmount = 2.0f;

    [SerializeField]
    private Transform targetPoint; //정확도 오차적용된 탄착점
    [SerializeField]
    public GameObject Offset_Target_Position;
    void Start()
    {
        gameObject.transform.position = StartPosition.position;
        ballSpeed = pitcherCtrl.PitchSpeed;
    }

    public void ThrowBall(PitchType type, float accuracy)
    {
        SetTargetPosition(accuracy);
        switch (type)
        {
            case PitchType.FastBall:
                StartCoroutine(FastBall());
                break;
            case PitchType.CurveBall:
                StartCoroutine(CurveBall());
                break;
            case PitchType.SliderBall:
                StartCoroutine(SliderBall());
                break;
        }
    }

    void SetTargetPosition(float accuracy)
    {
        float horizontalOffset = Random.RandomRange(0, accuracy); //가로 오차 랜덤값
        float verticalOffset = Random.RandomRange(0, accuracy); //세로 오차 랜덤값
        
        float signX = 1; //가로오차 +- 부호
        float signY = 1; //세로오차 +- 부호

        if(Random.value < 0.5f) signX = -1;
        if (Random.value < 0.5f) signY = -1;
        Vector3 start = StartPosition.position;
        Vector3 end = TargetPosition.position;

        Vector3 right = Vector3.Cross(Vector3.up, end - start).normalized;

        Vector3 offSet = (right * horizontalOffset * signX) + (Vector3.up * verticalOffset * signY);

        targetPoint.position += offSet;
        Offset_Target_Position.SetActive(true);
        Offset_Target_Position.transform.position = targetPoint.position;
    }
    IEnumerator FastBall()
    {
        Vector3 target = targetPoint.position;
        while (Vector3.Distance(gameObject.transform.position , target) > 0.05f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target, ballSpeed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("공 도착");
        gameObject.transform.position = StartPosition.position;
        pitcherCtrl.BallToTarget();
    }

    IEnumerator CurveBall()
    {
        Vector3 start = StartPosition.position;
        Vector3 end = targetPoint.position;       
        Vector3 mid = (start + end) * 0.5f;

        Vector3 right = Vector3.Cross(Vector3.up, end - start).normalized;

        Vector3 apex = mid + right * curveAmount;

        elapsed = 0f;

        while (elapsed < curveDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / curveDuration;

            Vector3 p1 = Vector3.Lerp(start, apex, t);
            Vector3 p2 = Vector3.Lerp(apex, end, t);
            Vector3 curvePos = Vector3.Lerp(p1, p2, t);

            transform.position = curvePos;

            yield return null;
        }

        Debug.Log("커브볼 도착");
        transform.position = StartPosition.position;
        pitcherCtrl.BallToTarget();
    }
    IEnumerator SliderBall()
    {
        Vector3 start = StartPosition.position;
        Vector3 end = targetPoint.position;
        Vector3 mid = (start + end) * 0.5f;

        Vector3 right = Vector3.Cross(Vector3.up, end - start).normalized;

        Vector3 apex = mid - right * curveAmount;

        elapsed = 0f;

        while (elapsed < curveDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / curveDuration;

            Vector3 p1 = Vector3.Lerp(start, apex, t);
            Vector3 p2 = Vector3.Lerp(apex, end, t);
            Vector3 curvePos = Vector3.Lerp(p1, p2, t);

            transform.position = curvePos;

            yield return null;
        }

        Debug.Log("슬라이더볼 도착");
        transform.position = StartPosition.position;
        pitcherCtrl.BallToTarget();
    }
}
