using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Debug용 UI 가로")]
    //가로
    public RectTransform DebugLine_1;
    public RectTransform DebugLine_2;
    [Header("Debug용 UI 세로")]
    //세로
    public RectTransform DebugLine_3;
    public RectTransform DebugLine_4;

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

    //디버깅용
    public void UIDebug(StrikeZoneConfig config)
    {
        Rect Zone = pitchZone.rect;

        float width = Zone.width;
        float height = Zone.height;

        float xMin = -width / 2f;
        float xMax = width / 2f;

        float yMin = -height / 2f;
        float yMax = height / 2f;

        float leftX = Mathf.Lerp(xMin, xMax, config.LeftZoneRatio);
        float rightX = Mathf.Lerp(xMin, xMax, 1f - config.RightZoneRatio);

        float bottomY = Mathf.Lerp(yMin, yMax, config.BottomZoneRatio);
        float topY = Mathf.Lerp(yMin, yMax, 1f - config.TopZoneRatio);

        Debug.Log($"LeftX{leftX} rightX{rightX}");
        //가로
        DebugLine_1.anchoredPosition = new Vector2(0f, bottomY);
        DebugLine_2.anchoredPosition = new Vector2(0f, topY);
        //세로
        DebugLine_3.anchoredPosition = new Vector2(leftX,0f);
        DebugLine_4.anchoredPosition = new Vector2(rightX, 0f);

    }
}
