using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerStatus : CharacterStatus, IDamageable
{
    [Header("플레이어 스테미나 이미지")]
    [SerializeField]
    private Image staminaImage;

    [Header("플레이어 스테미나 텍스트")]
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

    private float maxStamina;

    private float curStamina;
    public float CurStamina => curStamina;

    private float recoveryTimer;

    private void Awake()
    {
        stateManager = GetComponent<PlayerStateManager>();
    }

    protected override void Start()
    {
        SetHp(stateManager.Data.maxHp);

        base.Start();

        SetStamina(stateManager.Data.maxStamina);

        curStamina = maxStamina;

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

            isDead = true;
            OnDie?.Invoke();
        }
        else
        {
            curHp -= damage;
            isHit = true;
            OnHit?.Invoke();
        }

        UpdateHpUI(curHp);
    }

    public void ResetStatus()
    {
        OnRevive?.Invoke();

        isDead = false;

        curHp = maxHp;
        curStamina = maxStamina;

        UpdateHpUI(curHp);
    }

    public void SetStamina(float stamina)
    {
        maxStamina = stamina;
    }

    public void UseStamina(float amount)
    {
        curStamina = Mathf.Max(0.0f, curStamina - amount);

        UpdateStaminaUI(curStamina);
    }

    public void StaminaHeal(float amount)
    {
        if (curStamina + amount > maxStamina)
        {
            curStamina = maxStamina;
        }
        else
        {
            curStamina += amount;
        }

        UpdateStaminaUI(curStamina);
    }

    private void UpdateStaminaUI(float curStamina)
    {
        staminaImage.fillAmount = curStamina / maxStamina;
        staminaText.text = $"{curStamina.ToString("F0")} / {maxStamina}";
    }

    private void UpdateStamina()
    {
        if (IsDead) return;

        if(!stateManager.InventoryUI.isInventoryOpen)
        {
            if (InputManager.IsAttack)
            {
                recoveryTimer = 0.0f;
                return;
            }
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
