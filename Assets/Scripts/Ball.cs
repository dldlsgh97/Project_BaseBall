using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Transform StartPosition;
    [SerializeField]
    private Transform TargetPosition;
    [SerializeField]
    private PitcherCtrl pitcherCtrl;
    [SerializeField]
    private float ballSpeed;
    [SerializeField]
    private Vector3 midPosition;

    private float elapsed;

    private float curveDuration = 1.0f;
    public float curveAmount = 2.0f;
    void Start()
    {
        gameObject.transform.position = StartPosition.position;
        ballSpeed = pitcherCtrl.PitchSpeed;
    }
    void Update()
    {
        
    }
    public void ThrowBall(PitchType type)
    {
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

    IEnumerator FastBall()
    {
        Vector3 target = TargetPosition.position;
        while (Vector3.Distance(gameObject.transform.position , target) > 0.05f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target, ballSpeed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("°ø µµÂø");
        gameObject.transform.position = StartPosition.position;
        pitcherCtrl.BallToTarget();
    }

    IEnumerator CurveBall()
    {
        Vector3 start = StartPosition.position;
        Vector3 end = TargetPosition.position;       
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

        Debug.Log("Ä¿ºêº¼ µµÂø");
        transform.position = StartPosition.position;
        pitcherCtrl.BallToTarget();
    }
    IEnumerator SliderBall()
    {
        Vector3 start = StartPosition.position;
        Vector3 end = TargetPosition.position;
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

        Debug.Log("½½¶óÀÌ´õº¼ µµÂø");
        transform.position = StartPosition.position;
        pitcherCtrl.BallToTarget();
    }
}
