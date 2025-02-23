using UnityEngine;

public class CharCtrl : CoreMonoBehaviour
{

    [SerializeField] protected Animator charAnim;
    [SerializeField] protected CharAnimatorCtrl charAnimatorCtrl;
    [SerializeField] protected CharInput charInput;

    //public static CharCtrl instance;
    public CharInput CharInput
    {
        get => charInput;
    }
    public Animator CharAnim
    {
        get => charAnim;
        private set => charAnim = value;
    }
    protected override void Awake()
    {
        //CharCtrl.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCharAnimator();
        LoadCharAnimatorCtrl();
        LoadCharInput();
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
        charInput = GetComponentInChildren<CharInput>();
        Debug.LogWarning(transform.name + ": LoadCharInput", gameObject);
    }
}