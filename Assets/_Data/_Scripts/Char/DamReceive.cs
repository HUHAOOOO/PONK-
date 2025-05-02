using UnityEngine;

public class DamReceive : CoreMonoBehaviour
{
    [SerializeField] protected CharCtrl charCtrl;
    [SerializeField] protected BoxCollider2D boxCollider2D;

    [SerializeField] protected int maxHealthPoints = 10;
    [SerializeField] protected int currentHealthPoints;
    [SerializeField] protected bool isDie;
    public int MaxHealthPoints { get => maxHealthPoints; set => maxHealthPoints = value; }
    public int CurrentHealthPoints { get => currentHealthPoints; set => currentHealthPoints = value; }
    public bool IsDie { get => isDie; }

    protected override void OnEnable()
    {
        boxCollider2D.enabled = true;

        isDie = false;
        currentHealthPoints = maxHealthPoints;
    }
    protected override void Start()
    {
        currentHealthPoints = maxHealthPoints;
    }
    public virtual void TakeDam(int damReceive)
    {
        currentHealthPoints -= damReceive;
        Hurt();
        if (currentHealthPoints <= 0) Die();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCharCtrl();
        LoadboxCollider2D();
    }
    protected virtual void LoadCharCtrl()
    {
        if (this.charCtrl != null) return;
        charCtrl = transform.parent.GetComponent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }
    protected virtual void LoadboxCollider2D()
    {
        if (this.boxCollider2D != null) return;
        boxCollider2D = GetComponent<BoxCollider2D>();
        Debug.LogWarning(transform.name + ": LoadboxCollider2D", gameObject);
    }
    public virtual void Hurt()
    {
        charCtrl.CharAnimatorCtrl.SetTrueCanGetState();
        charCtrl.CharState.IsHurting = true;
        charCtrl.CharImmortalArmor.SetBool_TimeImmortalSheild(true);
        charCtrl.CharMeleeAttack.CancelInvokeAttack();
        AudioManager.Instance.PlaySFX("Hit");
    }
    public virtual void Die()
    {
        Debug.Log(transform.parent.name + " has been defeated !");
        charCtrl.CharState.IsDying = true;

        isDie = true;
        GameManager.Instance.SetDiePlayerByPlayerIndexType(charCtrl.PlayerIndexType);
        AudioManager.Instance.PlaySFX("Die");
        boxCollider2D.enabled = false;
    }
}
