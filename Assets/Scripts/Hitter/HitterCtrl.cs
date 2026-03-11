using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitterCtrl : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private HitterTimingGaugeUI timingUI;

    public Action OnSwingBat;
    private void Start()
    {
        anim = GetComponent<Animator>();       
    }
    //애니메이션 이벤트(타자배트가 스트라이크존을 지나는 시점)
    public void OnHit()
    {
        Debug.Log("OnHit");

        //타격판정 로직구현(직접 구현X 판단 주체 스크립트에서 판단)
        OnSwingBat?.Invoke();
        timingUI.StopHitterCoroutine();
    }

    public void OnClickSwingBtn()
    {
        anim.Play("HitterSwing", 0, 0f);        
    }

}
