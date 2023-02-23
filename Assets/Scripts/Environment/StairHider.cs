using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Place this script in basement hallways leading to and from stairs
/// Sets stairwells active/inactive when moving through a stair hallway
/// </summary>

[RequireComponent(typeof(BoxCollider))]
public class StairHider : MonoBehaviour
{
    [SerializeField] private GameObject _stairwell;
    [SerializeField] private bool _hide;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
             _stairwell.SetActive(!_hide);
        }
    }
}
