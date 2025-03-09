using Unity.VisualScripting;
using UnityEngine;

public class ItemTrigger : CoreMonoBehaviour
{
    [Header("Item Trigger")]
    [SerializeField] protected ItemCtrl itemCtrl;

    [SerializeField] protected int indexBall;
    [SerializeField] protected int timeReset;
    [SerializeField] protected int addSpeedBall;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadItemCtrl();
    }

    protected virtual void LoadItemCtrl()
    {
        if (this.itemCtrl != null) return;
        itemCtrl = this.transform.parent.GetComponent<ItemCtrl>();
        Debug.LogWarning(transform.name + ": LoadItemCtrl", gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamSender ballDamSender = collision.GetComponent<DamSender>();

        BallCtrl ballCtrl = ballDamSender.transform.parent.parent.parent.GetComponent<BallCtrl>();
        if (ballCtrl == null) Debug.Log("ItemTrigger OnTriggerEnter2D(ballCtrl) = null !");
        EffectItem(ballCtrl);
    }

    protected virtual void EffectItem(BallCtrl ballCtrl)
    {
        //override Item |...| Trigger
    }
}
