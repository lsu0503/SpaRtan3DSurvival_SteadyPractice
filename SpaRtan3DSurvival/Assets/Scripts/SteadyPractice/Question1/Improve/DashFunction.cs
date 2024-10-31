using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashFunction : MonoBehaviour
{
    private PlayerController controller;
    private PlayerCondition condition;

    [SerializeField] private float DashSpeed;
    [SerializeField] private float DashDuration;
    [SerializeField] private float DashCost;

    private Vector3 DashDirection = Vector3.zero;
    private float DashTriggeredTime = 0.0f;
    private bool isDash = false;

    private void Start()
    {
        controller = CharacterManager.Instance.player.controller;
        condition = CharacterManager.Instance.player.condition;

        DashDirection = Vector3.zero;
        DashTriggeredTime = 0.0f;
        isDash = false;
    }

    private void FixedUpdate()
    {
        if (isDash)
        {
            Dash(Time.deltaTime);

            if (Time.time - DashTriggeredTime >= DashDuration)
            {
                isDash = false;
                DashTriggeredTime = 0.0f;
                DashDirection = Vector3.zero;
            }
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            if (condition.UseDash(DashCost))  // Q2 - 확장문제 수정 내용 [UseStamina → UseDash]
            {
                isDash = true;
                DashTriggeredTime = Time.time;
                DashDirection = transform.forward;
            }
        }
    }

    private void Dash(float timePassed)
    {
        transform.position += DashDirection * DashSpeed * timePassed;
    }
}