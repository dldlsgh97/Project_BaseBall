using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitterCtrl : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void OnHit()
    {
        Debug.Log("OnHit");
        //타격판정 로직구현(직접 구현X 판단 주체 스크립트에서 판단)
    }

    public void OnClickSwingBtn()
    {
        anim.Play("HitterSwing", 0, 0f);
    }

}
