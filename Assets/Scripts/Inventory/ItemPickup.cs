using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Not currently used - Use interaction trigger instead
/// </summary>
[RequireComponent(typeof(SphereCollider))]
public class ItemPickup : MonoBehaviour, IInteractable
{
    public float pickUpRadius = 1f;
    public InventoryItemData inventoryItemData;

    private SphereCollider _collider;

    public UnityAction<IInteractable> OnInteractionComplete { get; set; }

    public void EndInteraction()
    {
    }

    public void Interact(Interactor interactor, out bool interactionSuccessful)
    {
        interactionSuccessful = false;
        var inventory = interactor.transform.GetComponent<PlayerInventoryHolder>();
        if (!inventory) return;
        if (inventory.AddToInventory(inventoryItemData, 1))
        {
            Destroy(gameObject);
            interactionSuccessful = true;
        } 
    }

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.isTrigger = true;
        _collider.radius = pickUpRadius;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    var inventory = other.transform.GetComponent<PlayerInventoryHolder>();
    //    if (!inventory) return;
    //    if (inventory.AddToInventory(inventoryItemData, 1))
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}
}
