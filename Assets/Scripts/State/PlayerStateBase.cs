using UnityEngine;

public class PlayerStateBase : CharacterStateBase
{
    protected PlayerStateManager stateManager;

    protected override void OnEnable()
    {
        base.OnEnable();

        if (stateManager == null)
        {
            stateManager = GetComponent<PlayerStateManager>();
        }
    }
}