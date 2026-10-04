using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ActionManager : MonoBehaviour
{
    public UnityEvent jump;
    public UnityEvent<int> sprint;
    public UnityEvent<int> moveCheck;

    public void OnJumpAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Jump was started");
        }
        else if (context.performed)
        {
            jump.Invoke();
            Debug.Log("Jump was performed");
        }
        else if (context.canceled)
        {
            Debug.Log("Jump was cancelled");
        }
    }

    public void OnMoveAction(InputAction.CallbackContext context)
    {
        // Debug.Log("OnMoveAction callback invoked");
        if (context.started)
        {
            int faceRight = Math.Sign(context.ReadValue<Vector2>().x);
            Debug.Log($"Move has started, faceRight = {faceRight}");
            moveCheck.Invoke(faceRight);
        }
        if (context.canceled)
        {
            Debug.Log("Move has stopped");
            moveCheck.Invoke(0);
        }
    }

    public void OnSprintAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Debug.Log("Sprint has started");
        }
        else if (context.performed)
        {
            Debug.Log("Sprint was performed");
            Debug.Log(context.duration);
            sprint.Invoke(1);
        }
        else if (context.canceled)
        {
            Debug.Log("Sprint was cancelled");
            sprint.Invoke(0);
        }
    }
}
