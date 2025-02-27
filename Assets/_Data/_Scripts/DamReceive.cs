using UnityEngine;

public class DamReceive : CoreMonoBehaviour
{
    [SerializeField] protected CharCtrl charCtrl;

    [SerializeField] protected int healthPoints = 5;

    public virtual void TakeDam(int damReceive)
    {
        healthPoints -= damReceive;
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
    public virtual void Die()
    {
        Debug.Log(transform.parent.name + " has been defeated (teof)!");
        charCtrl.CharState.IsDying = true;
    }
}
