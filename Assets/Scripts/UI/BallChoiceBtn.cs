using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallChoiceBtn : MonoBehaviour
{
    [SerializeField]
    private BallChoiceUI ballUI;
    [SerializeField]
    private PitchType pitchType;

    //선택한 구종 정보 넘겨주기
    public void OnClickBallChoiceBtn()
    {
        ballUI.SelectPitchType(pitchType);          
    }
}
