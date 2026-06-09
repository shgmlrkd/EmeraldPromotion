using UnityEngine;

public class BillBoard : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.position);
    }
}