using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InteractionTrigger : MonoBehaviour, IInteractable
{
    public InventoryItemData inventoryItemData;
    public DialogueData dialougeData;

    private DialoguePanel _dialoguePanel;
    private Interactor _interactor;

    public UnityAction<IInteractable> OnInteractionComplete { get; set; }

    private void Awake()
    {
        _dialoguePanel = GameObject.FindGameObjectWithTag("Dialogue Panel").GetComponent<DialoguePanel>();    
    }

    public void Interact(Interactor interactor, out bool interactionSuccessful)
    {
        _interactor = interactor;
        _dialoguePanel.Init(this);
        interactionSuccessful = true;
    }

    public void EndInteraction()
    {
        _interactor = null;
    }

    public void PickUp()
    {
        var inventory = _interactor.transform.GetComponent<PlayerInventoryHolder>();
        if (!inventory) return;
        if (inventory.AddToInventory(inventoryItemData, 1))
        {
            EndInteraction();
            Destroy(gameObject);
        }
        //Todo handle when not added to inventory
    }
}
