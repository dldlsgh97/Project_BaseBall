using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallChoiceUI : UIBase
{
    public PitchType pitchType;

    public PitcherCtrl pitcherCtrl;

    public override void OnOpened(object[] param)
    {
        Debug.Log("BallChoiceUI Open");
    }

    public override void OnClosed(object[] param)
    {
        pitcherCtrl.State = PitchState.SelectingTarget;
        Debug.Log("BallChoiceUI Close");

    }

    
}
