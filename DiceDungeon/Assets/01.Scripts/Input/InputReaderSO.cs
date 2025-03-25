using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(menuName = "SO/Core/InputReader")]
public class InputReaderSO : ScriptableObject, IInGameActions, IUIActions
{
    public Controls control;

    public Action OnMLBDownEvent;
    public Action<bool> OnMLBHoldEvent;
    public Action OnMLBUpEvent;
    public Action<bool> OnLCtrlEvent;

    public Action OnOpenInventoryEvent;
    public Action OnShowInfoEvent;

    private void OnEnable()
    {
        if (control == null)
        {
            control = new Controls();

            control.InGame.SetCallbacks(this);
            control.UI.SetCallbacks(this);
        }
        control.InGame.Enable();
        control.UI.Enable();
    }

    private void OnDisable()
    {
        DisableInput();
    }

    public void DisableInput()
    {
        control.InGame.Disable();
        control.UI.Disable();
    }

    public void OnMLBClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnMLBDownEvent?.Invoke();
            OnMLBHoldEvent?.Invoke(true);
        }
        else if (context.canceled)
        {
            OnMLBHoldEvent?.Invoke(false);
            OnMLBUpEvent?.Invoke();
        }
    }

    public void OnLCtrlPress(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnLCtrlEvent?.Invoke(true);
        else if (context.canceled)
            OnLCtrlEvent?.Invoke(false);
    }

    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnOpenInventoryEvent?.Invoke();
    }

    public void OnShowInfo(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnShowInfoEvent?.Invoke();
    }
}
