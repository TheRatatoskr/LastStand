using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInput;

[CreateAssetMenu(fileName = "New Input Reader", menuName = "Input/Input Reader")]
public class InputReader : ScriptableObject, IActionSceneControlsActions
{
    private PlayerInput input;

    public event Action<bool> JumpButtonPressed;

    public event Action<bool> AttackButtonPressed;

    public event Action<Vector2> MovementKeysChanged;



    private void OnEnable()
    {
        if(input == null)
        {
            input = new PlayerInput();
            input.ActionSceneControls.SetCallbacks(this);
        }
        input.ActionSceneControls.Enable();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            AttackButtonPressed?.Invoke(true);
        }
        else if(context.canceled)
        {
            AttackButtonPressed?.Invoke(false);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            JumpButtonPressed?.Invoke(true);
        }
        else if (context.canceled)
        {
            JumpButtonPressed?.Invoke(false);
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementKeysChanged?.Invoke(context.ReadValue<Vector2>());
    }

}
