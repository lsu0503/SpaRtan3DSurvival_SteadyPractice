using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;
    public Equipment equipment;
    public Interaction interaction; // Q3 Ȯ�� ���� �߰� ����

    public ItemData itemData;
    public Action addItem;

    public Transform dropPosition;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
        equipment = GetComponent<Equipment>();
        interaction = GetComponent<Interaction>(); // Q3 Ȯ�� ���� �߰� ����
    }
}
