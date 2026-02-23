using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUI : UIBase
{

    //public static Action
    public override void OnOpened(object[] param)
    {
        Debug.Log("PlayUI Open");
    }

    public override void OnClosed(object[] param)
    {
        Debug.Log("PlayUI Close");
    }

    //투수 버튼 클릭시 현재 UI닫고 메인 카메라 변경
    public void OnClickPitherBtn()
    {
        CameraEvents.OnCameraRequest?.Invoke(CameraMod.Pitcher_Cam);
        //gm.SetCamera(CameraMod.Pitcher_Cam);
        OpenUI();
    }
    //타자 버튼 클릭시 현재 UI닫고 메인 카메라 변경
    public void OnClickHitterBtn()
    {
        CameraEvents.OnCameraRequest?.Invoke(CameraMod.Hitter_Cam);
        //gm.SetCamera(CameraMod.Hitter_Cam);
        OpenUI();
    }
    void OpenUI()
    {
        uiMan.Hide<PlayUI>();
        uiMan.Show<InGameUI>();
        uiMan.Show<PitchZoneUI>();
    }
}
