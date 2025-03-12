using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReaderSO : ScriptableObject, IInGameActions, IUIActions
{
    public Controls control;

    public Action OnMLBDownEvent;
    public Action<bool> OnMLBHoldEvent;
    public Action OnMLBUpEvent;
    public Action<bool> OnLCtrlEvent;

    public void OnUxmlClick(InputAction.CallbackContext context)
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
        control.InGame.Disable();
        control.UI.Disable();
    }

}
