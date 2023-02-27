using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


/// <summary>
/// Handles item currently in hotbar 
/// Triggers Equipment script 
/// Triggers item equip animation
/// </summary>
public class EquipmentController : MonoBehaviour
{

    [Header("Flashlight")]
    [SerializeField] public Transform flashlightPosition;

    [Header("Hotbar")]
    [SerializeField] private InventorySlot_UI[] _inventorySlots;
    [SerializeField] private Color _equipedSlotColor;

    [HideInInspector] public CharacterController characterController;
    private Animator _animator;
    private PlayerInput _playerInput;
    private InputAction _hotbarOneAction;
    private InputAction _hotbarTwoAction;
    private InputAction _hotbarThreeAction;
    private InputAction _hotbarFourAction;
    public Equipment currentItem;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        characterController = GetComponent<CharacterController>();
        _playerInput = GetComponentInChildren<PlayerInput>();
        _animator = GetComponentInChildren<Animator>();
        _hotbarOneAction = _playerInput.actions["Hotbar1"];
        _hotbarTwoAction = _playerInput.actions["Hotbar2"];
        _hotbarThreeAction = _playerInput.actions["Hotbar3"];
        _hotbarFourAction = _playerInput.actions["Hotbar4"];
    }

    void Update()
    {
        if (_hotbarOneAction.triggered)
        {
            HandleHotBarAction(0);
        }
        if (_hotbarTwoAction.triggered)
        {
            HandleHotBarAction(1);
        }
        if (_hotbarThreeAction.triggered)
        {
            HandleHotBarAction(2);
        }
        if (_hotbarFourAction.triggered)
        {
            HandleHotBarAction(3);
        }
    }

    private void HandleHotBarAction(int hotBarIndex)
    {
        //check if slot has item, and get the item data
        var thisItem = GetSelectedHotbarItem(hotBarIndex);
        if (thisItem == null) Debug.Log("No item in this slot");
        else
        {
            Debug.Log(thisItem.name);
            //if current item is not null, and same as item clicked
            if (currentItem != null && currentItem.itemName == thisItem.itemName)
            {
                currentItem.Use();
            }
            else
            {
                //create the item prefab
                var item = Instantiate(thisItem.itemPrefab, gameObject.transform);
                //set it to currently equiped item
                currentItem = item.GetComponent<Equipment>();
                //initialize and use item
                currentItem.Init(this);
                currentItem.Use();
            }
        }
    }

    private InventoryItemData GetSelectedHotbarItem(int index)
    {
        return (_inventorySlots[index].AssignedInventorySlot.ItemData);
    }

    public void PlayFlighlightAnimation(bool equipped)
    {
        _animator.SetBool("Flashlight", equipped);
    }
}
