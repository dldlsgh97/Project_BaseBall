using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchZoneUI : UIBase
{
    [SerializeField]
    private RectTransform pitchZone;
    [SerializeField]
    private RectTransform strikeZone;

    //PitchZone의 모서리 4점을 World좌표로 저장
    Vector3[] pitchZonecorners = new Vector3[4];
    //StrikeZone의 모서리 4점을 World좌표로 저장
    Vector3[] strikeZoneCorners = new Vector3[4];

    void OnEnable()
    {        
        CalculateCorner();
    }

    void CalculateCorner()
    {
        pitchZone.GetWorldCorners(pitchZonecorners);
        strikeZone.GetWorldCorners(strikeZoneCorners);
    }

    public Rect GetPitchZoneWorldRect()
    {
        return ConvertToRect(pitchZonecorners);
    }
    public Rect GetStrikeZoneWorldRect()
    {
        return ConvertToRect(strikeZoneCorners);
    }

    //World좌표 -> Rect변환
    private Rect ConvertToRect(Vector3[] corners)
    {
        float width = corners[2].x - corners[0].x;
        float height = corners[2].y - corners[0].y;

        return new Rect(corners[0].x, corners[0].y, width, height);
    }
}
