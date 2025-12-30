using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraMod
{
    Pitcher_Cam,
    Hitter_Cam,
    MainCam
}
public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public UIManager ui;
    private CameraMod gameMod;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    //게임모드에 따른 카메라 변경( 이후 다른 카메라 메니저로 이동예정)
    // 추후 audioListener 도 카메라에 따라 활성화 및 비활성화 필요
    [SerializeField]
    private List<Camera> cameras;
    public void SetCamera(CameraMod mod)
    {
        foreach(Camera cam in cameras)
        {
            if(cam.name == mod.ToString())
            {
                cam.gameObject.SetActive(true);
                cam.enabled = true;               
            }
            else
            {
                cam.enabled = false;
                cam.gameObject.SetActive(false);
            }
        }
    }
}
