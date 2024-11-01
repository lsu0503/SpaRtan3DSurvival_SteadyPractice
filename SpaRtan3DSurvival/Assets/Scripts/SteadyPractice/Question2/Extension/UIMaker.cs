using UnityEngine;

public class UIMaker : MonoBehaviour
{
    [SerializeField] private GameObject[] uiPrefabs;

    private void Start()
    {
        foreach(GameObject ui in uiPrefabs)
        {
            Instantiate(ui, parent: gameObject.transform);
        }
    }
}
