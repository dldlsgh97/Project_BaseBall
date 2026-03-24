using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalJudge
{
    public FinalJudgeResult CalculateFinalJudge(PitchLocation pitcher, HitterTimingResult hitter)
    {
        #region 이전 판단로직
        /*//판단 로직 작성
        if (pitcher == ZoneResult.Strike)
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
        else if(pitcher == ZoneResult.DeadBall)
        {
            if(hitter == HitterTimingResult.None)
            {
                return FinalJudgeResult.DeadBall;
            }
            else
            {
                return FinalJudgeResult.Strike;
            }
            
        }
        else
        {
            //오류출력용
            return FinalJudgeResult.Error;
        }*/
        #endregion

        ZoneResult zone;
        PitchHeightResult height;
        PitchSideResult side;

        zone = pitcher.Zone;
        height = pitcher.Height;
        side = pitcher.Side;

        return FinalJudgeResult.DeadBall;
    }
}
