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
    public string displayName;
    public string description;
    public ITEMTYPE type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable[] consumable;

    [Header("Equip")]
    public GameObject equipPrefab;
}
