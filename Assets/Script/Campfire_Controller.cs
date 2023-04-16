using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//篝火管理器
public class Campfire_Controller : MonoBehaviour
{
    [SerializeField] Light light;
    private float time = 20;           //最大燃烧时间
    private float currentTime = 20;    //当前剩余燃烧时间

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

    //添加木头
    public void AddWood()
    {
        currentTime += 10;
        light.transform.parent.gameObject.SetActive(true);
    }
}
