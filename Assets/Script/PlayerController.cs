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
    public float R_speed = 10;//��ת�ٶ�
    Quaternion targetDirQuaternion;//��Ԫ����ת��

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        Move();
    }

    void Move()//�ƶ�
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
            //����
            animator.SetBool("Walk", true);
            //��ת
            targetDirQuaternion=Quaternion.LookRotation(forward);//��������ת����һ����Ԫ����ת��
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetDirQuaternion, Time.deltaTime* R_speed);//����ͻ�䣬ʹת�򽥱�
            //ʵ���ƶ�
            characterController.SimpleMove(forward * M_speed);
        }
    }
}
