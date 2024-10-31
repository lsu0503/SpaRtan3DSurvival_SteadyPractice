using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;
    public Condition hunger;
    public Condition stamina;
    public Condition dash; // Q2 - 확장문제 추가 내용

    private void Start()
    {
        CharacterManager.Instance.Player.condition.uiCondition = this;
    }
}
