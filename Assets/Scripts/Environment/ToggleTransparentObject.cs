using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Place on camera to cause objects with TransaparentObject script attached to become transparent when between player and camera
/// </summary>
public class ToggleTransparentObject : MonoBehaviour
{
    [SerializeField] private List<TransparentObject> _currentlyInTheWay;
    [SerializeField] private List<TransparentObject> _alreadyTransparent;
    [SerializeField] private Transform _player;
    private Transform _camera;


    private void Awake()
    {
        _camera = transform;
        _currentlyInTheWay = new List<TransparentObject>();
        _alreadyTransparent = new List<TransparentObject>();
        
    }
    private void Update()
    {
        GetAllObjectsInTheWay();
        MakeObjectsSolid();
        MakeObjectsTransparent();
    }

    private void GetAllObjectsInTheWay()
    {
        _currentlyInTheWay.Clear();

        float cameraPlayerDistance = Vector3.Magnitude(_camera.position - _player.position);

        //if using multiple players create new rays
        Ray rayOneForward = new Ray(_camera.position, _player.position - _camera.position);
        Ray rayOneBackward = new Ray(_player.position, _camera.position - _player.position);


        var hitsOneForward = Physics.RaycastAll(rayOneForward, cameraPlayerDistance);
        var hitsOneBackward = Physics.RaycastAll(rayOneBackward, cameraPlayerDistance);

        foreach(var hit in hitsOneForward)
        {
            if(hit.collider.gameObject.TryGetComponent(out TransparentObject inTheWay))
            {
                if (!_currentlyInTheWay.Contains(inTheWay))
                {
                    _currentlyInTheWay.Add(inTheWay);
                }
            }
        }

        foreach (var hit in hitsOneBackward)
        {
            if (hit.collider.gameObject.TryGetComponent(out TransparentObject inTheWay))
            {
                if (!_currentlyInTheWay.Contains(inTheWay))
                {
                    _currentlyInTheWay.Add(inTheWay);
                }
            }
        }
    }

    private void MakeObjectsTransparent()
    {
        for(int i = 0; i < _currentlyInTheWay.Count; i++)
        {
            TransparentObject obj = _currentlyInTheWay[i];

            if(!_alreadyTransparent.Contains(obj))
            {
                obj.ShowTransparent();
                _alreadyTransparent.Add(obj);
            }
        }
    }

    private void MakeObjectsSolid()
    {
        for (int i = _alreadyTransparent.Count - 1; i >= 0; i--)
        {
            TransparentObject obj = _alreadyTransparent[i];

            if (!_currentlyInTheWay.Contains(obj))
            {
                obj.ShowSolid();
                _alreadyTransparent.Remove(obj);
            }
        }
    }
}
