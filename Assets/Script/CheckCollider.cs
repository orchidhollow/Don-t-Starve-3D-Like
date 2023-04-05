using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ר������������ײ��
public class CheckCollider : MonoBehaviour
{
    private ObjectBase owner;
    private int damage;
    private bool canAttack=false;

    //���˱�ǩ
    [SerializeField] List<string> enemyTags= new List<string>();
    public void Init(ObjectBase owner,int damage)
    {
        this.damage = damage;
        this.owner = owner;
    }

    //�����˺����
    public void StarHit()
    {
        canAttack= true;
    }

    //�ر��˺����
    public void StopHit()
    {
        canAttack= false;
        lastAttackObjectList.Clear();
    }

    private List<GameObject> lastAttackObjectList= new List<GameObject>();
    private void OnTriggerStay(Collider other)
    {
        //�����ǰ�����˺����
        if(canAttack)
        {
            //�˴��˺���û���������λ && ���˵�tag���б���
            if(!lastAttackObjectList.Contains(other.gameObject)&&enemyTags.Contains(other.tag))
            {
                lastAttackObjectList.Add(other.gameObject);
                //�˺�
                other.GetComponent<ObjectBase>().Hurt(damage);
            }
        }
    }
}
