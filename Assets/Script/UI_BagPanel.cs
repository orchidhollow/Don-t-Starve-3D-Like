using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UI�������
public class UI_BagPanel : MonoBehaviour
{
    public static UI_BagPanel instance;
    public UI_BagPanelItem[] items;
    [SerializeField] GameObject itemPrefab;

    [SerializeField]
    private void Awake()
    {
        instance = this;
        //����������
        UI_BagPanelItem item = Instantiate(itemPrefab, transform).GetComponent<UI_BagPanelItem>();
        item.Init(ItemManager.Instance.GetItemDefine(ItemType.Campfire));
        items[0] = item;

        for (int i = 1; i < 5; i++)
        {
            item=Instantiate(itemPrefab,transform).GetComponent<UI_BagPanelItem>();
            item.Init(null);
            items[i] = item;
        }
    }

    //�����Ʒ
    public bool AddItem(ItemType itemType)
    {
        //�鿴һ�Σ���û�пո���
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemDefine==null)
            {
                ItemDefine itemDefine = ItemManager.Instance.GetItemDefine(itemType);
                items[i].Init(itemDefine);
                return true;
            }
        }
        return false;
    }
}
