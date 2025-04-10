using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class DamReceive : CoreMonoBehaviour
{
    [SerializeField] protected CharCtrl charCtrl;
    [SerializeField] protected int healthPoints = 5;
    [SerializeField] protected bool isDie;
    public int HealthPoints { get => healthPoints; set => healthPoints = value; }
    public bool IsDie { get => isDie; }

    protected override void OnEnable()
    {
        isDie = false;
    }

    public virtual void TakeDam(int damReceive)
    {
        healthPoints -= damReceive;
        Hurt();
        if (healthPoints <= 0) Die();
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
        Debug.Log(transform.parent.name + " has been defeated (teof)!");
        charCtrl.CharState.IsDying = true;

        isDie = true;
    }
}
