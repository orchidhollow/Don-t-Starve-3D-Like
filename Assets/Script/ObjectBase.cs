using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//���е�ͼ��λ�Ļ���
public class ObjectBase :MonoBehaviour
{
    [SerializeField] float hp;//Ѫ��
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> audioClips;
    public GameObject lootObject;//������Ʒ

    //HP�����ԣ���hp�޸�ʱ�Զ��ж�����/hp�����߼�
    public float Hp
    {
        get => hp;
        set
        {
            hp = value;
            //�������
            if(hp<=0)
            {
                hp= 0;
                Dead();
            }
            OnHpUpdate();//�Զ�����HP�����߼�
        }
     }

    public void PlayAudio(int index)
    {
        audioSource.PlayOneShot(audioClips[index]);//����һ��
    }


    //��HP�仯ʱ
    protected virtual void OnHpUpdate() { }
    //����
    protected virtual void Dead()
    {
        if(lootObject!= null)
        {
            Instantiate(lootObject,
                transform.position+new Vector3(Random.Range(-0.5f, 0.5f),Random.Range(1f,1.5f),Random.Range(-0.5f,0.5f)),
                Quaternion.identity);
        }
    }
    //����
    public virtual void Hurt(int damage)
    {
        Hp -= damage;
    }
}
