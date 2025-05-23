using UnityEngine;

public class CharMeleeAttack : CoreMonoBehaviour
{
    [SerializeField] protected CharCtrl _charCtrl;


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
        LoadCharCtrl();

        InitData();
    }
    protected virtual void InitData()
    {
        timeDelayMeleeAttack = _charCtrl.CharAnimatorCtrl.AttackAnimTime;
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

    protected virtual void LoadCharCtrl()
    {
        if (this._charCtrl != null) return;
        _charCtrl = transform.parent.GetComponent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }

    private void Update()
    {
        if (_charCtrl.DamReceive.IsDie == true) return;

        if (_charCtrl.CharInput.InputAttack)
        {
            Attack();
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    private void Attack()
    {
        CancelInvokeAttack();
        Invoke(nameof(MeleeAttack), timeDelayMeleeAttack);
    }

    private void MeleeAttack()
    {
        if (!isCanOverlapCircleMeleeAttack) return;
        
        Collider2D ballDamSender_Collider2D = Physics2D.OverlapCircle(attackPoint.position, attackRange, ballLayers);
        if (ballDamSender_Collider2D == null) return;
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
