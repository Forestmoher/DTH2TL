using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private GameObject _pickUpPanel;
    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;
    [SerializeField] private PlayerInput _playerInput;

    private InputAction continueAction;
    private InputAction exitAction;
    private InputAction yesAction;
    private InputAction noAction;

    private int _currentIndex;
    private DialogueData _dialogueData;
    private InteractionTrigger _interactionTrigger;

    private void Awake()
    {
        continueAction = _playerInput.actions["Continue"];
        exitAction = _playerInput.actions["Exit"];
        yesAction = _playerInput.actions["Yes"];
        noAction = _playerInput.actions["No"];
    }

    public void Init(InteractionTrigger trigger)
    {
        _interactionTrigger = trigger;
        _dialogueData = _interactionTrigger.dialougeData;
        _titleText.text = _dialogueData.title;
        _currentIndex = 0;

        ResetButtons();
        DisplayDialogue();
    }

    private void ResetButtons()
    {
        noAction.Disable();
        yesAction.Disable();

        _noButton.onClick.RemoveAllListeners();
        _yesButton.onClick.RemoveAllListeners();
    }

    private void ClosePanels()
    {
        ResetButtons();
        _pickUpPanel.SetActive(false);
        _dialogueBox.SetActive(false);
    }

    public void DisplayDialogue()
    {
        _dialogueBox.SetActive(true);
        _dialogueText.text = _dialogueData.dialogues[_currentIndex];
    }

    private void Update()
    {
        if (continueAction.WasPerformedThisFrame())
        {
            _currentIndex++;
            if(_currentIndex >= _dialogueData.dialogues.Length)
            {
                if(_interactionTrigger.inventoryItemData != null)
                {
                    ShowPickUpPanel();
                }
                else
                {
                    ClosePanels();
                    _interactionTrigger.EndInteraction();
                }
            }
            else
            {
                DisplayDialogue();
            }
        }

        if(exitAction.WasPerformedThisFrame())
        {
            ClosePanels();
        }

        if (yesAction.WasPerformedThisFrame())
        {
            OnClickYesButton();
        }
        if (noAction.WasPerformedThisFrame())
        {
            OnClickNoButton();
        }
    }

    private void OnClickYesButton()
    {
        _interactionTrigger.PickUp();
        ClosePanels();
    }

    private void OnClickNoButton()
    {
        _interactionTrigger.EndInteraction();
        ClosePanels();
    }

    public void ShowPickUpPanel()
    {
        _pickUpPanel.SetActive(true);

        //assign button handlers
        _noButton.onClick.AddListener(OnClickNoButton);
        _yesButton.onClick.AddListener(OnClickYesButton);

        //enable y/n key actions
        yesAction.Enable();
        noAction.Enable();
    }
}
