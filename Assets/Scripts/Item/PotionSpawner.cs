using System.Collections;
using UnityEngine;

public class PotionSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject healPotionPrefab;

    [SerializeField]
    private Vector3 healPotionPos;

    [SerializeField]
    private GameObject staminaPotionPrefab;

    [SerializeField]
    private Vector3 staminaPotionPos;

    private void Start()
    {
        StartCoroutine(HealLoop());
        StartCoroutine(StaminaLoop());
    }

    private IEnumerator HealLoop()
    {
        while (true)
        {
            Instantiate(healPotionPrefab, healPotionPos, Quaternion.identity, transform);

            yield return new WaitUntil(() =>
            {
                foreach (Transform child in transform)
                {
                    if (child.name.StartsWith(healPotionPrefab.name))
                        return false; // 아직 있으므로 계속 기다림
                }

                return true; // 없으므로 WaitUntil 종료
            });

            yield return new WaitForSeconds(3.0f);
        }
    }

    private IEnumerator StaminaLoop()
    {
        while (true)
        {
            Instantiate(staminaPotionPrefab, staminaPotionPos, Quaternion.identity, transform);

            yield return new WaitUntil(() =>
            {
                foreach (Transform child in transform)
                {
                    if (child.name.StartsWith(staminaPotionPrefab.name))
                        return false; // 아직 있으므로 계속 기다림
                }

                return true; // 없으므로 WaitUntil 종료
            });


            yield return new WaitForSeconds(3.0f);
        }
    }
}
