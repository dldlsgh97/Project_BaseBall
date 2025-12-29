using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIBase : MonoBehaviour
{
    public UnityAction<object[]> opened;
    public UnityAction<object[]> closed;

    // 코드 압축 및 작성 편의를 위해 GameManager과 UIManager을 변수화
    protected GameManager gm;
    protected UIManager uiMan;
    private void Awake()
    {
        opened = OnOpened;
        closed = OnClosed;
        
    }
    private void Start()
    {
        gm = GameManager.instance;
        uiMan = GameManager.instance.ui;
    }

    public void SetActive(bool isActive) //코드 압축용 SetActive 함수 생성
    {
        gameObject.SetActive(isActive);
    }

    public virtual void OnOpened(object[] param) 
    {
        //UI 열릴때 실행되는 기능
    }

    public virtual void OnClosed(object[] param) 
    {
        //UI 닫힐때 실행되는 기능
    }

}
