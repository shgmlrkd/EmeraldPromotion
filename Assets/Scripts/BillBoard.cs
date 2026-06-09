using UnityEngine;

public class BillBoard : MonoBehaviour
{
    private void Update()
    {
        Vector3 pos = Camera.main.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(pos);
    }
}