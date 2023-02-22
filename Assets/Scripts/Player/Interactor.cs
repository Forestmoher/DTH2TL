using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Place on player character. Checks for interactable objects in range, and on E clicked, triggers interaction 
/// </summary>

public class Interactor : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private LayerMask _interactionLayer;
    [SerializeField] private float _interactionRadius = 1f;
    private InputAction _interactAction;

    public bool isInteracting { get; private set; }

    private void Awake()
    {
        _interactAction = _input.actions["Interact"];
    }

    private void Update()
    {
        var colliders = Physics.OverlapSphere(_interactionPoint.position, _interactionRadius, _interactionLayer);

        if (_interactAction.triggered)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                var interactable = colliders[i].GetComponent<IInteractable>();
                if (interactable != null) StartInteraction(interactable);
            }
        }
    }

    private void StartInteraction(IInteractable interactable)
    {
        interactable.Interact(this, out bool interactSuccessful);
        isInteracting = true;
    }

    //unused
    private void EndInteraction()
    {
        isInteracting = false;
    }
}
