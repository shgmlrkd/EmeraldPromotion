using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;
    public static bool IsAttack;
    public static bool IsRun;

    private InputAction moveAction;
    private InputAction runAction;
    private InputAction attackAction;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        runAction = InputSystem.actions.FindAction("Sprint");
        attackAction = InputSystem.actions.FindAction("Attack");
    }

    private void Update()
    {
        Movement = moveAction.ReadValue<Vector2>();
        IsRun = runAction.IsPressed();
        IsAttack = attackAction.WasPressedThisFrame();
    }
}