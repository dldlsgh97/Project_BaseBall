using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitcherExecutor : MonoBehaviour
{
    [SerializeField]
    private Ball ball;

    //PitcherFlowManager로 투구 종료 알리는 이벤트
    public event Action OnPitchFinished;

    //타자 UI타이밍 전달 이벤트
    public Action<float> OnStartHittingTimer;

    public Action<Vector3> OnOffSetBallTargetPos;
    //일반 투구 로직
    public void ExecutePitch(PitchRequest request)
    {
        ball.OnBallToTarget -= EndPitch;//중복 방지
        ball.OnOffsetTargetPosition -= OffSetBallTargetPos;//중복 방지
        ball.gameObject.SetActive(true);
        ball.OnStartHittingTimer += StartHitterTiming;
        ball.OnOffsetTargetPosition += OffSetBallTargetPos;
        ball.ThrowBall(request.PitchType, request.Accuracy, request.TargetPos);
        //투구 종료 트리거
        ball.OnBallToTarget += EndPitch;
    }

    void EndPitch()
    {
        //스트라이크 판정 스크립트 만들어서 이벤트로 넘기기로직 추가
        ball.OnBallToTarget -= EndPitch;
        ball.OnStartHittingTimer -= StartHitterTiming;
        ball.OnOffsetTargetPosition -= OffSetBallTargetPos;
        ball.gameObject.SetActive(false);
        ball.Offset_Target.SetActive(false);
        //투구 로직 종료
        OnPitchFinished?.Invoke();
    }

    void StartHitterTiming(float duration)
    {
        OnStartHittingTimer?.Invoke(duration);
    }

    void OffSetBallTargetPos(Vector3 pos)
    {
        OnOffSetBallTargetPos?.Invoke(pos);
    }

}
