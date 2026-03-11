using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitterJudge
{
    private HitterAccuracyConfig hitterAccData;
    //생성자로 데이터 가져오기
    public HitterJudge(HitterAccuracyConfig data)
    {
        hitterAccData = data;
    }
    //타자 타이밍 판단
    public HitterTimingResult HitterTimingJudge(float ratio)
    {
        //타이밍 배율가지고 정확도 체크
        if (ratio < hitterAccData.missLeftEnd) return HitterTimingResult.Miss;
        if (ratio < hitterAccData.fastEnd) return HitterTimingResult.Fast;
        if (ratio < hitterAccData.perfectEnd) return HitterTimingResult.Perfect;
        if (ratio < hitterAccData.slowEnd) return HitterTimingResult.Late;


        return HitterTimingResult.Miss;
    }
}
