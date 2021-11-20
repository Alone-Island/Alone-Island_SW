using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{

    public struct Stat
    {
        [SerializeField] private Image filledImage;
        private float currValue; // N : ���� ����
        private float currFill; // N : ���� �̹��� fill (Max : 1)
        public float maxValue { get; set; } // N : �ִ� ����

        public float CurrValue
        {
            get
            {
                return currValue;
            }
            set
            {
                if (value > maxValue) currValue = maxValue; // N : �ִ� ������ �ʰ��ϴ� ���
                else if (value < 0) currValue = 0; // N : 0 �̸��� ���
                else currValue = value;

                currFill = currValue / maxValue; // N : ���� ���ȿ� ���� �̹��� fill ����
            }
        }

        // N : ���� �ʱ�ȭ
        public void InitStat(float curr, float max)
        {
            maxValue = max;
            CurrValue = curr;
        }
    }

    [SerializeField]
    private float speed;

    Stat hungerStat; // N : �����
    Stat happyStat; // N : �ູ
    Stat temperatureStat; // N : ü��
    Stat dangerStat; // N : ����

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
