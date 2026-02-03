using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PitcherPitchZoneUI : UIBase
{
    private Action<bool> onComplete; //콜백 저장용 변수

    [SerializeField] //탄착점 표시 UI(투수 전용)
    private RectTransform TargetUI;
    [SerializeField] //공 던지는 구역 UI(투수 전용)
    private RectTransform PitchZone;
    [SerializeField] //탄착점 표시 UI(WorldUI)
    private RectTransform WorldTargetUI;
    [SerializeField] //투수UI캔버스
    private Canvas UICanvas;
   
    //TestObj 최종 위치값 => 탄착점
    private Vector3 WorldTargetPos;

    [SerializeField]
    private TextMeshProUGUI PitchTypeText;

    [SerializeField]
    private GameObject TestObj;

    public void StartPitch(Action<bool> callback, PitchType type)
    {
        onComplete = callback;
        SetPitchTypeText(type);
    }

    void SetPitchTypeText(PitchType type)
    {
        switch (type)
        {
            case PitchType.FastBall:
                PitchTypeText.text = "직구";
                break;
            case PitchType.CurveBall:
                PitchTypeText.text = "커브";
                break;
            case PitchType.SliderBall:
                PitchTypeText.text = "슬라이더";
                break;
        }
    }
    void Update()
    {
        Vector2 localMousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            PitchZone, Input.mousePosition, UICanvas.worldCamera, out localMousePos);

        if (PitchZone.rect.Contains(localMousePos))
        {
            TargetUI.gameObject.SetActive(true);
            
            TargetUI.localPosition = localMousePos;
            WorldTargetUI.localPosition = TargetUI.localPosition;
            WorldTargetPos = WorldTargetUI.position;
            TestObj.transform.position = WorldTargetPos;
            
            if (Input.GetMouseButtonDown(0))//커서로 지정후 클릭시 확정
            {
                onComplete?.Invoke(true); //콜백으로 결과 전달
                //State = PitchState.SetAccuracy;
            }
        }
    }
}
