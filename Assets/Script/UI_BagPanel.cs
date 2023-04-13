using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UI背包面板
public class UI_BagPanel : MonoBehaviour
{
    public static UI_BagPanel instance;
    public UI_BagPanelItem[] items;
    [SerializeField] GameObject itemPrefab;

    [SerializeField]
    private void Awake()
    {
        instance = this;
        //先生成篝火
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

    //添加物品
    public bool AddItem(ItemType itemType)
    {
        //查看一次，有没有空格子
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
