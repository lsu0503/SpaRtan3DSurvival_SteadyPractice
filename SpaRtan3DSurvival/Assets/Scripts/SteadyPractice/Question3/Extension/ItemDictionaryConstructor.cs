using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemDictionaryConstructor : DictionaryConstructor<GameObject, ItemDictionary>
{
    [Serializable]
    private struct ItemDictionaryContentCell
    {
        public ItemData data;
        public GameObject prefab;

        public ItemDictionaryContentCell(ItemData _data, GameObject _prefab)
        {
            data = _data;
            prefab = _prefab;
        }
    }

    [SerializeField] private ItemDictionaryContentCell[] itemList;

    private void Awake()
    {
        contentCells = new List<DictionaryContentCell>();

        foreach (ItemDictionaryContentCell cell in itemList)
            contentCells.Add(new DictionaryContentCell(cell.data.id, cell.prefab));
    }
}
