using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;
    public static bool IsAttack;

    private InputAction moveAction;
    private InputAction attackAction;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Attack");
    }

    private void Update()
    {
        Movement = moveAction.ReadValue<Vector2>();
        IsAttack = attackAction.WasPressedThisFrame();
    }
}