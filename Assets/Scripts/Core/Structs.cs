using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PitchRequest
{
    public PitchType PitchType;
    public Vector3 TargetPos;
    public float Accuracy;
}

// 정확한 투구판정 위한 구조체
public struct PitchLocation
{
    public ZoneResult Zone;
    public PitchHeightResult Height;
    public PitchSideResult Side;

    public PitchLocation(ZoneResult zone, PitchHeightResult height, PitchSideResult side)
    {
        Zone = zone;
        Height = height;
        Side = side;
    }
}