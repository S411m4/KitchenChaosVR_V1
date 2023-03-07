using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    [SerializeField] InputActionReference moveAction;
    [SerializeField] InputActionReference interactAction;
    [SerializeField] InputActionReference interactAlternateAction;
    [SerializeField] InputActionReference pauseAction;

    public event EventHandler OnInteractionAction;
    public event EventHandler OnAlternateAction;
    public event EventHandler OnPauseAction;

    public static GameInput Instance { get; private set; }

    void Awake()
    {
        Instance = this;

        interactAction.action.performed += Interact_performed;
        interactAlternateAction.action.performed += InteractAlternate_performed;
        pauseAction.action.performed += Pause_performed;


    }

    private void OnDestroy()
    {
        interactAction.action.performed -= Interact_performed;
        interactAlternateAction.action.performed -= InteractAlternate_performed;
        pauseAction.action.performed -= Pause_performed;
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        if (pauseAction.action.IsPressed())
        { OnPauseAction?.Invoke(this, EventArgs.Empty); }
    }

    private void InteractAlternate_performed(InputAction.CallbackContext obj)
    {

        OnAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        

        OnInteractionAction?.Invoke(this, EventArgs.Empty);

    }

   



}
