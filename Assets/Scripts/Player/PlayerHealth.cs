using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private Image hpImage;

    private float maxHp = 100;
    private float curHp;

    private void Start()
    {
        curHp = maxHp;

        hpImage.fillAmount = 1.0f;
    }

    public void Heal(float amount)
    {
        if(curHp + amount > maxHp)
        {
            curHp = maxHp;
        }

        curHp += amount;

        UpdateHpUI(curHp);

        print("체력 회복 : " + amount);
    }

    public void TakeDamage(float damage)
    {
        curHp -= damage;

        UpdateHpUI(curHp);

        if (curHp <= 0)
        { 
            Die();
        }
    }

    private void UpdateHpUI(float curHp)
    {
        hpImage.fillAmount = curHp / maxHp;
    }

    private void Die()
    {
        Debug.Log("Player Dead");
    }
}
