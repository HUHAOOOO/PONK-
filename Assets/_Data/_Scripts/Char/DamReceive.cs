using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class DamReceive : CoreMonoBehaviour
{
    [SerializeField] protected CharCtrl charCtrl;

    [SerializeField] protected int maxHealthPoints = 5;
    [SerializeField] protected int currentHealthPoints;
    [SerializeField] protected bool isDie;
    public int MaxHealthPoints { get => maxHealthPoints; set => maxHealthPoints = value; }
    public int CurrentHealthPoints { get => currentHealthPoints; set => currentHealthPoints = value; }
    public bool IsDie { get => isDie; }

    protected override void OnEnable()
    {
        isDie = false;
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
    }
    protected virtual void LoadCharCtrl()
    {
        if (this.charCtrl != null) return;
        charCtrl = transform.parent.GetComponent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }
    public virtual void Hurt()
    {
        charCtrl.CharAnimatorCtrl.SetTrueCanGetState();
        charCtrl.CharState.IsHurting = true;
        charCtrl.CharImmortalArmor.SetBool_TimeImmortalSheild(true);
        charCtrl.CharMeleeAttack.CancelInvokeAttack();
    }
    public virtual void Die()
    {
        Debug.Log(transform.parent.name + " has been defeated !");
        charCtrl.CharState.IsDying = true;

        isDie = true;
    }
}
