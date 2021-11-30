using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// J : 프로그램이 끝나도 저장되어야 하는 게임설정 데이터
[Serializable]  // J : 직렬화된 Data
public class SettingData
{
    public float BGMSound = 1;    // J : 소리 on/off
    public int firstGame = 1;   // J : 첫 게임 여부
}