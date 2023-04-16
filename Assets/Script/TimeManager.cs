using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ʱ�������
public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    [SerializeField] Light sunLight;

    public float dayTime;         //�����ʱ��
    public float dayToNightTime;  //���쵽ҹ���ʱ��
    public float nightTime;       //ҹ���ʱ��
    public float nightToDayTime;  //ҹ�������ʱ��

    private float lightValue = 1;
    private int dayNum=0;
    [SerializeField] Image timeStateImg;
    [SerializeField] Text dayNumText;
    [SerializeField] Sprite[] dayStateSprites; //�����ҹ���ͼƬ

    private bool isDay = true;

    public bool IsDay
    {
        get { return isDay; }
        set 
        { 
            isDay = value;
            if (isDay)
            {
                dayNum += 1;
                dayNumText.text = "Day " + dayNum;
                timeStateImg.sprite = dayStateSprites[0];
            }
            else
            {
                timeStateImg.sprite = dayStateSprites[1];
            }
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        IsDay= true;
        //����ʱ��
        StartCoroutine(UpdateTime());
    }

    private IEnumerator UpdateTime()
    {
        while (true)
        {
            yield return null;
            //ͣһ֡�жϵ�ǰ����ʱ
            if (IsDay)
            {
                lightValue-=1/dayToNightTime*Time.deltaTime;
                SetLightValue(lightValue);
                if (lightValue<=0)
                {
                    IsDay = false;
                    yield return new WaitForSeconds(nightTime);//�ȴ�ҹ���ȥ
                }
            }
            //ҹ��ʱ
            else
            {
                lightValue += 1 / nightToDayTime * Time.deltaTime;
                SetLightValue(lightValue);
                if (lightValue >= 1)
                {
                    IsDay = true;
                    yield return new WaitForSeconds(dayTime);//�ȴ������ȥ
                }
            }
        }
    }

    //���õƹ��ֵ
    private void SetLightValue(float value)
    {
        RenderSettings.ambientIntensity= value;//Windows->Rendering->Lighting->Environment
        sunLight.intensity= value;
    }
}
