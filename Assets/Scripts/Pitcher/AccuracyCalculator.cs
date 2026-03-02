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

    private float accuracyResult;

    private float perfectResult = 0.1f;
    private float veryGoodResult = 0.3f;
    private float goodResult = 0.5f;
    private float badResult = 0.7f;
    private float missResult = 1.0f;
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

    //정확도 -> 오차 수치로 변경함수
    public float CheckResult(AccuracyResult result)
    {
        switch (result)
        {
            case (AccuracyResult.Perfect):
                accuracyResult = perfectResult;
                break;
            case (AccuracyResult.VeryGood):
                accuracyResult = veryGoodResult;
                break;
            case (AccuracyResult.Good):
                accuracyResult = goodResult;
                break;
            case (AccuracyResult.Bad):
                accuracyResult = badResult;
                break;
            case (AccuracyResult.Miss):
                accuracyResult = missResult;
                break;
        }
        return accuracyResult;
    }

}
