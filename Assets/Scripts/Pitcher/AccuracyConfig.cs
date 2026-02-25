using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyConfig
{
    //생성자에서 구간 비율계산
    public AccuracyConfig()
    {
        SetAccuaryRatio();
    }
    [Header("정확도 구간")]
    public float perfectStartRatio;
    public float perfectEndRatio;
    
    public float goodStartRatio;
    public float goodEndRatio;
    
    public float veryGoodStartRatio;
    public float veryGoodEndRatio;
    
    public float badStartRatio = 0;
    public float badEndRatio;
    
    public float perfectRatio = 0.1f;
    public float veryGoodRatio = 0.3f;
    public float goodRatio = 0.5f;
    public float badRatio = 0.1f;

    void SetAccuaryRatio() //정확도 구간 비율에 따른 구간 시작점과 끝점 설정
    {
        badEndRatio = badStartRatio + badRatio;
        goodStartRatio = badEndRatio;
        goodEndRatio = goodStartRatio + goodRatio;
        veryGoodStartRatio = goodEndRatio;
        veryGoodEndRatio = veryGoodStartRatio + veryGoodRatio;
        perfectStartRatio = veryGoodEndRatio;
        perfectEndRatio = perfectStartRatio + perfectRatio;
    }
}
