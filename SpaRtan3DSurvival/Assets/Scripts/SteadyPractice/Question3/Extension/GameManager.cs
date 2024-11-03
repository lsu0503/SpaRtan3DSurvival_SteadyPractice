using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : ManagerBase<GameManager>
{
    public ItemDictionary itemDict;
    public EquipmentDictionary equipDict;

    // Q3 개선 문제 추가 사항
    public TextMeshProUGUI promptText;

    public void SetPrompt(string text)
    {
        promptText.text = text;

        if(!promptText.gameObject.activeSelf)
            promptText.gameObject.SetActive(true);
    }

    public void OffPrompt()
    {
        if(promptText.gameObject.activeSelf)
            promptText.gameObject.SetActive(false);
    }
}