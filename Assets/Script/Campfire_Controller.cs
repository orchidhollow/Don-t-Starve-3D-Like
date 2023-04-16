using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���������
public class Campfire_Controller : MonoBehaviour
{
    [SerializeField] Light light;
    private float time = 20;           //���ȼ��ʱ��
    private float currentTime = 20;    //��ǰʣ��ȼ��ʱ��

    private void Update()
    {
        if (currentTime<=0)
        {
            currentTime = 0;
            light.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            currentTime-=Time.deltaTime;
            light.intensity = Mathf.Clamp(currentTime / time, 0, 1) * 3+0.5f;
        }
    }

    //���ľͷ
    public void AddWood()
    {
        currentTime += 10;
        light.transform.parent.gameObject.SetActive(true);
    }
}
