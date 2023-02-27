using UnityEngine;
using UnityEngine.InputSystem;

public class DoorController : MonoBehaviour
{
    [SerializeField] private bool _isLocked;
    private bool _isOpening;
    private bool _inReach;
    private bool _atDoorFront; //used to tell which side of door player is on
    private float _openSpeed = 0.01f;
    private float _closeSpeed = 0.01f;
    private float _doorState;
    private Animator _animator;

    void Start()
    {
        _isOpening = false;
        _doorState = 0;
        _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        UpdateDoorState();
        OpenDoor(_doorState);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inReach = true;
            if (transform.InverseTransformPoint(other.transform.position).z > 0)
            {
                _atDoorFront = true;
            }
            else
            {
                _atDoorFront = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inReach = false;
        }
    }

    private void UpdateDoorState()
    {
        if(_isLocked) { return; }

        //if the player is within range and clicks 'E'
        if(_inReach && Keyboard.current.eKey.wasPressedThisFrame)
        {
            _isOpening = true;
        }

        //set door state float for animator, depending on which side the player is on
        if(_isOpening && _atDoorFront)
        {
            _doorState -= _openSpeed;
            if (_doorState < -.9f) _isOpening = false;
        }
        else if (_isOpening && !_atDoorFront) 
        {
            _doorState += _openSpeed;
            if (_doorState > .9f) _isOpening = true;
        }

        //if out of reach, automatically close door
        if (!_inReach)
        {
            _doorState = Mathf.Lerp(_doorState, 0, _closeSpeed);
            _isOpening = false;
        }
    }

    private void OpenDoor(float state)
    {
        _animator.SetFloat("State", state);
    }
}
