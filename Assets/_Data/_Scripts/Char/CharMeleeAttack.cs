using UnityEngine;

public class CharMeleeAttack : CoreMonoBehaviour
{
    [SerializeField] protected CharCtrl charCtrl;


    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected float attackRange = 1.5f;
    [SerializeField] protected LayerMask enemyLayers;

    [SerializeField] protected float timeDelayMeleeAttack = 0f;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBallCtrl();
        LoadCharCtrl();

        InitData();
    }
    protected virtual void InitData()
    {
        timeDelayMeleeAttack = charCtrl.CharAnimatorCtrl.AttackAnimTime;
    }
    protected virtual void LoadBallCtrl()
    {
        if (this.attackPoint != null) return;
        attackPoint = transform.Find("AttackPoint").GetComponent<Transform>();
        Debug.LogWarning(transform.name + ": LoadBallCtrl", gameObject);
    }

    protected virtual void LoadCharCtrl()
    {
        if (this.charCtrl != null) return;
        charCtrl = transform.parent.GetComponent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }

    private void Update()
    {
        Attack();
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    private void Attack()
    {
        if (charCtrl.CharState.IsAttacking)
        {
            Invoke(nameof(MeleeAttack), timeDelayMeleeAttack);
        }
    }

    private void MeleeAttack()
    {
        Collider2D ball = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayers);
        if (ball == null) return;
        Debug.Log(this.transform.parent.name + " hit " + ball.name);
        BallCtrl ballctrl = ball.transform.parent.parent.parent.GetComponent<BallCtrl>();
        ballctrl.BallRotate.ChangeDirection();
    }
}
