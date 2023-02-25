using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Place this script on a gameobject with a box collider in stairwells
/// Sets floors active/inactive when moving from one floor to another
/// </summary>

[RequireComponent(typeof(BoxCollider))]
public class FloorHider : MonoBehaviour
{
    [SerializeField] private GameObject _currentFloor;
    [SerializeField] private GameObject _nextFloor;
    //[SerializeField] private bool _rotateClockwise;
    //[SerializeField] private CameraRotator _cameraRotator;
    //[SerializeField] private TransparentObject[] _frontFacingWalls;
    //[SerializeField] private TransparentObject[] _rearFacingWalls;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //_cameraRotator.isRotating = true;
            //_cameraRotator.clockwise = _rotateClockwise;
            //CycleWallTransparency();
            _nextFloor.SetActive(true);
            _currentFloor.SetActive(false);
        }
    }

    //private void CycleWallTransparency()
    //{
    //    foreach(var wall in _frontFacingWalls)
    //    {
    //        wall.ShowTransparent();
    //    }
    //    foreach (var wall in _rearFacingWalls)
    //    {
    //        wall.ShowSolid();
    //    }
    //}


}
