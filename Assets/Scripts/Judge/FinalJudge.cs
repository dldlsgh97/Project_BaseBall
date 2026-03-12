using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalJudge
{
    public FinalJudgeResult CalculateFinalJudge(ZoneResult pitcher, HitterTimingResult hitter)
    {       
        //판단 로직 작성
        if(pitcher == ZoneResult.Strike)
        {
            if(hitter == HitterTimingResult.None)
            {
                return FinalJudgeResult.Strike;
            }
            else
            {
                return FinalJudgeResult.Hit;
            }
        }
        else if(pitcher == ZoneResult.Ball)
        {
            if (hitter == HitterTimingResult.None)
            {
                return FinalJudgeResult.Ball;
            }
            else
            {
                return FinalJudgeResult.Strike;
            }
        }
        else
        {
            //오류
            return FinalJudgeResult.Hit;
        }
        
    }
}
