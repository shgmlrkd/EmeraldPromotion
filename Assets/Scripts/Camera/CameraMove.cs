using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    private Vector3 camPosOffset = new Vector3(0.0f, 5.0f, -8.0f);
    private Vector3 camRotOffset = new Vector3(25.0f, 0.0f, 0.0f);

    private void Start()
    {
        transform.position = playerTransform.position;
        transform.rotation = Quaternion.Euler(camRotOffset);
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(camRotOffset);
        Vector3 camPos = playerTransform.position + camPosOffset;

        transform.position = camPos;
    }
}