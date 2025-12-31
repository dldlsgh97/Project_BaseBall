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
    void Start()
    {
        gameObject.transform.position = StartPosition.position;
        ballSpeed = pitcherCtrl.PitchSpeed;
    }
    void Update()
    {
        
    }
    public void ThrowBall()
    {
        StartCoroutine(BallToTarget(TargetPosition.position));
    }

    IEnumerator BallToTarget(Vector3 target)
    {
        while(Vector3.Distance(gameObject.transform.position , target) > 0.05f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target, ballSpeed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("°ø µµÂø");
        gameObject.transform.position = StartPosition.position;
        pitcherCtrl.BallToTarget();
    }
}
