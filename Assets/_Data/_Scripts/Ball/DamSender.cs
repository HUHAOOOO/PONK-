using UnityEngine;

[RequireComponent (typeof(CircleCollider2D))]
[RequireComponent (typeof(Rigidbody2D))]
public abstract class DamSender : CoreMonoBehaviour
{
    [SerializeField] protected int damSender = 1;
    [SerializeField] protected BallCtrl ballCtrl;
    [SerializeField] protected CircleCollider2D circleCollider2D;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBallCtrl();
        LoadCircleCollider2D();
    }
    protected virtual void LoadBallCtrl()
    {
        if (this.ballCtrl != null) return;
        ballCtrl = transform.parent.parent.parent.GetComponent<BallCtrl>();
        Debug.LogWarning(transform.name + ": LoadBallCtrl", gameObject);
    }
    protected virtual void LoadCircleCollider2D()
    {
        if (this.circleCollider2D != null) return;
        circleCollider2D = GetComponent<CircleCollider2D>();
        Debug.LogWarning(transform.name + ": LoadCircleCollider2D", gameObject);

        circleCollider2D.isTrigger = true;
        circleCollider2D.radius = 0.5f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EffectSpecialBall(collision);
    }
    protected abstract void EffectSpecialBall(Collider2D collision);
}
