using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitterStatConfig
{
    //기본 타자 파워 스텟
    //추후 스텟 데이터로 기본 파워 스텟 이동
    public float HitterPower = 30f;

    //파워 스텟 세분화 계산
    public float VeryLow = 0.5f;
    public float Low = 0.7f;
    public float Middle = 1.0f;
    public float High = 1.2f;
    public float VeryHigh = 1.4f;
}
