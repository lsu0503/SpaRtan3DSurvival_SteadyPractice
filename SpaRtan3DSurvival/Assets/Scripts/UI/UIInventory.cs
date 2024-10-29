using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;

    public GameObject inventoryWIndow;
    public Transform slotPanel;
    public Transform dropPosition;

    [Header("Select Item")]
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDesc;
    public TextMeshProUGUI selectedStatName;
    public TextMeshProUGUI selectedStatValue;

    [Header("Buttons")]
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unequipButton;
    public GameObject dropButton;

    private PlayerController controller;
    private PlayerCondition condition;

    ItemData selectedItem;
    int selectedItemIndex = 0;

    private int curEquipIdx;

    private void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;
        dropPosition = CharacterManager.Instance.Player.dropPosition;

        controller.inventory += ToggleInventoryDisplay;
        CharacterManager.Instance.player.addItem += AddItem;

        inventoryWIndow.SetActive(false);
        slots = new ItemSlot[slotPanel.childCount];

        for(int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }

        UpdateUI();
    }

    private void ClearSelectedItemWindow()
    {
        selectedItemName.text = string.Empty;
        selectedItemDesc.text = string.Empty;
        selectedStatName.text = string.Empty;
        selectedStatValue.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unequipButton.SetActive(false);
        dropButton.SetActive(false);
    }

    public void ToggleInventoryDisplay()
    {
        inventoryWIndow.SetActive(!inventoryWIndow.activeInHierarchy);
    }

    void AddItem()
    {
        ItemData data = CharacterManager.Instance.player.itemData;

        if (data.canStack)
        {
            ItemSlot slot = GetItemStack(data);

            if (slot != null)
            {
                slot.quantity++;
                UpdateUI();
                CharacterManager.Instance.Player.itemData = null;
                return;
            }
        }

        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = data;
            emptySlot.quantity = 1;
            UpdateUI();
        }

        else
        {
            ThrowItem(data);
        }

        CharacterManager.Instance.Player.itemData = null;
    }

    private void UpdateUI()
    {
        foreach(var slot in slots)
        {
            if (slot.item != null) slot.SetSlot();
            else slot.ClearSlot();
        }
    }

    private ItemSlot GetItemStack(ItemData data)
    {
        foreach(var slot in slots)
            if(slot.item == data && slot.quantity < data.maxStackAmount)
                return slot;

        return null;
    }

    private ItemSlot GetEmptySlot()
    {
        foreach(var slot in slots)
            if (slot.item == null)
                return slot;

        return null;
    }

    private void ThrowItem(ItemData data)
    {
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * UnityEngine.Random.value * 360));
    }

    public void SelectItem(int index)
    {
        if (slots[index].item == null) return;

        selectedItem = slots[index].item;
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.displayName;
        selectedItemDesc.text = selectedItem.description;
        selectedStatName.text = string.Empty;
        selectedStatValue.text = string.Empty;

        for (int i = 0; i < selectedItem.consumable.Length; i++)
        {
            selectedStatName.text += selectedItem.consumable[i].type.ToString() + "\n";
            selectedStatValue.text += selectedItem.consumable[i].value.ToString() + "\n";
        }

        useButton.SetActive(selectedItem.type == ITEMTYPE.CONSUMABLE);
        equipButton.SetActive(selectedItem.type == ITEMTYPE.EQUIPABLE && !slots[index].equipped);
        unequipButton.SetActive(selectedItem.type == ITEMTYPE.EQUIPABLE && slots[index].equipped);
        dropButton.SetActive(true);
    }

    public void OnUseButton()
    {
        if(selectedItem.type == ITEMTYPE.CONSUMABLE)
        {
            foreach(var effect in selectedItem.consumable)
            {
                switch (effect.type)
                {
                    case CONSUMABLETYPE.Health:
                        condition.Heal(effect.value);
                        break;

                    case CONSUMABLETYPE.Hunger:
                        condition.Eat(effect.value);
                        break;
                }
            }

            RemoveSelectedItem();
        }
    }

    public void OnDropButton()
    {
        ThrowItem(selectedItem);
        RemoveSelectedItem();
    }

    void RemoveSelectedItem()
    {
        if (curEquipIdx == selectedItemIndex)
            Unequip(curEquipIdx);

        slots[selectedItemIndex].quantity--;
        if(slots[selectedItemIndex].quantity <= 0)
        {
            selectedItem = null;
            slots[selectedItemIndex].item = null;
            selectedItemIndex = -1;
            ClearSelectedItemWindow();
        }

        UpdateUI();
    }

    public void OnEquipButton()
    {
        if (slots[curEquipIdx].equipped)
            Unequip(curEquipIdx);

        slots[selectedItemIndex].equipped = true;
        curEquipIdx = selectedItemIndex;
        CharacterManager.Instance.Player.equipment.EquipNew(selectedItem);

        UpdateUI();
        SelectItem(selectedItemIndex);
    }

    void Unequip(int index)
    {
        slots[index].equipped = false;
        CharacterManager.Instance.Player.equipment.Unequip();
        UpdateUI();

        if (selectedItemIndex == index)
            SelectItem(selectedItemIndex);
    }

    public void OnUnequipButton()
    {
        Unequip(selectedItemIndex);
    }
}