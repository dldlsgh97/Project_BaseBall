using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBall : MonoBehaviour
{
    //타격결과 데이터
    private FinalHitResult result;
    //투수 탄착점 및 공 출발지점
    private Vector3 StartPos;
    //타자 스텟 데이터
    private HitterStatConfig data;
    //로직 시작 트리거변수
    private bool isInit = false;
    //공이 날아갈 방향
    private Vector3 velocity;
    //중력변수(공이 날아갈떄 포물선으로 날아가기위한 변수)
    [SerializeField]
    private float gravity = -9.8f;

    //데이터 초기화
    public void Init(FinalHitResult hitResult, Vector3 pos, HitterStatConfig config)
    {
        result = hitResult;
        StartPos = pos;
        data = config;

        StartLogic();
    }

    public void StartLogic()
    {
        transform.position = StartPos;
        Vector3 dir = CalculateAngle();
        float power = data.HitterPower;

        velocity = dir * power;
        isInit = true;
    }

    Vector3 CalculateAngle()
    {
        float horizontalAngle = GetHorizontalAngle(result.Side);
        float VerticalAngle = GetVerticalAngle(result.Height);

        Quaternion rotation = Quaternion.Euler(VerticalAngle, horizontalAngle, 0f);
        return rotation * Vector3.back;
    }
    //좌우 각도 계산
    float GetHorizontalAngle(HitSideResult side)
    {
        switch (side)
        {
            case HitSideResult.VeryLeft: return -60f;
            case HitSideResult.Left: return -30f;
            case HitSideResult.Center: return 0f;
            case HitSideResult.Right: return 30f;
            case HitSideResult.VeryRight: return 60f;
        }
        return 0f;
    }

    //상하 각도 계산
    float GetVerticalAngle(HitHeightResult height)
    {
        switch (height)
        {
            case HitHeightResult.VeryLow: return 5f;
            case HitHeightResult.Low: return 15f;
            case HitHeightResult.Middle: return 30f;
            case HitHeightResult.High: return 45f;
            case HitHeightResult.VeryHigh: return 60f;
        }
        return 0f;
    }

    private void Update()
    {
        if (!isInit) return;
        velocity.y += gravity * Time.deltaTime;

        transform.position += velocity * Time.deltaTime;
    }
}
