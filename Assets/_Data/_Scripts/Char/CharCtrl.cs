using UnityEngine;

public class CharCtrl : CoreMonoBehaviour
{

    [SerializeField] protected Animator charAnim;
    [SerializeField] protected CharAnimatorCtrl charAnimatorCtrl;
    [SerializeField] protected CharInput charInput;
    [SerializeField] protected CharState charState;
    [SerializeField] protected DamReceive damReceive;

    [SerializeField] protected Transform currentPos;
    public CharInput CharInput { get => charInput; }

    public Animator CharAnim { get => charAnim; }

    public CharState CharState { get => charState; }
    public CharAnimatorCtrl CharAnimatorCtrl { get => charAnimatorCtrl; }
    public DamReceive DamReceive => damReceive;
    public Transform CurrentPos => currentPos;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCharAnimator();
        LoadCharAnimatorCtrl();
        LoadCharInput();
        LoadCharState();
        LoadDamReceive();
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


    public virtual void SetPosChar(Transform pos)
    {
        this.transform.position = pos.position;
        this.transform.rotation = pos.rotation;
        currentPos = pos;
    }
}