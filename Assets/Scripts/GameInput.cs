using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private PlayerInputActions _playerInputActions;

    public event EventHandler OnPlayerAttack;
    public event EventHandler OnPlayerDash;
    public event EventHandler OnPlayerInteract;   // <-- добавлено

    private void Awake()
    {
        Instance = this;

        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();

        _playerInputActions.Combat.Attack.started += PlayerAttack_started;
        _playerInputActions.Player.Dash.performed += PlayerDash_perfomed;
        _playerInputActions.Player.Interact.performed += Interact_performed;   // <-- _
    }

    private void PlayerDash_perfomed(InputAction.CallbackContext obj)
    {
        OnPlayerDash?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerAttack_started(InputAction.CallbackContext obj)
    {
        OnPlayerAttack?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext ctx)
    {
        OnPlayerInteract?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVector()
    {
        return _playerInputActions.Player.Move.ReadValue<Vector2>();
    }

    public void DisableMovement()
    {
        _playerInputActions.Player.Move.Disable();
        _playerInputActions.Player.Dash.Disable();
        _playerInputActions.Combat.Disable();
    }

    public void EnableMovement()
    {
        _playerInputActions.Player.Move.Enable();
        _playerInputActions.Player.Dash.Enable();
        _playerInputActions.Combat.Enable();
    }

    private void OnDestroy()
    {
        _playerInputActions.Player.Interact.performed -= Interact_performed;   // <-- _
        _playerInputActions.Dispose();
    }
}