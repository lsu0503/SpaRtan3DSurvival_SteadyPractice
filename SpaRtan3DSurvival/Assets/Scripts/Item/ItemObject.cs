using UnityEngine;

public interface IInteractable
{
    // Q3 개선 문제 수정 사항
    //public string GetInteractPrompt();

    // Q3 개선 문제 추가 사항
    public void OnAimed();
    public void OutAim();

    public void OnInteract();
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData ItemData;
    private bool isChecking = false;

    // Q3 개선 문제 추가 사항
    public void OnAimed()
    {
        if (!isChecking)
        {
            isChecking = true;
            GameManager.Instance.SetPrompt($"{ItemData.displayName}\n{ItemData.description}");
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

    // Q3 개선 문제 수정 사항
    //public string GetInteractPrompt()
    //{
    //    string str = $"{ItemData.displayName}\n{ItemData.description}";
    //    return str;
    //}

    public void OnInteract()
    {
        CharacterManager.Instance.Player.itemData = ItemData;
        CharacterManager.Instance.Player.addItem?.Invoke();

        // Q3 개선 문제 추가 사항
        OutAim();
        GameManager.Instance.OffPrompt();

        Destroy(gameObject);
    }
}
