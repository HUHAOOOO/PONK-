using UnityEngine;

public class CharCtrl : CoreMonoBehaviour
{
    [SerializeField] protected int posIndex;

    [SerializeField] protected CharInput charInput;
    [SerializeField] protected CharState charState;
    [SerializeField] protected Animator charAnim;
    [SerializeField] protected CharAnimatorCtrl charAnimatorCtrl;
    [SerializeField] protected CharMeleeAttack charMeleeAttack;
    [SerializeField] protected CharDodge charDodge;
    [SerializeField] protected DamReceive damReceive;
    [SerializeField] protected CharImmortalSheild charImmortalShield;

    [SerializeField] protected Transform currentPos;
    public int PosIndex { get => posIndex; set => posIndex = value; }
    public CharInput CharInput { get => charInput; }

    public Animator CharAnim { get => charAnim; }

    public CharState CharState { get => charState; }
    public CharAnimatorCtrl CharAnimatorCtrl { get => charAnimatorCtrl; }
    public DamReceive DamReceive => damReceive;
    public CharDodge CharDodge => charDodge;
    public CharImmortalSheild CharImmortalArmor => charImmortalShield;
    public CharMeleeAttack CharMeleeAttack => charMeleeAttack;
    public Transform CurrentPos => currentPos;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCharAnimator();
        LoadCharAnimatorCtrl();
        LoadCharInput();
        LoadCharState();
        LoadDamReceive();
        LoadCharDodge();
        LoadCharImmortalShield();
        LoadCharMeleeAttack();
    }

    protected virtual void LoadCharAnimator()
    {
        if (this.charAnim != null) return;
        charAnim = GetComponentInChildren<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }
    protected virtual void LoadCharAnimatorCtrl()
    {
        if (this.charAnimatorCtrl != null) return;
        charAnimatorCtrl = GetComponentInChildren<CharAnimatorCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharAnimatorCtrl", gameObject);
    }
    protected virtual void LoadCharInput()
    {
        if (this.charInput != null) return;
        charInput = GetComponent<CharInput>();
        Debug.LogWarning(transform.name + ": LoadCharInput", gameObject);
    }
    protected virtual void LoadCharState()
    {
        if (this.charState != null) return;
        charState = GetComponent<CharState>();
        Debug.LogWarning(transform.name + ": LoadCharState", gameObject);
    }
    protected virtual void LoadDamReceive()
    {
        if (this.damReceive != null) return;
        damReceive = GetComponentInChildren<DamReceive>();
        Debug.LogWarning(transform.name + ": LoadDamReceive", gameObject);
    }
    protected virtual void LoadCharDodge()
    {
        if (this.charDodge != null) return;
        charDodge = GetComponentInChildren<CharDodge>();
        Debug.LogWarning(transform.name + ": LoadCharDodge", gameObject);
    }
    protected virtual void LoadCharImmortalShield()
    {
        if (this.charImmortalShield != null) return;
        charImmortalShield = GetComponentInChildren<CharImmortalSheild>();
        Debug.LogWarning(transform.name + ": LoadcharImmortalShield", gameObject);
    }
    protected virtual void LoadCharMeleeAttack()
    {
        if (this.charMeleeAttack != null) return;
        charMeleeAttack = GetComponentInChildren<CharMeleeAttack>();
        Debug.LogWarning(transform.name + ": LoadCharMeleeAttack", gameObject);
    }


    private void Update()
    {
        //if(damReceive.IsDie)
        //{
        //    GameManager.Instance.SetPosPiOff(posIndex);
        //    this.gameObject.SetActive(false);
        //}
    }


    public virtual void SetPosChar(Transform pos,int indexPos)
    {
        this.transform.position = pos.position;
        this.transform.rotation = pos.rotation;
        currentPos = pos;

        posIndex = indexPos;
    }
}