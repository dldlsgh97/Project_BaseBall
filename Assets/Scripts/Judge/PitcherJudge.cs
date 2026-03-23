using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitcherJudge : MonoBehaviour
{
    private Rect pitchZonePos;
    private Rect strikeZonePos;
    private StrikeZoneConfig config;

    //Zone 값 받아오기
    public void Initialize(Rect pitchZone,Rect strikeZone,StrikeZoneConfig data)
    {
        pitchZonePos = pitchZone;
        strikeZonePos = strikeZone;
        config = data;
    }

    //스트라이크 판정 메서드
    public PitchLocation JudgeStrike(Vector3 pos)
    {
        PitchLocation result = new PitchLocation();
        #region 판정로직(기초(수정 전))
        /*if (strikeZonePos.Contains(pos))
        {
            return ZoneResult.Strike;
            Debug.Log("Strike");
        }
        else if (pitchZonePos.Contains(pos))
        {
            return ZoneResult.Ball;
            Debug.Log("Ball");
        }
        else
        {
            return ZoneResult.DeadBall;
            Debug.Log("DeadBall");
        }*/
        #endregion

        //PitchZone 내부 판정
        if (!pitchZonePos.Contains(pos))
        {
            return new PitchLocation(
                ZoneResult.DeadBall,
                PitchHeightResult.None,
                PitchSideResult.None);
        }

        //위치 계산
        result.Height = GetPitchHeight(pos);
        result.Side = GetPitchSide(pos);

        //Strike 판정
        if (strikeZonePos.Contains(pos)) result.Zone = ZoneResult.Strike;
        else result.Zone = ZoneResult.Ball;
        Debug.Log($"Pitch Result{result.Zone}, {result.Height},{result.Side}");
        return result;
    }

    //공 높낮이 가져오기
    PitchHeightResult GetPitchHeight(Vector3 pos)
    {
        //상하 위치값 비율
        float ratioY = (pos.y - pitchZonePos.yMin) / pitchZonePos.height;

        if (ratioY < config.BottomZoneRatio) return PitchHeightResult.Low;
        else if (ratioY > 1 - config.TopZoneRatio) return PitchHeightResult.High;
        else return PitchHeightResult.Middle;

    }

    //공 좌우위치 가져오기
    PitchSideResult GetPitchSide(Vector3 pos)
    {
        //상하 위치값 비율
        float ratioX= (pos.x - pitchZonePos.xMin) / pitchZonePos.width;

        if (ratioX < config.LeftZoneRatio) return PitchSideResult.Right;
        else if (ratioX > 1 - config.RightZoneRatio) return PitchSideResult.Left;
        else return PitchSideResult.Center;
    }


}
