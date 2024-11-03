using UnityEngine;

public interface IHeal
{
    public void HealPlayer();
}

public class HealObj : MonoBehaviour, IInteractable, IHeal
{
    [SerializeField] private string name;
    [TextArea, SerializeField] private string desc;
    [SerializeField] private float healAmount;
    private bool isChecking = false;

    // Q3 개선 문제 수정 사항
    //public string GetInteractPrompt()
    //{
    //    return $"{name}\n{desc}";
    //}

    // Q3 개선 문제 추가 사항
    public void OnAimed()
    {
        if (!isChecking)
        {
            isChecking = true;
            GameManager.Instance.SetPrompt($"{name}\n{desc}");
            CharacterManager.Instance.player.interaction.onInteractionEvent += OnInteract;
        }
    }

    public void OutAim()
    {
        if (isChecking)
        {
            isChecking = false;
            CharacterManager.Instance.player.interaction.onInteractionEvent -= OnInteract;
        }
    }

    public void HealPlayer()
    {
        CharacterManager.Instance.player.condition.Heal(healAmount);
    }

    public void OnInteract()
    {
        HealPlayer();
        Destroy(gameObject);
    }
}