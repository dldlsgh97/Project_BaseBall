using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitterAccuracyConfig
{
    public float perfectCenter = 0.7f;
    public float perfectRange = 0.05f;
    public float fastRange = 0.15f;
    public float slowRange = 0.15f;

    public float perfectStart;
    public float perfectEnd;

    public float fastStart;
    public float fastEnd;

    public float slowStart;
    public float slowEnd;

    public float missLeftEnd;
    public float missRightStart;

    public HitterAccuracyConfig()
    {
        CalculateRanges();
    }

    void CalculateRanges()
    {
        perfectStart = perfectCenter - perfectRange;
        perfectEnd = perfectCenter + perfectRange;

        fastStart = perfectStart - fastRange;
        fastEnd = perfectStart;

        slowStart = perfectEnd;
        slowEnd = perfectEnd + slowRange;

        missLeftEnd = fastStart;
        missRightStart = slowEnd;
    }
}
