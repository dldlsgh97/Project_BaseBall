using System.Collections;
using System.Collections.Generic;

public enum PitchType
{
    FastBall,//직구
    CurveBall,//커브
    SliderBall//슬라이더
}
public enum CameraMod
{
    Pitcher_Cam, //투수 카메라
    Hitter_Cam, //타자 카메라
    MainCam //메인 카메라
}

public enum PitchState
{
    Idle,//아무것도 안하는 상태(기본 상태)
    SelectingPitchType,//구종 선택
    SelectingTarget,// 타겟위치 선택
    SetAccuracy,//정확도 조정
    Throwing//던지는 상태(최종 상태)
}

//정확도 미니게임 결과 enum
public enum AccuracyResult
{
    Perfect,
    VeryGood,
    Good,
    Bad,
    Miss
}

//투구 판정(투수 판정) 결과 enum
public enum ZoneResult
{
    Strike,
    Ball,
    DeadBall
}

//투구 판정(타자 판정) 결과 enum
public enum HitterTimingResult
{
    Fast,
    Late,
    Perfect,
    Miss
}

//투구 판정(타자 + 투수 판정 -> 최종 투구 판정) 결과 enum
public enum PitchResult
{
    Strike,
    Ball,
    Foul,
    Hit,
    HomeRun,
    Out
}
