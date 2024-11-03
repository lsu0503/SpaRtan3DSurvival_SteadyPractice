﻿using UnityEngine;

public class Resource: MonoBehaviour
{
    public ItemData itemToGive;
    public int quantityPerHit = 1;
    public int capacity;

    public void Gather(Vector3 hitPoint, Vector3 hitNormal)
    {
        for(int i = 0; i < quantityPerHit; i++)
        {
            if (capacity <= 0) break;
            capacity--;

            Instantiate(GameManager.Instance.itemDict.GetDict(itemToGive.Id), hitPoint + Vector3.up, Quaternion.LookRotation(hitNormal, Vector3.up));
        }
    }
}