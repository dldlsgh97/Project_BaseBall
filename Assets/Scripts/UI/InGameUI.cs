using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//인게임 내의 UI
//거의 모든상황에서 켜져있을 예정
public class InGameUI : UIBase
{
    public override void OnOpened(object[] param)
    {
        Debug.Log("InGameUI Open");
    }

    public override void OnClosed(object[] param)
    {
        Debug.Log("InGameUI Close");
    }
    //일시정지 메뉴 팝업(추후 게임시간 정지등 로직추가 필요(pauseui에서 실행해도 되는 로직))
    public void OnClickPauseBtn()
    {
        uiMan.Show<PauseUI>();
    }
}
