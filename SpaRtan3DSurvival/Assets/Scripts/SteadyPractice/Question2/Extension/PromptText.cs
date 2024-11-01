using TMPro;
using UnityEngine;

public class PromptText : MonoBehaviour
{
    private void Awake()
    {
        CharacterManager.Instance.player.interaction.promptText = gameObject.GetComponent<TextMeshProUGUI>();
    }
}