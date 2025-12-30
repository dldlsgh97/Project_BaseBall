using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUI : UIBase
{
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
        uiMan.Hide<PlayUI>();
        gm.SetCamera(CameraMod.Pitcher_Cam);
        uiMan.Show<InGameUI>();
    }
    //타자 버튼 클릭시 현재 UI닫고 메인 카메라 변경
    public void OnClickHitterBtn()
    {
        uiMan.Hide<PlayUI>();
        gm.SetCamera(CameraMod.Hitter_Cam);
        uiMan.Show<InGameUI>();
    }
}
