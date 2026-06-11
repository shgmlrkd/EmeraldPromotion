using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour, IDamageable
{
    [Header("플레이어 상태 이미지")]
    [SerializeField]
    private Image hpImage;

    [SerializeField]
    private Image staminaImage;

    [Header("플레이어 상태 텍스트")]
    [SerializeField]
    private TextMeshProUGUI hpText;

    [SerializeField]
    private TextMeshProUGUI staminaText;

    [Header("이벤트")]
    [SerializeField]
    private UnityEvent OnRevive;

    [SerializeField]
    private UnityEvent OnHit;

    [SerializeField] 
    private UnityEvent OnDie;

    private PlayerStateManager stateManager;

    private float maxHp;

    private float curHp;

    private float maxStamina;

    private float curStamina;
    public float CurStamina => curStamina;

    private float recoveryTimer;

    public bool IsHit { get; private set; } = false;
    public bool IsDead { get; private set; } = false;

    private void Awake()
    {
        stateManager = GetComponent<PlayerStateManager>();
    }

    private void Start()
    {
        SetHp(stateManager.Data.maxHp);
        SetStamina(stateManager.Data.maxStamina);

        curHp = maxHp;
        curStamina = maxStamina;

        hpImage.fillAmount = 1.0f;
        staminaImage.fillAmount = 1.0f;
    }

    private void Update()
    {
        UpdateStamina();
        UpdateStaminaUI(curStamina);
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

    public void SetHp(float hp)
    {
        maxHp = hp;
    }

    public void SetStamina(float stamina)
    {
        maxStamina = stamina;
    }

    private void UpdateHpUI(float curHp)
    {
        hpImage.fillAmount = curHp / maxHp;
        hpText.text = $"{curHp} / {maxHp}";
    }

    public void UseStamina(float amount)
    {
        curStamina = Mathf.Max(0.0f, curStamina - amount);

        UpdateStaminaUI(curStamina);
    }

    private void UpdateStaminaUI(float curStamina)
    {
        staminaImage.fillAmount = curStamina / maxStamina;
        staminaText.text = $"{curStamina.ToString("F0")} / {maxStamina}";
    }

    private void OnHitEnd()
    {
        IsHit = false;
    }

    private void UpdateStamina()
    {
        if(InputManager.IsAttack)
        {
            recoveryTimer = 0.0f;
            return;
        }

        if (stateManager.CurState == PlayerStateManager.State.Run 
            && curStamina > 0.0f)
        {
            recoveryTimer = 0.0f;

            curStamina = Mathf.Max(0.0f,
            curStamina - stateManager.Data.staminaRunConsumeAmount * Time.deltaTime);
            return;
        }

        recoveryTimer += Time.deltaTime;

        if (recoveryTimer >= stateManager.Data.staminaRecoveryDelay)
        {
            curStamina = Mathf.Min(maxStamina,
            curStamina + stateManager.Data.staminaRecoveryAmount * Time.deltaTime);
        }
    }
}
