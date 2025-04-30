using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class CharState : CoreMonoBehaviour
{
    [SerializeField] protected CharCtrl _charCtrl;


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

    [SerializeField] protected bool isCanPress = true;

    public bool CanAttack { get => canAttack; }
    public bool CanDodge { get => canDodge; }

    public bool IsAttacking { get => isAttacking; }
    public bool IsDodging { get => isDodging; }
    public bool IsHurting { get => isHurting; set => isHurting = value; }
    public bool IsDying { get => isDying; set => isDying = value; }
    public bool IsCanPressBool { get => isCanPress; set => isCanPress = value; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCharCtrl();
    }
    protected virtual void LoadCharCtrl()
    {
        if (this._charCtrl != null) return;
        _charCtrl = GetComponent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }
    private void Update()
    {
        if (_charCtrl.DamReceive.IsDie == true) return;

        if (!IsCanPress()) return;
        GetInput();
    }

    protected override void OnEnable()
    {
        isDying = false;

        canAttack = true;
        canDodge = true;
        isCanPress = true;


        isAttacking = false;
        isAttackingFake = false;

        isDodging = false;
        isHurting = false;

        IsCanPressBool = true;
    }

    private void GetInput()
    {
        InputDodge();
        InputAttack();
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

        IsCanPressBool = true;
    }
    public virtual void InputAttack()
    {
        if (!IsCanAttack()) return;
        if (_charCtrl.CharInput.InputAttack)
        {
            isAttacking = true; isAttackingFake = true;
            canAttack = false;
            _charCtrl.CharInput.SetFalseInput();

            IsCanPressBool = false;
        }
    }
    public virtual bool IsCanAttack()
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
        if (_charCtrl.CharInput.InputDodge)
        {
            isDodging = true;
            canDodge = false;
            _charCtrl.CharInput.SetFalseInput();

            IsCanPressBool = false;
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
        if (!isCanPress) return false;
        if (this.isAttackingFake || IsDodging || IsHurting || IsDying) return false;
        return true;
    }
}
