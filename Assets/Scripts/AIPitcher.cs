using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPitcher : MonoBehaviour
{
    [SerializeField]
    private Rect pitchZone;

    [SerializeField]
    private float targetZ;

    public void Initialize(Rect Zone,float z)
    {
        pitchZone = Zone;
        targetZ = z;
    }
    private PitchType DecidePitchType()
    {
        PitchType result = PitchType.FastBall;
        int rand = Random.Range(1, 4);
        switch (rand)
        {
            case 1:
                result = PitchType.FastBall;
                break;
            case 2:
                result = PitchType.CurveBall;
                break;
            case 3:
                result = PitchType.SliderBall;
                break;
        }
        //구종 선택
        return result;
    }
    private Vector3 DecideTargetPos()
    {
        #region 이전 로직
        /*//스트라이크존 중심점
        Vector3 center = PitchZone.transform.position;

        //스트라이크존 UI의 네 모서리 위치 받아오기
        //corners[0] -> 좌하단 모서리 corners[2] -> 우상단 모서리
        Vector3[] corners = pitchZoneUI.GetCorners();
        float minX = corners[0].x;
        float maxX = corners[2].x;
        float minY = corners[0].y;
        float maxY = corners[2].y;

        //랜덤한 탄착점 지정
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        float z = center.z;
        //탄착점 출력
        return new Vector3(randomX,randomY,z);*/
        #endregion

        float x = Random.Range(pitchZone.xMin, pitchZone.xMax);
        float y = Random.Range(pitchZone.yMin, pitchZone.yMax);
        return new Vector3(x, y, targetZ);
    }
    private float DecideAccuracy()
    {
        //정확도 지정 -> 0~1사이의 실수
        float rand = Random.value;               
        return rand;
    }
    public PitchRequest SetAIPitcher()
    {
        //구조체 데이터 설정
        PitchRequest request = new PitchRequest
        {
            PitchType = DecidePitchType(),
            TargetPos = DecideTargetPos(),
            Accuracy = DecideAccuracy()
        };
        //구조체 데이터 넘겨주기
        return request;
    }

    
}
