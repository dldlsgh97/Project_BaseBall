using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyCalculator
{
    private AccuracyConfig accData;
    //데이터를 넘겨받기위한 생성자 구현
    public AccuracyCalculator(AccuracyConfig data)
    {
        accData = data;
    }

    public AccuracyResult Calculate(float ratio)
    {
        //로직 작성
        if(ratio < 0) return AccuracyResult.Miss;
        if (ratio <= accData.badEndRatio) return AccuracyResult.Bad;
        if (ratio <= accData.goodEndRatio) return AccuracyResult.Good;
        if (ratio <= accData.veryGoodEndRatio) return AccuracyResult.VeryGood;
        if (ratio <= accData.perfectEndRatio) return AccuracyResult.Perfect;
        
        //출력시 오류
        return AccuracyResult.Miss;
    }

}
