using UnityEngine;

public class BotState : CoreMonoBehaviour
{
    [SerializeField] protected BotCtrl botCtrl;

    [SerializeField] protected bool isAttacking;
    [SerializeField] protected bool isDodging;
    [SerializeField] protected bool isHurting;
    [SerializeField] protected bool isDying;

    [SerializeField] protected float timerAttack = 0f;
    [SerializeField] protected float timeDelayAttack = 0.5f;
    [SerializeField] protected bool canAttack = true;

    [SerializeField] protected float timerDodge = 0f;
    [SerializeField] protected float timeDelayDodge = 1f;
    [SerializeField] protected bool canDodge = true;

    [SerializeField] protected bool isCanPress;

    public bool CanAttack { get => canAttack; }
    public bool CanDodge { get => canDodge; }

    public bool IsAttacking { get => isAttacking; }
    public bool IsDodging { get => isDodging; }
    public bool IsHurting { get => isHurting; set => isHurting = value; }
    public bool IsDying { get => isDying; set => isDying = value; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBotCtrl();
    }
    protected virtual void LoadBotCtrl()
    {
        if (this.botCtrl != null) return;
        botCtrl = GetComponent<BotCtrl>();
        Debug.LogWarning(transform.name + ": LoadBotCtrl", gameObject);
    }
    private void Update()
    {
        GetInput();
    }
    private void GetInput()
    {
        isCanPress = IsCanPress();
        if (!IsCanPress()) return;
        InputAttack();
        InputDodge();
    }
    public virtual void SetFalseSomeThing()
    {
        Invoke(nameof(SetFalse), 0.1f);//0.5f
    }
    public virtual void SetFalse()
    {
        isAttacking = false;
        isDodging = false;
        isHurting = false;
    }
    public virtual void InputAttack()
    {
        if (!IsCanAttack()) return;
        if (botCtrl.BotInput.InputAttack)
        {
            isAttacking = true;
            canAttack = false;
            botCtrl.BotInput.SetFalseInput();
        }
    }
    protected virtual bool IsCanAttack()
    {
        if (canAttack) return true;

        timerAttack += Time.deltaTime;
        if (timerAttack < timeDelayAttack) return false;
        timerAttack = 0f;
        canAttack = true;
        return true;
    }
    public virtual void InputDodge()
    {
        if (!IsCanDodge()) return;
        if (botCtrl.BotInput.InputDodge)
        {
            isDodging = true;
            canDodge = false;
            botCtrl.BotInput.SetFalseInput();
        }
    }
    protected virtual bool IsCanDodge()
    {
        if (canDodge) return true;

        timerDodge += Time.deltaTime;
        if (timerDodge < timeDelayDodge) return false;
        timerDodge = 0f;
        canDodge = true;
        return true;
    }

    protected virtual bool IsCanPress()
    {
        if (isAttacking || isDodging || isHurting || isDying) return false;
        return true;
    }
}