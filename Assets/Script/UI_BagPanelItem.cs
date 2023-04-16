using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;//事件系统

//继承鼠标进入和鼠标退出的接口
public class UI_BagPanelItem :
    MonoBehaviour,
    IPointerEnterHandler,   //鼠标进入
    IPointerExitHandler ,   //鼠标退出
    IBeginDragHandler,      //开始拖拽
    IDragHandler,           //拖拽中
    IEndDragHandler         //拖拽结束
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

    private void Update()
    {
        if(isSelect&&itemDefine!=null&&Input.GetMouseButtonDown(1))
        {
            if(PlayerController.instance.UseItem(itemDefine.ItemType))
            {
                Init(null);
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
    
    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemDefine == null) return;
        PlayerController.instance.isDarging= true;
    }

    //拖拽中
    public void OnDrag(PointerEventData eventData)
    {
        if (itemDefine == null) return;
        iconIgm.transform.position=eventData.position;
    }

    //拖拽结束
    public void OnEndDrag(PointerEventData eventData)
    {
        if (itemDefine == null) return;
        PlayerController.instance.isDarging = false;
        //发射射线查看当前碰到的物体
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out RaycastHit hitInfo))
        {
            string targetTag = hitInfo.collider.tag;
            iconIgm.transform.localPosition = Vector3.zero;//Icon归位

            //根据自身类型 和 目标类型 做逻辑判断
            switch (itemDefine.ItemType)
            {
                case ItemType.Meat://肉
                    if (targetTag=="Ground")//遇到地板
                    {
                        Instantiate(itemDefine.Prefab, hitInfo.point + new Vector3(0, 1f, 0), Quaternion.identity);
                        Init(null);
                    }
                    else if (targetTag == "Campfire")//遇到篝火
                    {
                        Init(ItemManager.Instance.GetItemDefine(ItemType.CookedMeat));
                    }
                    break;
                case ItemType.CookedMeat://烤过的肉
                    if (targetTag == "Ground")//遇到地板
                    {
                        Instantiate(itemDefine.Prefab, hitInfo.point + new Vector3(0, 1f, 0), Quaternion.identity);
                        Init(null);
                    }
                    else if (targetTag == "Campfire")//遇到篝火
                    {
                        hitInfo.collider.GetComponent<Campfire_Controller>().AddWood();
                        Init(null);
                    }
                    break;
                case ItemType.Wood://木头
                    if (targetTag == "Ground")//遇到地板
                    {
                        Instantiate(itemDefine.Prefab, hitInfo.point + new Vector3(0, 1f, 0), Quaternion.identity);
                        Init(null);
                    }
                    else if (targetTag=="Campfire")//遇到篝火
                    {
                        hitInfo.collider.GetComponent<Campfire_Controller>().AddWood();
                        Init(null);
                    }
                    break;
                case ItemType.Campfire://篝火
                    if (targetTag == "Ground")
                    {
                        Instantiate(itemDefine.Prefab, hitInfo.point , Quaternion.identity);
                        Init(null);
                    }
                    break;
            }
        }
    }
}
