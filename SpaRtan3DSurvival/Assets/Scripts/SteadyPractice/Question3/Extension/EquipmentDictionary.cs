using UnityEngine;

public class EquipmentDictionary : BaseDictionary<GameObject>
{
    private void Awake()
    {
        GameManager.Instance.equipDict = this;
    }
}