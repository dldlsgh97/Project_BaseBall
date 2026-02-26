using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitcher_Com : MonoBehaviour
{
    //타자모드에서 투수가 공을 던져주는 로직
    [SerializeField]
    private PitcherCtrl pitcherCtrl;

    [SerializeField]
    private RectTransform PitchZone;

    //임시 정확도 배율
    private float perfectRatio = 1f;
    private float veryGoodRatio = 0.9f;
    private float goodRatio = 0.6f;
    private float badRatio = 0.1f;

    private PitchZoneUI pitchZoneUI;

    private void Start()
    {
        //pitchZoneSize = PitchZone.GetComponent<Renderer>().bounds.size;
        pitchZoneUI = GameManager.instance.ui.Get<PitchZoneUI>();
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
        //스트라이크존 중심점
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
        return new Vector3(randomX,randomY,z);
    }
    private AccuracyResult DecideAccuracy()
    {
        //정확도 지정
        float rand = Random.value;
        AccuracyResult result = AccuracyResult.Bad;
        
        if(rand < badRatio)
        {
            result = AccuracyResult.Bad;
        }
        else if(rand < goodRatio)
        {
            result = AccuracyResult.Good;
        }
        else if(rand < veryGoodRatio)
        {
            result = AccuracyResult.VeryGood;
        }
        else if(rand < perfectRatio)
        {
            result = AccuracyResult.Perfect;
        }
        else
        {
            result = AccuracyResult.Miss;
        }
       
        return result;
    }
    public void OnClickAIPitcherBtn()
    {
        //구조체 데이터 설정
        PitchRequest request = new PitchRequest
        {
            PitchType = DecidePitchType(),
            TargetPos = DecideTargetPos(),
            Accuracy = DecideAccuracy()
        };
        //구조체 데이터 넘겨주기
        //pitcherCtrl.RequestPitch(request);
    }

    
}
