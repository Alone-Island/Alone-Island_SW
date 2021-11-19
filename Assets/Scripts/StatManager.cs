using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{

    public struct Stat
    {
        [SerializeField] private Image filledImage;
        private float currValue; // N : 현재 스탯
        private float currFill; // N : 현재 이미지 fill (Max : 1)
        public float maxValue { get; set; } // N : 최대 스탯

        public float CurrValue
        {
            get
            {
                return currValue;
            }
            set
            {
                if (value > maxValue) currValue = maxValue; // N : 최대 스탯을 초과하는 경우
                else if (value < 0) currValue = 0; // N : 0 미만일 경우
                else currValue = value;

                currFill = currValue / maxValue; // N : 현재 스탯에 따른 이미지 fill 수정
            }
        }

        // N : 스탯 초기화
        public void InitStat(float curr, float max)
        {
            maxValue = max;
            CurrValue = curr;
        }
    }

    [SerializeField]
    private float speed;

    Stat hungerStat; // N : 배고픔
    Stat happyStat; // N : 행복
    Stat temperatureStat; // N : 체온
    Stat dangerStat; // N : 위험

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
