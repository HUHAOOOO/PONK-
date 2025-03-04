using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class CharState : CoreMonoBehaviour
{
    [SerializeField] protected CharCtrl charCtrl;


    [SerializeField] protected bool isAttackingFake;
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
        LoadCharCtrl();
    }
    protected virtual void LoadCharCtrl()
    {
        if (this.charCtrl != null) return;
        charCtrl = GetComponent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
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
        isAttacking = false;

        Invoke(nameof(SetFalse), 0.5f);
    }

    public virtual void SetFalse()
    {
        isAttackingFake = false;

        isDodging = false;
        isHurting = false;
    }
    public virtual void InputAttack()
    {
        if (!IsCanAttack()) return;
        if (charCtrl.CharInput.InputAttack)
        {
            isAttacking = true; isAttackingFake = true;
            canAttack = false;
            charCtrl.CharInput.SetFalseInput();
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
        if (charCtrl.CharInput.InputDodge)
        {
            isDodging = true;
            canDodge = false;
            charCtrl.CharInput.SetFalseInput();
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
        if (this.isAttackingFake || IsDodging || IsHurting || IsDying) return false;
        return true;
    }
}
