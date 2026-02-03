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
    private Vector3 pitchZoneSize;


    //임시 정확도 배율
    private float perfectRatio = 1f;
    private float veryGoodRatio = 0.9f;
    private float goodRatio = 0.6f;
    private float badRatio = 0.1f;
    private void Start()
    {
        pitchZoneSize = PitchZone.GetComponent<Renderer>().bounds.size;
    }
    private PitchType DecidePitchType()
    {
        PitchType result = PitchType.FastBall;
        int rand = Random.RandomRange(1, 4);
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
        Vector3 center = PitchZone.transform.position;
        float zoneWidth = PitchZone.rect.width;
        float zoneHeight = PitchZone.rect.height;
        //탄착점 지정
        return new Vector3(0, 0, 0);
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
        pitcherCtrl.RequestPitch(request);
    }

    
}
