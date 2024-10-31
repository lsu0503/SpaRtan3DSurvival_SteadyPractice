using System;
using UnityEngine;

public interface IDamageable
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamageable
{
    public UICondition uiCondition;
    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }
    Condition dash { get { return uiCondition.dash; } }  // Q2 - 확장문제 추가 내용

    public float noHungerHealthDecay;

    public event Action onTakeDamage;

    private void Update()
    {
        hunger.Substract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if(hunger.curValue == 0.0f)
        {
            health.Substract(noHungerHealthDecay * Time.deltaTime);
        }

        if(health.curValue == 0f)
        {
            Die();
        }

        dash.Add(dash.passiveValue * Time.deltaTime);
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    private void Die()
    {
        Debug.Log("사망");
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Substract(damage);
        onTakeDamage?.Invoke();
    }

    public bool UseStamina(float amount)
    {
        if (stamina.curValue < amount) return false;

        stamina.Substract(amount);
        return true;
    }

    public bool UseDash(float amount) // Q2 - 확장문제 추가 내용
    {
        if(dash.curValue < amount) return false;

        dash.Substract(amount);
        return true;
    }
}
