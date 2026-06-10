using System.Collections;
using UnityEngine;

public class PlayerDeadState : CharacterStateBase
{
    [SerializeField]
    private PlayerRespawn respawn;

    private WaitForSeconds wait;

    [SerializeField]
    private AnimationClip clip;

    private float length = 0.0f;

    private void Awake()
    {
        if (respawn == null)
        { 
            respawn = GetComponentInChildren<PlayerRespawn>();
        }

        length = clip.length;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        wait = new WaitForSeconds(length);

        StartCoroutine(ReSpawnCoroutine());
    }

    private IEnumerator ReSpawnCoroutine()
    {
        yield return wait;

        respawn.Respawn(characterController);
    }
}
