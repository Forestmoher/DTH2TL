using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot_UI : MonoBehaviour
{
    [SerializeField] private Image _itemSprite;
    [SerializeField] private TextMeshProUGUI _itemCount;
    [SerializeField] private InventorySlot _assignedInventorySlot;

    private Button _button;

    public InventorySlot AssignedInventorySlot => _assignedInventorySlot;
    public InventoryDisplay parentDisplay {get; private set;}
    private void Awake()
    {
        ClearSlot();
        _button = GetComponent<Button>();
        _button?.onClick.AddListener(OnUISlotClick);

        parentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }

    public void ClearSlot()
    {
        _assignedInventorySlot?.ClearSlot();

        _itemSprite.sprite = null;
        _itemSprite.color = Color.clear;
        _itemCount.text = "";
    }

    public void Init(InventorySlot slot)
    {
        _assignedInventorySlot = slot;
        UpdateUISlot(slot);
    }

    public void UpdateUISlot(InventorySlot slot)
    {
        if(slot.ItemData != null)
        {
            _itemSprite.sprite = slot.ItemData.itemIcon;
            _itemSprite.color = Color.white;

            if (slot.StackSize > 1) _itemCount.text = slot.StackSize.ToString();
            else _itemCount.text = "";
        }
        else ClearSlot();

    }

    public void UpdateUISlot()
    {
        if (_assignedInventorySlot != null) UpdateUISlot(_assignedInventorySlot);
    }

    public void OnUISlotClick()
    {
        parentDisplay.SlotClicked(this);
    }
}
