using System.Collections;
using UnityEngine;

public class HealSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject healPrefab;

    private void Start()
    {
        Instantiate(healPrefab, transform.position, Quaternion.identity, transform);
        StartCoroutine(HealItemSpawnCoroutine());
    }

    private IEnumerator HealItemSpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => transform.childCount == 0);

            yield return new WaitForSeconds(3.0f);

            Instantiate(healPrefab, transform.position, Quaternion.identity, transform);
        }
    }
}
