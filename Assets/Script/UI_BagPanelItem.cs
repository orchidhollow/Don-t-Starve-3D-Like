using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;//事件系统

//继承鼠标进入和鼠标退出的接口
public class UI_BagPanelItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]  Image bg;//背景色
    [SerializeField]  Image iconIgm;//图标

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

    //鼠标进入时调用
    public void OnPointerEnter(PointerEventData eventData)
    {
        IsSelect=true;
    }

    //鼠标退出时调用
    public void OnPointerExit(PointerEventData eventData)
    {
        IsSelect=false;
    }

    //初始化，如果传一个null过来，相当于空格子的逻辑
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
