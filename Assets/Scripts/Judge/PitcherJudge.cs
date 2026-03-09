using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitcherJudge : MonoBehaviour
{
    private Rect pitchZonePos;
    private Rect strikeZonePos;

    //Zone 값 받아오기
    public void Initialize(Rect pitchZone,Rect strikeZone,float z)
    {
        pitchZonePos = pitchZone;
        strikeZonePos = strikeZone;
    }

    //스트라이크 판정 메서드
    public ZoneResult JudgeStrike(Vector3 pos)
    {
        if (strikeZonePos.Contains(pos))
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
        }
    }


}
