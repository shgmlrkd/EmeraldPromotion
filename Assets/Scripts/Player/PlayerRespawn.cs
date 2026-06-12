using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField]
    private Transform respawnPoint;

    private PlayerStatus playerHealth;

    private void Awake()
    {
        playerHealth = GetComponentInParent<PlayerStatus>();
    }

    public void Respawn(CharacterController characterController)
    {
        playerHealth.ResetStatus();

        characterController.enabled = false;

        transform.root.position = Vector3.zero;
        transform.root.rotation = Quaternion.identity;

        characterController.enabled = true;
    }
}