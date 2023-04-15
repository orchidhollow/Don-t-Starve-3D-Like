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
    }

    private void Start()
    {
        items =new UI_BagPanelItem[5];
        //先生成篝火
        //Instantiate第1参数为克隆源对象，第2为生成后的父对象
        //获取其身上的UI_BagPanelItem组件
        UI_BagPanelItem item = Instantiate(itemPrefab, transform).GetComponent<UI_BagPanelItem>();
        item.Init(ItemManager.Instance.GetItemDefine(ItemType.Campfire));
        items[0] = item;
        //其余格子为空白
        for (int i = 1; i < 5; i++)
        {
            item = Instantiate(itemPrefab, transform).GetComponent<UI_BagPanelItem>();
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
