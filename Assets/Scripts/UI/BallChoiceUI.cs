using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallChoiceUI : UIBase
{
    //구종선택 이벤트
    public Action<PitchType> OnPitchTypeSelected;

    public override void OnOpened(object[] param)
    {
        Debug.Log("BallChoiceUI Open");
    }

    public override void OnClosed(object[] param)
    {
        Debug.Log("BallChoiceUI Close");
    }

    public void SelectPitchType(PitchType type)
    {
        OnPitchTypeSelected?.Invoke(type);
    }
}
