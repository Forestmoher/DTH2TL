using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flashlight : Equipment
{
    private EquipmentController _controller;

    public override void Init(EquipmentController controller)
    {
        itemName = "Flashlight";
        isEquipped = false;    
        _controller = controller;
    }

    public override void Use()
    {
        if (!isEquipped)
        {
            isEquipped = true;
            //_controller.characterController.radius = 3;
            Debug.Log("Equipped flashlight");
        }
        else
        {
            isEquipped = false;
            Debug.Log("Unequipped flashlight");
        }
        gameObject.SetActive(isEquipped);
        _controller.PlayFlighlightAnimation(isEquipped);
    }

    private void Update()
    {
        if(isEquipped)
        {
            transform.position = _controller.flashlightPosition.position;
        }
    }
}
