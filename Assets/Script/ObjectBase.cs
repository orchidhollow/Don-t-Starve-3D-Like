using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//所有地图单位的基类
public class ObjectBase :MonoBehaviour
{
    [SerializeField] float hp;//血量
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> audioClips;
    public GameObject lootObject;//掉落物品

    //HP的属性，当hp修改时自动判断死亡/hp更新逻辑
    public float Hp
    {
        get => hp;
        set
        {
            hp = value;
            //检测死亡
            if(hp<=0)
            {
                hp= 0;
                Dead();
            }
            OnHpUpdate();//自动调用HP更新逻辑
        }
     }

    public void PlayAudio(int index)
    {
        audioSource.PlayOneShot(audioClips[index]);//播放一次
    }


    //当HP变化时
    protected virtual void OnHpUpdate() { }
    //死亡
    protected virtual void Dead()
    {
        if(lootObject!= null)
        {
            Instantiate(lootObject,
                transform.position+new Vector3(Random.Range(-0.5f, 0.5f),Random.Range(1f,1.5f),Random.Range(-0.5f,0.5f)),
                Quaternion.identity);
        }
    }
    //受伤
    public virtual void Hurt(int damage)
    {
        Hp -= damage;
    }
}
