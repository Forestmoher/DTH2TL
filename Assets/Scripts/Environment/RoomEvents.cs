using UnityEngine;

public class RoomEvents : MonoBehaviour
{
    [SerializeField] private string _roomName;
    [SerializeField] private LightDimmer[] _dimmers;
    [SerializeField] private TransparentObject[] _frontFacingWalls;
    [SerializeField] private GameObject _ceiling;

    private void Awake()
    {
        _ceiling.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().currentRoom = _roomName;
            _ceiling.SetActive(false);
            TurnLightsOn();
            SetWallsTransparent();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerController>().currentRoom != _roomName)
        {
            _ceiling.SetActive(true);
            TurnLightsOff();
            SetWallsSolid();
        }
    }

    private void TurnLightsOn()
    {
        foreach (var dimmer in _dimmers)
        {
            dimmer.lightOn = true;
        }
    }

    private void TurnLightsOff()
    {
        foreach (var dimmer in _dimmers)
        {
            dimmer.lightOn = false;
        }
    }

    private void SetWallsTransparent()
    {
        foreach(var wall in _frontFacingWalls)
        {
            wall.ShowTransparent();
        }
    }

    private void SetWallsSolid()
    {
        foreach (var wall in _frontFacingWalls)
        {
            wall.ShowSolid();
        }
    }
}
