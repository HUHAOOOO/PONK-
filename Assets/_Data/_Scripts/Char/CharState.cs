using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class CharState : CoreMonoBehaviour
{
    [SerializeField] protected CharCtrl charCtrl;


    [SerializeField] protected bool isAttacking;
    [SerializeField] protected bool isDodging;
    [SerializeField] protected bool isHurting;
    [SerializeField] protected bool isDying;//[ ]

    [SerializeField] protected float timerAttack = 0f;
    [SerializeField] protected float timeDelayAttack = 1f;
    [SerializeField] protected bool canAttack = true;

    [SerializeField] protected float timerDodge = 0f;
    [SerializeField] protected float timeDelayDodge = 1.5f;
    [SerializeField] protected bool canDodge = true;

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
        InputAttack();
        InputDodge();
    }


    public virtual void SetFalseSomeThing()
    {
        isAttacking = false;
        Invoke(nameof(SetFalse), 0.1f);
    }

    public virtual void SetFalse()
    {
        isDodging = false;
        isHurting = false;
    }
    public virtual void InputAttack()
    {
        IsCanAttack();
        if (!canAttack) return;
        if (charCtrl.CharInput.InputAttack)
        {
            isAttacking = true;
            canAttack = false;
            charCtrl.CharInput.SetFalseInput();
        }
    }
    protected virtual void IsCanAttack()
    {
        if (canAttack) return;

        timerAttack += Time.deltaTime;
        if (timerAttack < timeDelayAttack) return;
        timerAttack = 0f;
        canAttack = true;
    }
    public virtual void InputDodge()
    {
        IsCanDodge();
        if (!canDodge) return;

        if (charCtrl.CharInput.InputDodge)
        {
            isDodging = true;
            canDodge = false;
            charCtrl.CharInput.SetFalseInput();
        }
    }
    protected virtual void IsCanDodge()
    {
        if (canDodge) return;

        timerDodge += Time.deltaTime;
        if (timerDodge < timeDelayDodge) return;
        timerDodge = 0f;
        canDodge = true;
    }

}
