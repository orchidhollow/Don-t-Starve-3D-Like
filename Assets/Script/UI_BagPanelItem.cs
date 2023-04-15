using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;//�¼�ϵͳ

//�̳������������˳��Ľӿ�
public class UI_BagPanelItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]  Image bg;//����ɫ
    [SerializeField]  Image iconIgm;//ͼ��

    public ItemDefine itemDefine;

    private bool isSelect = false;

    public bool IsSelect 
    { 
        get => isSelect;
        set
        {
            isSelect = value;
            if (isSelect )
            {
                bg.color= Color.green;
            }
            else
            {
                bg.color= Color.white;
            }
        }
    }

    //������ʱ����
    public void OnPointerEnter(PointerEventData eventData)
    {
        IsSelect=true;
    }

    //����˳�ʱ����
    public void OnPointerExit(PointerEventData eventData)
    {
        IsSelect=false;
    }

    //��ʼ���������һ��null�������൱�ڿո��ӵ��߼�
    public void Init(ItemDefine itemDefine=null)
    {
        this.itemDefine = itemDefine;
        isSelect = false;
        if (this.itemDefine==null)
        {
            iconIgm.gameObject.SetActive(false);
        }
        else
        {
            iconIgm.gameObject.SetActive(true);
            iconIgm.sprite = itemDefine.Icon;
        }
    }
}
