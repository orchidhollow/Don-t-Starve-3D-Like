using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyState
{
    Idle,        //待机
    Move,        //移动
    Pursue,      //追击
    Attack,      //攻击
    Hurt,        //受伤
    Die          //死亡
}

//野猪AI

public class Pig_Controller : ObjectBase
{
    [SerializeField] Animator Animator;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] CheckCollider checkCollider;

    //行动范围
    public float maxX = 4.74f;
    public float minX = -5.62f;
    public float maxZ = 5.92f;
    public float minZ = -6.33f;

    private EnemyState enemyState;

    private Vector3 targetPos;

    //当状态修改时，进入进状态要做的事情
    public EnemyState EnemyState//调用,改变时触发
    {
        get => enemyState;

        set
        {
            enemyState = value;
            switch (enemyState)
            {
                case EnemyState.Idle:
                    //播放动画
                    //关闭导航
                    //休息一段时间之后去巡逻
                    Animator.CrossFadeInFixedTime("Idle", 0.25f);//过渡播放动画：动画名称，过渡时间
                    navMeshAgent.enabled= false;
                    Invoke(nameof(GoMove), Random.Range(3f, 10f));//过一段时间调用：方法，时间
                    break;
                case EnemyState.Move:
                    //播放动画
                    //开启导航
                    //获取巡逻点
                    //移动到目标位置
                    Animator.CrossFadeInFixedTime("Move", 0.25f);
                    navMeshAgent.enabled = true;
                    targetPos = GetTargetPos();
                    navMeshAgent.SetDestination(targetPos);
                    break;
                case EnemyState.Pursue:
                    Animator.CrossFadeInFixedTime("Move", 0.25f);
                    navMeshAgent.enabled = true;
                    break;
                case EnemyState.Attack:
                    Animator.CrossFadeInFixedTime("Attack", 0.25f);
                    transform.LookAt(PlayerController.instance.transform.position);
                    navMeshAgent.enabled = false;
                    break;
                case EnemyState.Hurt:
                    Animator.CrossFadeInFixedTime("Hurt", 0.25f);
                    PlayAudio(0);
                    navMeshAgent.enabled = false;
                    break;
                case EnemyState.Die:
                    Animator.CrossFadeInFixedTime("Die", 0.25f);
                    PlayAudio(0);
                    navMeshAgent.enabled = false;
                    break;
            }
        }
    }

    private void Start()
    {
        EnemyState =EnemyState.Idle;
        checkCollider.Init(this, 10);
    }
    private void Update()
    {
        StateOnUpdate();
    }

    private void StateOnUpdate()//一直检测，一直运行
    {
        switch (enemyState)
        {
            //非一直检测的可以删除
            //case EnemyState.Idle:
            //    break;
            case EnemyState.Move:
                if(Vector3.Distance(transform.position,targetPos)<1.5f)
                {
                    EnemyState= EnemyState.Idle;
                }
                break;
            case EnemyState.Pursue:
                //距离足够近，攻击
                if (Vector3.Distance(transform.position,PlayerController.instance.transform.position)<1)
                {
                    EnemyState = EnemyState.Attack;
                }
                else
                {
                    //距离遥远，继续追
                    navMeshAgent.SetDestination(PlayerController.instance.transform.position);
                }
                break;
            //case EnemyState.Attack:
            //    break;
            //case EnemyState.Hurt:
            //    break;
            //case EnemyState.Die:
            //    break;
        }
    }

    private void GoMove()
    {
        EnemyState = EnemyState.Move;
    }

    //获得一个范围内随机点
    private Vector3 GetTargetPos()
    {
        return new Vector3 (Random.Range(minX ,maxX),0,Random.Range(minZ,maxZ));
    }

    public override void Hurt(int damage)
    {
        if (EnemyState==EnemyState.Die) return;
        CancelInvoke(nameof(GoMove));//取消切换到移动状态的延迟调用
        base.Hurt(damage);
        if (Hp > 0)
        {
            EnemyState = EnemyState.Hurt;
        }
    }

    protected override void Dead()
    {
        base.Dead();
        EnemyState = EnemyState.Die;
    }

    #region 动画事件
    private void StartHit()
    {
        checkCollider.StarHit();
    }

    private void StopHit()
    {
        checkCollider.StopHit();
    }

    private void StopAttack()
    {
        if (EnemyState != EnemyState.Die)
        {
            EnemyState = EnemyState.Pursue;
        }
    }

    private void HurtOver()
    {
        if (EnemyState!=EnemyState.Die)
        {
            EnemyState = EnemyState.Pursue;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    #endregion
}
