using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatus : MonoBehaviour
{
    [Header("캐릭터 체력 이미지")]
    [SerializeField]
    protected Image hpImage;

    [Header("캐릭터 체력 텍스트")]
    [SerializeField]
    protected TextMeshProUGUI hpText;

    protected float maxHp;

    protected float curHp;
    public float CurHp => curHp;

    protected bool isHit;

    public bool IsHit => isHit;

    protected bool isDead;
    public bool IsDead => isDead;

    protected virtual void Start()
    {
        curHp = maxHp;
        hpImage.fillAmount = 1.0f;
    }

    protected void UpdateHpUI(float curHp)
    {
        hpImage.fillAmount = curHp / maxHp;
        hpText.text = $"{curHp} / {maxHp}";
    }

    public void SetHp(float hp)
    {
        maxHp = hp;
    }

    private void OnHitEnd()
    {
        isHit = false;
    }
}