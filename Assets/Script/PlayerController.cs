using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] Animator animator;
    [SerializeField] CharacterController characterController;

    public float M_speed = 1;
    public float R_speed = 10;//旋转速度
    Quaternion targetDirQuaternion;//四元数旋转量

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        Move();
    }

    void Move()//移动
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 forward = new Vector3(horizontal, 0, vertical).normalized;

        if (horizontal==0&&vertical==0)
        {
            animator.SetBool("Walk", false);
        }
        else
        {
            //动画
            animator.SetBool("Walk", true);
            //旋转
            targetDirQuaternion=Quaternion.LookRotation(forward);//将向量组转换成一个四元数旋转量
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetDirQuaternion, Time.deltaTime* R_speed);//避免突变，使转向渐变
            //实际移动
            characterController.SimpleMove(forward * M_speed);
        }
    }
}
