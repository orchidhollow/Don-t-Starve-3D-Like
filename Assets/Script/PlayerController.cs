using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : ObjectBase
{
    public static PlayerController instance;
    [SerializeField] CheckCollider checkCollider;
    [SerializeField] Animator animator;
    [SerializeField] CharacterController characterController;

    public float M_speed = 1;
    public float R_speed = 10;//旋转速度
    Quaternion targetDirQuaternion;//四元数旋转量

    private bool isAttacking=false;//攻击状态
    private bool isHurting=false;//受伤状态
    private float hungry = 100;

    public float Hungry { get => hungry;
        set {
            hungry= value;
            if (hungry <= 0)
            {
                hungry = 0;
                //衰减HP
                Hp -= Time.deltaTime / 2;
            }
            //更新饥饿值图片
            hungryImage.fillAmount = hungry / 100;
        }
    }


    [SerializeField] Image hpImage;
    [SerializeField] Image hungryImage;
    private void Awake()
    {
        instance = this;
        //初始化伤害检测器
        checkCollider.Init(this, 30);
    }

    private void Update()
    {
        UpdateHungry();
        //即不再攻击中、也不在受伤中才能移动攻击
        if (!isAttacking&& !isHurting)
        {
            Move();
            Attack();
        }
        else//攻击过程中
        {
            transform.localRotation=Quaternion.Slerp(transform.localRotation,targetDirQuaternion,Time.deltaTime*R_speed);
        }
        
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

    void Attack()
    {
        //检测攻击按键
        if(Input.GetMouseButton(0))
        {            
            //射线检测，从主摄像头向鼠标发出射线，带回信息，检测最大距离和检测层
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out RaycastHit hitInfo,10,LayerMask.GetMask("Ground")))
            {
                //说明碰到地面
                animator.SetTrigger("Attack");
                //进入攻击状态
                isAttacking = true;
                //转向到攻击点
                targetDirQuaternion=Quaternion.LookRotation(hitInfo.point- transform.position);
            }
        }
    }


    private void UpdateHungry()
    {
        //衰减饥饿值
        Hungry -= Time.deltaTime * 3;       
    }
    protected override void OnHpUpdate()//角色的血条更新
    {
        //更新血条
        hpImage.fillAmount = Hp / 100;
    }

    public override void Hurt(int damage)
    {
        base.Hurt(damage);
        animator.SetTrigger("Hurt");
        PlayAudio(2);
        isHurting=true;
    }

    public override bool AddItem(ItemType itemType)
    {
        //检测背包能不能放下
        return UI_BagPanel.instance.AddItem(itemType);
    }

    #region 动画事件
    private void StartHit()
    {
        //播放音效
        PlayAudio(0);
        //攻击检测
        checkCollider.StarHit();
    }

    private void StopHit()
    {
        //停止攻击检测
        isAttacking=false;
        checkCollider.StopHit();
    }

    private void HurtOver()
    {
        isHurting = false;
    }

    #endregion
}
