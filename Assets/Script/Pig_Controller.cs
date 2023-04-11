using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyState
{
    Idle,        //����
    Move,        //�ƶ�
    Pursue,      //׷��
    Attack,      //����
    Hurt,        //����
    Die          //����
}

//Ұ��AI

public class Pig_Controller : ObjectBase
{
    [SerializeField] Animator Animator;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] CheckCollider checkCollider;

    //�ж���Χ
    public float maxX = 4.74f;
    public float minX = -5.62f;
    public float maxZ = 5.92f;
    public float minZ = -6.33f;

    private EnemyState enemyState;

    private Vector3 targetPos;

    //��״̬�޸�ʱ�������״̬Ҫ��������
    public EnemyState EnemyState//����,�ı�ʱ����
    {
        get => enemyState;

        set
        {
            enemyState = value;
            switch (enemyState)
            {
                case EnemyState.Idle:
                    //���Ŷ���
                    //�رյ���
                    //��Ϣһ��ʱ��֮��ȥѲ��
                    Animator.CrossFadeInFixedTime("Idle", 0.25f);//���ɲ��Ŷ������������ƣ�����ʱ��
                    navMeshAgent.enabled= false;
                    Invoke(nameof(GoMove), Random.Range(3f, 10f));//��һ��ʱ����ã�������ʱ��
                    break;
                case EnemyState.Move:
                    //���Ŷ���
                    //��������
                    //��ȡѲ�ߵ�
                    //�ƶ���Ŀ��λ��
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

    private void StateOnUpdate()//һֱ��⣬һֱ����
    {
        switch (enemyState)
        {
            //��һֱ���Ŀ���ɾ��
            //case EnemyState.Idle:
            //    break;
            case EnemyState.Move:
                if(Vector3.Distance(transform.position,targetPos)<1.5f)
                {
                    EnemyState= EnemyState.Idle;
                }
                break;
            case EnemyState.Pursue:
                //�����㹻��������
                if (Vector3.Distance(transform.position,PlayerController.instance.transform.position)<1)
                {
                    EnemyState = EnemyState.Attack;
                }
                else
                {
                    //����ңԶ������׷
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

    //���һ����Χ�������
    private Vector3 GetTargetPos()
    {
        return new Vector3 (Random.Range(minX ,maxX),0,Random.Range(minZ,maxZ));
    }

    public override void Hurt(int damage)
    {
        if (EnemyState==EnemyState.Die) return;
        CancelInvoke(nameof(GoMove));//ȡ���л����ƶ�״̬���ӳٵ���
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

    #region �����¼�
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
