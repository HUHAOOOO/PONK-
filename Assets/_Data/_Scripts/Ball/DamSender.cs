using UnityEngine;

public class DamSender : CoreMonoBehaviour
{
    [SerializeField] protected int damSender = 1;
    [SerializeField] protected BallCtrl ballCtrl;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBallCtrl();
    }

    protected virtual void LoadBallCtrl()
    {
        if (this.ballCtrl != null) return;
        ballCtrl = transform.parent.GetComponent<BallCtrl>();
        Debug.LogWarning(transform.name + ": LoadBallCtrl", gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamReceive player = collision.gameObject.GetComponent<DamReceive>();
        if (player == null)
        {
            Debug.Log("player DamReceive null !");
            return;
        }
        player.TakeDam(damSender);
        ballCtrl.BallRotate.SetDefaultSpeed();
    }
}
