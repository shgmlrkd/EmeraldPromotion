using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    private Image hpImage;

    [SerializeField]
    private UnityEvent OnRevive;

    [SerializeField]
    private UnityEvent OnHit;

    [SerializeField] 
    private UnityEvent OnDie;

    private float maxHp = 100;
    private float curHp;

    public bool IsHit { get; private set; } = false;
    public bool IsDead { get; private set; } = false;

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
        else
        {
            curHp += amount;
        }

        UpdateHpUI(curHp);

        print("체력 회복 : " + amount);
    }

    public void TakeDamage(float damage)
    {
        if(curHp - damage <= 0)
        {
            curHp = 0.0f;

            IsDead = true;
            OnDie?.Invoke();
        }
        else
        {
            curHp -= damage;
            IsHit = true;
            OnHit?.Invoke();
        }

        UpdateHpUI(curHp);
    }

    public void ResetHealth()
    {
        OnRevive?.Invoke();
        IsDead = false;
        curHp = maxHp;
        UpdateHpUI(curHp);
    }

    private void UpdateHpUI(float curHp)
    {
        hpImage.fillAmount = curHp / maxHp;
    }

    private void OnHitEnd()
    {
        IsHit = false;
    }

}
