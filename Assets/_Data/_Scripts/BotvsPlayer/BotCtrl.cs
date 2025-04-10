using UnityEngine;

public class BotCtrl : CoreMonoBehaviour
{
    private static BotCtrl instance;
    public static BotCtrl Instance => instance;

    [SerializeField] protected BotInput botInput;
    [SerializeField] protected BotState botState;
    [SerializeField] protected Animator botAnim;
    [SerializeField] protected BotAnimatorCtrl botAnimatorCtrl;
    [SerializeField] protected BotMeleeAttack botMeleeAttack;
    [SerializeField] protected BotDodge botDodge;
    [SerializeField] protected BotDespawn botDespawn;
    [SerializeField] protected BotTriggerBall botTrigger;

    [SerializeField] protected BotOption botOption;

    [SerializeField] protected Transform currentPos;
    public BotInput BotInput { get => botInput; }

    public Animator BotAnim { get => botAnim; }

    public BotState BotState { get => botState; }
    public BotAnimatorCtrl BotAnimatorCtrl { get => botAnimatorCtrl; }
    public BotMeleeAttack BotMeleeAttack => botMeleeAttack;
    public BotDodge BotDodge => botDodge;
    public BotDespawn BotDespawn => botDespawn;
    public BotTriggerBall BotTrigger => botTrigger;
    public BotOption BotOption => botOption;
    public Transform CurrentPos => currentPos;
    protected override void Awake()
    {
        if (instance != null) Debug.LogError("only allow 1 BotCtrl | Singleton");
        BotCtrl.instance = this;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBotAnimator();
        LoadBotAnimatorCtrl();
        LoadBotInput();
        LoadBotState();
        LoadBotMeleeAttack();
        LoadBotDodge();
        LoadBotDespawn();
        LoadBotTrigger();

        LoadBotOption();
    }

    protected virtual void LoadBotAnimator()
    {
        if (this.botAnim != null) return;
        botAnim = GetComponentInChildren<Animator>();
        Debug.LogWarning(transform.name + ": LoadBotAnimator", gameObject);
    }
    protected virtual void LoadBotAnimatorCtrl()
    {
        if (this.botAnimatorCtrl != null) return;
        botAnimatorCtrl = GetComponentInChildren<BotAnimatorCtrl>();
        Debug.LogWarning(transform.name + ": LoadBotAnimatorCtrl", gameObject);
    }
    protected virtual void LoadBotInput()
    {
        if (this.botInput != null) return;
        botInput = GetComponent<BotInput>();
        Debug.LogWarning(transform.name + ": LoadBotInput", gameObject);
    }
    protected virtual void LoadBotState()
    {
        if (this.botState != null) return;
        botState = GetComponent<BotState>();
        Debug.LogWarning(transform.name + ": LoadBotState", gameObject);
    }
    protected virtual void LoadBotMeleeAttack()
    {
        if (this.botMeleeAttack != null) return;
        botMeleeAttack = GetComponentInChildren<BotMeleeAttack>();
        Debug.LogWarning(transform.name + ": LoadBotMeleeAttack", gameObject);
    }
    protected virtual void LoadBotDodge()
    {
        if (this.botDodge != null) return;
        botDodge = GetComponentInChildren<BotDodge>();
        Debug.LogWarning(transform.name + ": LoadBotDodge", gameObject);
    }
    protected virtual void LoadBotDespawn()
    {
        if (this.botDespawn != null) return;
        botDespawn = GetComponentInChildren<BotDespawn>();
        Debug.LogWarning(transform.name + ": LoadBotDespawn", gameObject);
    }
    protected virtual void LoadBotTrigger()
    {
        if (this.botTrigger != null) return;
        botTrigger = GetComponentInChildren<BotTriggerBall>();
        Debug.LogWarning(transform.name + ": LoadBotTrigger", gameObject);
    }
    protected virtual void LoadBotOption()
    {
        if (this.botOption != null) return;
        botOption = GetComponent<BotOption>();
        Debug.LogWarning(transform.name + ": LoadBotOption", gameObject);
    }
    public virtual void SetPosBot(Transform pos)
    {
        this.transform.position = pos.position;
        this.transform.rotation = pos.rotation;
        currentPos = pos;
    }



    /// <summary>
    /// Auto
    /// </summary>
    private void Update()
    {
        BotActive();
    }

    private void BotActive()
    {
        if (!BotTrigger.DetectedBall) return;

        if (BotOption.CurrentBotTyleOption == BotTypeOption.Dodge)
        {
            BotInput.InputDodge = true;
            DespawnBotByTime(botAnimatorCtrl.DodgeAnimTime);
        }
        else if (BotOption.CurrentBotTyleOption == BotTypeOption.MeleeAttack)
        {
            BotInput.InputAttack = true;
            DespawnBotByTime(botAnimatorCtrl.AttackAnimTime);
        }
    }

    private void DespawnBotByTime(float timeDespawn)
    {
        if (IsInvoking(nameof(SetBotIsCanDespawn)))
        {
            //Debug.Log("CancelInvoke(nameof(SetBotIsCanDespawn)); ");
            CancelInvoke(nameof(SetBotIsCanDespawn));
        }
        // Despawn sau khi dien xong anim 
        Invoke(nameof(SetBotIsCanDespawn), timeDespawn);
    }

    private void SetBotIsCanDespawn()
    {
        BotInput.InputDodge = false;
        BotInput.InputAttack = false;
        botDespawn.IsCanDespawn = true;
    }
}
