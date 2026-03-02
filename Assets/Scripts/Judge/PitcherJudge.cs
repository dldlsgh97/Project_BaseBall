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
    public void JudgeStrike(Vector3 pos)
    {
        if (strikeZonePos.Contains(pos))
        {
            Debug.Log("Strike");
        }
        else if (pitchZonePos.Contains(pos))
        {
            Debug.Log("Ball");
        }
        else
        {
            Debug.Log("DeadBall");
        }
    }
}
