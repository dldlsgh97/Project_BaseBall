using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalJudge
{
    public FinalHitResult CalculateFinalJudge(PitchLocation pitcher, HitterTimingResult hitter)
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

        FinalHitResult result = new FinalHitResult();
        ZoneResult zone;
        PitchHeightResult height;
        PitchSideResult side;

        zone = pitcher.Zone;
        height = pitcher.Height;
        side = pitcher.Side;

        result.Height = HitHeightJudgeResult(hitter, height);
        result.Side = HitSideJudgeResult(hitter, side);
        result.Result = HitJudgeResult(hitter, zone);
        return result;
    }

    FinalJudgeResult HitJudgeResult(HitterTimingResult hit, ZoneResult zone)
    {
        if (zone == ZoneResult.Strike)
        {
            if (hit == HitterTimingResult.None)
            {
                return FinalJudgeResult.Strike;
            }
            else
            {
                return FinalJudgeResult.Hit;
            }
        }
        else if (zone == ZoneResult.Ball)
        {
            if (hit == HitterTimingResult.None)
            {
                return FinalJudgeResult.Ball;
            }
            else
            {
                return FinalJudgeResult.Strike;
            }
        }
        else if (zone == ZoneResult.DeadBall)
        {
            if (hit == HitterTimingResult.None)
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
        }
    }

    HitHeightResult HitHeightJudgeResult(HitterTimingResult hit,PitchHeightResult height)
    {
        switch (height)
        {
            case PitchHeightResult.High:
                switch (hit)
                {
                    case HitterTimingResult.Fast: return HitHeightResult.VeryHigh;
                    case HitterTimingResult.Perfect: return HitHeightResult.High;
                    case HitterTimingResult.Late: return HitHeightResult.Middle;
                }
                break;
            case PitchHeightResult.Middle:
                switch (hit)
                {
                    case HitterTimingResult.Fast: return HitHeightResult.High;
                    case HitterTimingResult.Perfect: return HitHeightResult.Middle;
                    case HitterTimingResult.Late: return HitHeightResult.Low;
                }
                break;
            case PitchHeightResult.Low:
                switch (hit)
                {
                    case HitterTimingResult.Fast: return HitHeightResult.Middle;
                    case HitterTimingResult.Perfect: return HitHeightResult.Low;
                    case HitterTimingResult.Late: return HitHeightResult.VeryLow;
                }
                break;
        }
        return HitHeightResult.Middle;
    }

    HitSideResult HitSideJudgeResult(HitterTimingResult hit, PitchSideResult side)
    {
        switch (side)
        {
            case PitchSideResult.Left:
                switch (hit)
                {
                    case HitterTimingResult.Fast: return HitSideResult.VeryLeft;
                    case HitterTimingResult.Perfect: return HitSideResult.Left;
                    case HitterTimingResult.Late: return HitSideResult.Center;
                }
                break;
            case PitchSideResult.Center:
                switch (hit)
                {
                    case HitterTimingResult.Fast: return HitSideResult.Left;
                    case HitterTimingResult.Perfect: return HitSideResult.Center;
                    case HitterTimingResult.Late: return HitSideResult.Right;
                }
                break;
            case PitchSideResult.Right:
                switch (hit)
                {
                    case HitterTimingResult.Fast: return HitSideResult.Center;
                    case HitterTimingResult.Perfect: return HitSideResult.Right;
                    case HitterTimingResult.Late: return HitSideResult.VeryRight;
                }
                break;
        }
        return HitSideResult.Center;
    }
}
