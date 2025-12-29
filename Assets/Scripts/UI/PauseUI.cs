using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//일시정지 팝업 스크립트
public class PauseUI : UIBase
{
    public override void OnOpened(object[] param)
    {
        Debug.Log("PauseUI Open");
    }

    public override void OnClosed(object[] param)
    {
        Debug.Log("PauseUI Close");
    }

    //게임 일시정지 해제로직 추가 필요(Onclosed에 추가하면 될것같음)
    public void OnClickResumeBtn()
    {
        uiMan.Hide<PauseUI>();
    }
    //메인메뉴로 복귀
    public void OnClickMainMenuBtn()
    {
        uiMan.Hide<PauseUI>();
        uiMan.Show<PlayUI>();
    }
}
