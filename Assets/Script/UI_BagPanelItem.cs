using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;//�¼�ϵͳ

//�̳������������˳��Ľӿ�
public class UI_BagPanelItem :
    MonoBehaviour,
    IPointerEnterHandler,   //������
    IPointerExitHandler ,   //����˳�
    IBeginDragHandler,      //��ʼ��ק
    IDragHandler,           //��ק��
    IEndDragHandler         //��ק����
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
    
    //��ʼ��ק
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemDefine == null) return;
        PlayerController.instance.isDarging= true;
    }

    //��ק��
    public void OnDrag(PointerEventData eventData)
    {
        if (itemDefine == null) return;
        iconIgm.transform.position=eventData.position;
    }

    //��ק����
    public void OnEndDrag(PointerEventData eventData)
    {
        if (itemDefine == null) return;
        PlayerController.instance.isDarging = false;
        //�������߲鿴��ǰ����������
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out RaycastHit hitInfo))
        {
            string targetTag = hitInfo.collider.tag;
            iconIgm.transform.localPosition = Vector3.zero;//Icon��λ

            //������������ �� Ŀ������ ���߼��ж�
            switch (itemDefine.ItemType)
            {
                case ItemType.Meat://��
                    if (targetTag=="Ground")//�����ذ�
                    {
                        Instantiate(itemDefine.Prefab, hitInfo.point + new Vector3(0, 1f, 0), Quaternion.identity);
                        Init(null);
                    }
                    else if (targetTag == "Campfire")//��������
                    {
                        Init(ItemManager.Instance.GetItemDefine(ItemType.CookedMeat));
                    }
                    break;
                case ItemType.CookedMeat://��������
                    if (targetTag == "Ground")//�����ذ�
                    {
                        Instantiate(itemDefine.Prefab, hitInfo.point + new Vector3(0, 1f, 0), Quaternion.identity);
                        Init(null);
                    }
                    else if (targetTag == "Campfire")//��������
                    {
                        hitInfo.collider.GetComponent<Campfire_Controller>().AddWood();
                        Init(null);
                    }
                    break;
                case ItemType.Wood://ľͷ
                    if (targetTag == "Ground")//�����ذ�
                    {
                        Instantiate(itemDefine.Prefab, hitInfo.point + new Vector3(0, 1f, 0), Quaternion.identity);
                        Init(null);
                    }
                    else if (targetTag=="Campfire")//��������
                    {
                        hitInfo.collider.GetComponent<Campfire_Controller>().AddWood();
                        Init(null);
                    }
                    break;
                case ItemType.Campfire://����
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
