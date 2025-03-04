using UnityEngine;

public class ItemTrigger : CoreMonoBehaviour
{
    [SerializeField] protected ItemCtrl itemCtrl;

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
        BallCtrl ballCtrl = collision.transform.parent.GetComponent<BallCtrl>();
        if (ballCtrl == null) Debug.Log("ItemTrigger OnTriggerEnter2D(ballCtrl) = null !");

        ballCtrl.SetActiveBallsByTime(1,5);
        ballCtrl.BallRotate.SpeedSpecialBall(100,5);

        this.itemCtrl.ItemDespawn.SetItemCanDespawn(true);
    }
}
