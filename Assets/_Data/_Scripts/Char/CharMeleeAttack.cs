using UnityEngine;

public class CharMeleeAttack : CoreMonoBehaviour
{
    [SerializeField] protected CharCtrl charCtrl;


    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected float attackRange = 1.5f;
    [SerializeField] protected int ballLayers;

    [SerializeField] protected float timeDelayMeleeAttack = 0f;

    //[SerializeField] protected float timerAttack = 0f;
    //[SerializeField] protected float timeDelayAttack = 1f;
    //[SerializeField] protected bool canAttack = true;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAttackPoint();
        LoadCharCtrl();

        InitData();
    }
    protected virtual void InitData()
    {
        timeDelayMeleeAttack = charCtrl.CharAnimatorCtrl.AttackAnimTime;

        ballLayers = LayerManager.Instance.BallLayerGetMask;
    }
    protected virtual void LoadAttackPoint()
    {
        if (this.attackPoint != null) return;
        attackPoint = transform.Find("AttackPoint").GetComponent<Transform>();
        Debug.LogWarning(transform.name + ": LoadAttackPoint", gameObject);
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
        //if (!IsCanAttack()) return;
        if (charCtrl.CharState.IsAttacking)
        {
            Invoke(nameof(MeleeAttack), timeDelayMeleeAttack);
        }
    }

    private void MeleeAttack()
    {
        Collider2D ball = Physics2D.OverlapCircle(attackPoint.position, attackRange, ballLayers);// LayerMask.GetMask("Ball"));// enemyLayers);
        if (ball == null) return;
        Debug.Log(this.transform.parent.name + " hit " + ball.name);
        BallCtrl ballctrl = ball.transform.parent.parent.parent.GetComponent<BallCtrl>();
        ballctrl.BallRotate.ChangeDirection();
    }
    public void CancelInvokeAttack()
    {
        if (IsInvoking(nameof(MeleeAttack)))
        {
            CancelInvoke(nameof(MeleeAttack));
        }
    }

    //protected virtual bool IsCanAttack()
    //{
    //    if (canAttack) return true;

    //    timerAttack += Time.deltaTime;
    //    if (timerAttack < timeDelayAttack) return false;
    //    timerAttack = 0f;
    //    canAttack = true;

    //    return true;
    //}
}
