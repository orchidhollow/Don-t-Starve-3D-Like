using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//物品类型
public enum ItemType
{
    None,
    Meat,
    CookedMeat,
    Wood,
    Campfire
}
//物品定义
public class ItemDefine
{
    public ItemType ItemType;
    public Sprite Icon;
    public GameObject Prefab;

    public ItemDefine(ItemType itemType, Sprite icon, GameObject prefab)
    {
        ItemType = itemType;
        Icon = icon;
        Prefab = prefab;
    }
}

//物品管理器

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    [SerializeField] GameObject[] itemPrefabs;
    [SerializeField] Sprite[] icons;


    private void Awake()
    {
        Instance = this;
    }

    //获取物品定义
    public ItemDefine GetItemDefine(ItemType itemType)
    {
        return new ItemDefine(itemType, icons[(int)itemType-1], itemPrefabs[(int)itemType-1]);
        //(int)强转为int，枚举有序列值
    }
}
