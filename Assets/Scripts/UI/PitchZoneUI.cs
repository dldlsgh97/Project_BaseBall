using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchZoneUI : UIBase
{
    //PitchZone의 모서리 4점을 World좌표로 저장
    Vector3[] corners = new Vector3[4];

    private RectTransform rt;
    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }
    void OnEnable()
    {        
        CalculateCorner();
    }

    void CalculateCorner()
    {
        rt.GetWorldCorners(corners);
    }

    public Vector3[] GetCorners()
    {
        return corners;
    }
}
