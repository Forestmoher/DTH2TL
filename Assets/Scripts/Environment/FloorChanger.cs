using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Place in stairwells
/// Rotates camera, sets the next floor active, previous floor inactive, and cycles transparent walls in stairwell
/// </summary>

public class FloorChanger : MonoBehaviour
{
    [SerializeField] private GameObject _currentFloor;
    [SerializeField] private GameObject _nextFloor;
    [SerializeField] private bool _rotateClockwise;
    [SerializeField] private CameraRotator _cameraRotator;
    [SerializeField] private TransparentObject[] _frontFacingWalls;
    [SerializeField] private TransparentObject[] _rearFacingWalls;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _cameraRotator.isRotating = true;
            _cameraRotator.clockwise = _rotateClockwise;
            CycleWallTransparency();
            _nextFloor.SetActive(true);
            _currentFloor.SetActive(false);
        }
    }

    private void CycleWallTransparency()
    {
        foreach(var wall in _frontFacingWalls)
        {
            wall.ShowTransparent();
        }
        foreach (var wall in _rearFacingWalls)
        {
            wall.ShowSolid();
        }
    }


}
