using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static CustomInputSystemActions;

public interface IInputReader
{
    Vector2 Direction { get; }
    void EnablePlayerActions();
}

[CreateAssetMenu(fileName = "InputReader", menuName = "ScriptableObjects/InputReader")]
public class InputReader : ScriptableObject, IInputReader, IPlayerInputsActions
{
    public event UnityAction<Vector2> Move = delegate { };
    public event UnityAction<bool> PrimaryAttack = delegate { };
    public event UnityAction<bool> SecondaryAttack = delegate { };
    public event UnityAction<bool> OfensiveAbility = delegate { };
    public event UnityAction<bool> DefensiveAbility = delegate { };
    public event UnityAction<bool> UtilityAbility = delegate { };
    public event UnityAction<bool> MoveAbility = delegate { };

    public CustomInputSystemActions inputActions;

    public Vector2 Direction => inputActions.PlayerInputs.Movement.ReadValue<Vector2>();
    public bool IsPrimaryAttackPressed => inputActions.PlayerInputs.PrimaryAttack.IsPressed();
    public bool IsSecondaryAttackPressed => inputActions.PlayerInputs.SecondaryAttack.IsPressed();
    public bool IsOfensiveAbilityPressed => inputActions.PlayerInputs.OfensiveAbility.IsPressed();
    public bool IsDefensiveAbilityPressed => inputActions.PlayerInputs.DefensiveAbility.IsPressed();
    public bool IsUtilityAbilityPressed => inputActions.PlayerInputs.UtilityAbility.IsPressed();
    public bool IsMoveAbilityPressed => inputActions.PlayerInputs.MoveAbility.IsPressed();

    public void EnablePlayerActions()
    {
        if (inputActions == null)
        {
            inputActions = new CustomInputSystemActions();
            inputActions.PlayerInputs.SetCallbacks(this);
        }
        inputActions.Enable();
    }
    
    public void OnMovement(InputAction.CallbackContext context)
    {
        Move.Invoke(context.ReadValue<Vector2>());
    }
    
    public void OnPrimaryAttack(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                PrimaryAttack.Invoke(true);
                break;
            case InputActionPhase.Canceled:
                PrimaryAttack.Invoke(false);
                break;
        }
    }

    public void OnSecondaryAttack(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                SecondaryAttack.Invoke(true);
                break;
            case InputActionPhase.Canceled:
                SecondaryAttack.Invoke(false);
                break;
        }
    }
    
    public void OnOfensiveAbility(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                OfensiveAbility.Invoke(true);
                break;
            case InputActionPhase.Canceled:
                OfensiveAbility.Invoke(false);
                break;
        }
    }

    public void OnDefensiveAbility(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                DefensiveAbility.Invoke(true);
                break;
            case InputActionPhase.Canceled:
                DefensiveAbility.Invoke(false);
                break;
        }
    }

    public void OnMoveAbility(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                MoveAbility.Invoke(true);
                break;
            case InputActionPhase.Canceled:
                MoveAbility.Invoke(false);
                break;
        }
    }

    public void OnUtilityAbility(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                UtilityAbility.Invoke(true);
                break;
            case InputActionPhase.Canceled:
                UtilityAbility.Invoke(false);
                break;
        }
    }
}
