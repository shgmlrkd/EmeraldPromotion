using UnityEngine;

public class EnemyDetectionChecker : MonoBehaviour
{
    [SerializeField]
    private float detectionRange = 3.0f;

    [SerializeField]
    private LayerMask enemyLayer;
    
    [SerializeField]
    private GameObject enemyUI;

    private Collider detectedCollider;

    private void Awake()
    {
        if(enemyUI == null)
        {
            enemyUI = GameObject.FindWithTag("EnemyUI");
        }

        enemyUI.SetActive(false);
    }

    private void Update()
    {
        /*if (detectedCollider != null)
        {
            // 공격했을 때의 충돌체의 체력 UI가 업데이트 되게 해야함

            return;
        }*/

        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange, enemyLayer);

        if (colliders.Length == 0)
        {
            detectedCollider = null;
            enemyUI.SetActive(false);
            return;
        }

        float minDistance = float.MaxValue;

        foreach (Collider collider in colliders)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);

            if (distance < minDistance) 
            {
                minDistance = distance;
                detectedCollider = collider;
            }
        }
        
        if (detectedCollider.TryGetComponent(out EnemyStatus enemy))
        {
            enemyUI.SetActive(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}