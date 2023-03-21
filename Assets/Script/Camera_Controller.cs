using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;             //��Ŀ��ƫ����
    [SerializeField] float transitionSpeed = 2;

    //������ƶ��������׷
    private void LateUpdate()
    {
        if (target != null) 
        {
            Vector3 targetPos = target.position + offset;
            //��A��Bƽ������
            transform.position =Vector3.Lerp(transform.position,targetPos,transitionSpeed*Time.deltaTime);
        }
    }
}
