using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class for all items that can be equipped to hotbar 
/// </summary>
public abstract class Equipment: MonoBehaviour
{
    public string itemName;
    public bool isEquipped;
    public abstract void Use();
    public abstract void Init(EquipmentController controller);
}
