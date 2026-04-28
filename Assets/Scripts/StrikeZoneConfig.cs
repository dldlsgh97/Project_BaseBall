using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeZoneConfig
{
    [Header("PitchZone 상하구역 설정")]
    public float TopZoneRatio = 0.3f;
    public float BottomZoneRatio = 0.3f;

    [Header("PitchZone 좌우구역 설정")]
    public float LeftZoneRatio = 0.3f;
    public float RightZoneRatio = 0.3f;
}

