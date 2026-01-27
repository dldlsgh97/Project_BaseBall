using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallChoiceBtn : MonoBehaviour
{
    [SerializeField]
    private BallChoiceUI ballUI;
    [SerializeField]
    private PitchType pitchType;
    public void OnClickBallChoiceBtn()
    {
        ballUI.pitchType = pitchType;
        GameManager.instance.ui.Hide<BallChoiceUI>();
    }
}
