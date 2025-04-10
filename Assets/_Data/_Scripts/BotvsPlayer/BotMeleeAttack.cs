using UnityEngine;

public class BotMeleeAttack : CoreMonoBehaviour
{
    [SerializeField] protected BotCtrl botCtrl;


    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected float attackRange = 1.5f;
    [SerializeField] protected int ballLayers;

    [SerializeField] protected float timeDelayMeleeAttack = 0f;
    [SerializeField] protected bool isCanOverlapCircleMeleeAttack;
    public bool IsCanOverlapCircleMeleeAttack { get => isCanOverlapCircleMeleeAttack; set => isCanOverlapCircleMeleeAttack = value; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAttackPoint();
        LoadBotCtrl();

        InitData();
    }
    protected virtual void InitData()
    {
        timeDelayMeleeAttack = botCtrl.BotAnimatorCtrl.AttackAnimTime;
        isCanOverlapCircleMeleeAttack = true;
    }

    protected override void Start()
    {
        //ballLayers = 128;
        ballLayers = LayerManager.Instance.BallLayerGetMask;
    }

    protected virtual void LoadAttackPoint()
    {
        if (this.attackPoint != null) return;
        attackPoint = transform.Find("AttackPoint").GetComponent<Transform>();
        Debug.LogWarning(transform.name + ": LoadAttackPoint", gameObject);
    }

    protected virtual void LoadBotCtrl()
    {
        if (this.botCtrl != null) return;
        botCtrl = transform.parent.GetComponent<BotCtrl>();
        Debug.LogWarning(transform.name + ": LoadBotCtrl", gameObject);
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
        if (botCtrl.BotState.IsAttacking)
        {
            CancelInvoke(nameof(MeleeAttack));
            Invoke(nameof(MeleeAttack), timeDelayMeleeAttack);
        }
    }

    private void MeleeAttack()
    {
        if (!isCanOverlapCircleMeleeAttack) return;

        Collider2D ballDamSender_Collider2D = Physics2D.OverlapCircle(attackPoint.position, attackRange, ballLayers);
        if (ballDamSender_Collider2D == null) return;
        Debug.Log(this.transform.parent.name + " hit " + ballDamSender_Collider2D.name);
        // DamSender de navigation . lieu co ok ko khi ko dung nhung van GetComponent ... chi de navigation code cho de
        DamSender ballDamSender = ballDamSender_Collider2D.GetComponent<DamSender>();

        BallCtrl ballctrl = ballDamSender.transform.parent.parent.parent.GetComponent<BallCtrl>();
        ballctrl.BallRotate.ChangeDirection();

        Transform fx = FXSpawner.Instance.Spawn(FXSpawner.FX_vetchem1, ballDamSender.transform.position, Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
        fx.gameObject.SetActive(true);
    }
    public void CancelInvokeAttack()
    {
        if (IsInvoking(nameof(MeleeAttack)))
        {
            CancelInvoke(nameof(MeleeAttack));
        }
    }
}
