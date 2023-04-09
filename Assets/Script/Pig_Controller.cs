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

    //当状态修改时，进入进状态要做的事情
    public EnemyState EnemyState
    {
        get => enemyState;

        set
        {
            enemyState = value;
            switch (enemyState)
            {
                case EnemyState.Idle:
                    break;
                case EnemyState.Move:
                    break;
                case EnemyState.Pursue:
                    break;
                case EnemyState.Attack:
                    break;
                case EnemyState.Hurt:
                    break;
                case EnemyState.Die:
                    break;
                default:
                    break;
            }
        }
        
    }
}
