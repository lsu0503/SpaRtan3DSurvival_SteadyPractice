using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentDictionaryConstructor : DictionaryConstructor<GameObject, EquipmentDictionary>
{
    [Serializable]
    private struct EquipmentDictionaryContentCell
    {
        public ItemData data;
        public GameObject prefab;

        public EquipmentDictionaryContentCell(ItemData _data, GameObject _prefab)
        {
            data = _data;
            prefab = _prefab;
        }
    }

    [SerializeField] private EquipmentDictionaryContentCell[] itemList;

    private void Awake()
    {
        contentCells = new List<DictionaryContentCell>();

        foreach (EquipmentDictionaryContentCell cell in itemList)
            contentCells.Add(new DictionaryContentCell(cell.data.id, cell.prefab));
    }
}