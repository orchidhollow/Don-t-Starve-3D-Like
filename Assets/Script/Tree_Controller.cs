using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

//Ê÷Ä¾¿ØÖÆ
public class Tree_Controller : ObjectBase
{
    [SerializeField] Animator Animator;


    public override void Hurt(int damage)
    {
        base.Hurt(damage);
        Animator.SetTrigger("Hurt");
        PlayAudio(0);
    }

    protected override void Dead()
    {
        base.Dead();
        Destroy(gameObject);
    }
}
