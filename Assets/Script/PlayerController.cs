using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : ObjectBase
{
    public static PlayerController instance;
    [SerializeField] Animator animator;
    [SerializeField] CharacterController characterController;

    public float M_speed = 1;
    public float R_speed = 10;//��ת�ٶ�
    Quaternion targetDirQuaternion;//��Ԫ����ת��

    private bool isAttacking=false;//����״̬
    private float hungry = 100;

    public float Hungry { get => hungry;
        set {
            hungry= value;
            if (hungry <= 0)
            {
                hungry = 0;
                //˥��HP
                Hp -= Time.deltaTime / 2;
            }
            //���¼���ֵͼƬ
            hungryImage.fillAmount = hungry / 100;
        }
    }


    [SerializeField] Image hpImage;
    [SerializeField] Image hungryImage;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        UpdateHungry();
        if (!isAttacking)
        {
            Move();
            Attack();
        }
        else//����������
        {
            transform.localRotation=Quaternion.Slerp(transform.localRotation,targetDirQuaternion,Time.deltaTime*R_speed);
        }
        
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

    void Attack()
    {
        //��⹥������
        if(Input.GetMouseButton(0))
        {            
            //���߼�⣬��������ͷ����귢�����ߣ�������Ϣ�����������ͼ���
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out RaycastHit hitInfo,10,LayerMask.GetMask("Ground")))
            {
                //˵����������
                animator.SetTrigger("Attack");
                //���빥��״̬
                isAttacking = true;
                //ת�򵽹�����
                targetDirQuaternion=Quaternion.LookRotation(hitInfo.point- transform.position);
            }
        }
    }


    private void UpdateHungry()
    {
        //˥������ֵ
        Hungry -= Time.deltaTime * 3;       
    }
    protected override void OnHpUpdate()
    {
        //����Ѫ��
        hpImage.fillAmount = Hp / 100;
    }

    #region �����¼�
    private void StartHit()
    {
        //������Ч
        PlayAudio(0);
        //�������
    }

    private void StopHit()
    {
        //ֹͣ�������
        isAttacking=false;
        
    }

    #endregion
}
