using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitterJudge : MonoBehaviour
{

    private float hitterTimingRatio;

    public HitterTimingResult HitterTimingJudge(float ratio)
    {
        return HitterTimingResult.Fast;
    }
}
