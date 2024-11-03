using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary : BaseDictionary<GameObject>
{
    private void Awake()
    {
        GameManager.Instance.itemDict = this;
    }
}
