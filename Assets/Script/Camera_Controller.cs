using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;             //和目标偏移量
    [SerializeField] float transitionSpeed = 2;

    //玩家先移动，相机再追
    private void LateUpdate()
    {
        if (target != null) 
        {
            Vector3 targetPos = target.position + offset;
            //从A到B平滑过渡
            transform.position =Vector3.Lerp(transform.position,targetPos,transitionSpeed*Time.deltaTime);
        }
    }
}
