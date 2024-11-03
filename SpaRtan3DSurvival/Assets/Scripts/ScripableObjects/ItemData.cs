using System;
using System.Security;
using UnityEngine;

public enum ITEMTYPE
{
    EQUIPABLE,
    CONSUMABLE,
    RESOURCE
}

public enum CONSUMABLETYPE
{
    Health,
    Hunger
}

[Serializable]
public class ItemDataConsumable
{
    public CONSUMABLETYPE type;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName ="New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public int id; // Q3 확장 문제 - 2 추가 항목
    public string displayName;
    public string description;
    public ITEMTYPE type;
    public Sprite icon;
    //public GameObject dropPrefab; // Q3 확장 문제 - 2 수정 항목

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable[] consumable;

    // Q3 확장 문제 - 2 수정 항목
    //[Header("Equip")]
    //public GameObject equipPrefab;

    public int Id { get { return id; } } // Q3 확장 문제 - 2 추가 항목
}
