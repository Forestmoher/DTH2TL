using TMPro;
using UnityEngine;

/// <summary>
/// Handles interaction UI when player is in range of this item
/// </summary>
public class InteractionIndicator : MonoBehaviour
{
    [SerializeField] private GameObject _interactionIconObject;
    [SerializeField] private TextMeshPro _interactionText;
    [SerializeField] private SpriteRenderer _interactionImage;
    private Transform _camera;
    public string text;
    public Sprite sprite;
    

    private void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("UI Camera").transform;
        if(sprite != null)
        {
            _interactionImage.sprite = sprite;
            _interactionImage.color = Color.white;
        }
        else
        {
            _interactionText.text = text;
            _interactionImage.color = Color.clear;
        }
        _interactionIconObject.gameObject.SetActive(false);

    }

    private void Update()
    {
        //rotate to always face camera
        _interactionIconObject.transform.LookAt(_interactionIconObject.transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.transform.GetChild(1).CompareTag("Player Interaction"))
            {
                ShowInteractor();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.transform.GetChild(1).CompareTag("Player Interaction"))
            {
                HideInteractor();
            }
        }
    }

    public void ShowInteractor()
    {
        _interactionIconObject.SetActive(true);
    }

    public void HideInteractor()
    {
        _interactionIconObject.SetActive(false);
    }
}
