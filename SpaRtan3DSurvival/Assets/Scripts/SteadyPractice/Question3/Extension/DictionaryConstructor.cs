using System;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryConstructor<ContentT, DictionaryT> : MonoBehaviour where DictionaryT : BaseDictionary<ContentT>
{
    [Serializable]
    protected struct DictionaryContentCell
    {
        public int key;
        public ContentT content;

        public DictionaryContentCell(int _key, ContentT _content)
        {
            key = _key;
            content = _content;
        }
    }

    [SerializeField] protected List<DictionaryContentCell> contentCells;

    private void Start()
    {
        DictionaryT dictionary = GetComponent<DictionaryT>();

        if(dictionary == null)
            dictionary = gameObject.AddComponent<DictionaryT>();

        foreach (DictionaryContentCell contentCell in contentCells)
            dictionary.AddDict(contentCell.key, contentCell.content);

        Destroy(this);
    }
}
