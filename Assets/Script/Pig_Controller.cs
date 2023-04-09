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

    //��״̬�޸�ʱ�������״̬Ҫ��������
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
