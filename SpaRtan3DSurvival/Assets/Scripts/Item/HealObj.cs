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

    public string GetInteractPrompt()
    {
        return $"{name}\n{desc}";
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