using TMPro;
using UnityEngine;

public class PromptText : MonoBehaviour
{
    private void Awake()
    {
        // Q3 개선 문제 추가 사항
        GameManager.Instance.promptText = gameObject.GetComponent<TextMeshProUGUI>();

        // Q3 개선 문제 수정 사항
        //CharacterManager.Instance.player.interaction.promptText = gameObject.GetComponent<TextMeshProUGUI>();

        Destroy(this);
    }
}