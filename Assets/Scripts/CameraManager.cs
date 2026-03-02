using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager: MonoBehaviour
{
    // 추후 audioListener 도 카메라에 따라 활성화 및 비활성화 필요
    [SerializeField]
    private List<Camera> cameras;

    private CameraMod gameMod;
    private void OnEnable()
    {
        //카메라 변경 이벤트 구독
        CameraEvents.OnCameraRequest += SetCamera;
    }
    private void OnDisable()
    {
        //카메라 변경 이벤트 구독
        CameraEvents.OnCameraRequest -= SetCamera;
    }
    public void SetCamera(CameraMod mod)
    {
        foreach (Camera cam in cameras)
        {
            if (cam.name == mod.ToString())
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
