using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//专门用来检测的碰撞体
public class CheckCollider : MonoBehaviour
{
    private ObjectBase owner;
    private int damage;
    private bool canAttack=false;

    //敌人标签
    [SerializeField] List<string> enemyTags= new List<string>();
    //可拾取标签
    [SerializeField] List<string> itemTags= new List<string>();
    public void Init(ObjectBase owner,int damage)
    {
        this.damage = damage;
        this.owner = owner;
    }

    //开启伤害检测
    public void StarHit()
    {
        canAttack= true;
    }

    //关闭伤害检测
    public void StopHit()
    {
        canAttack= false;
        lastAttackObjectList.Clear();
    }

    private List<GameObject> lastAttackObjectList= new List<GameObject>();
    private void OnTriggerStay(Collider other)
    {
        //如果当前允许伤害检测
        if(canAttack)
        {
            //此次伤害还没检测过这个单位 && 敌人的tag再列表中
            if(!lastAttackObjectList.Contains(other.gameObject)&&enemyTags.Contains(other.tag))
            {
                lastAttackObjectList.Add(other.gameObject);
                //伤害
                other.GetComponent<ObjectBase>().Hurt(damage);
            }
            return;
        }

        //检测拾取
        if(itemTags.Contains(other.tag))
        {            
            //把见到的东西的tag转为枚举
            ItemType itemType=System.Enum.Parse<ItemType>(other.tag);
            if (owner.AddItem(itemType))
            {
                owner.PlayAudio(1); //宿主播放音效
                Destroy(other.gameObject);
            }
        }
    }
}
