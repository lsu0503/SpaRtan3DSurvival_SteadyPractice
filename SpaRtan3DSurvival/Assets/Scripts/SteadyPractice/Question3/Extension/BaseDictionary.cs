using System.Collections.Generic;
using UnityEngine;

public class BaseDictionary<T> : MonoBehaviour
{
    protected Dictionary<int, T> dictionary = new Dictionary<int, T>();

    public void AddDict(int key, T value)
    {
        dictionary.Add(key, value);
    }

    public virtual T GetDict(int key)
    {
        return dictionary[key];
    }
}
