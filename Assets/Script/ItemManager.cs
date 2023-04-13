using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��Ʒ����
public enum ItemType
{
    None,
    Meat,
    CookedMeat,
    Wood,
    Campfire
}
//��Ʒ����
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

//��Ʒ������

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    [SerializeField] GameObject[] itemPrefabs;
    [SerializeField] Sprite[] icons;


    private void Awake()
    {
        Instance = this;
    }

    //��ȡ��Ʒ����
    public ItemDefine GetItemDefine(ItemType itemType)
    {
        return new ItemDefine(itemType, icons[(int)itemType-1], itemPrefabs[(int)itemType-1]);
        //(int)ǿתΪint��ö��������ֵ
    }
}
