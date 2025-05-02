using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class CharAnimatorCtrl : CoreMonoBehaviour
{
    [SerializeField] protected CharCtrl _charCtrl;
    [SerializeField] protected CharTimeAnimClip charTimeAnimClip;

    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Dodge = Animator.StringToHash("Dodge");
    private static readonly int Hurt = Animator.StringToHash("Hurt");
    private static readonly int Die = Animator.StringToHash("Die");

    [SerializeField] protected float _attackAnimTime = 1.0f;
    [SerializeField] protected float _dodgeAnimTime = 1.0f;
    [SerializeField] protected float _hurtAnimTime = 1.0f;
    [SerializeField] protected float _dieAnimTime = 1.0f;

    [SerializeField] protected int state;
    [SerializeField] protected int _currentState;
    [SerializeField] protected bool canGetState = false;

    [SerializeField] protected float timeDelay = 0;
    [SerializeField] protected float timer = 0;


    public CharCtrl CharCtrl
    {
        get => _charCtrl;
        private set => _charCtrl = value;
    }
    public float AttackAnimTime
    {
        get => _attackAnimTime;
    }
    public float DodgeAnimTime => _dodgeAnimTime;
    public float HurtAnimTime => _hurtAnimTime;

    protected override void OnEnable()
    {
        canGetState = true;
        state = Idle;
    }

    protected override void LoadComponents()
    {
        LoadCharCtrl();
        LoadCharTimeAnimClip();

        GetAnimClipTime();
    }

    protected virtual void GetAnimClipTime()
    {
        _attackAnimTime = charTimeAnimClip.AtackAnimTime;
        _dodgeAnimTime = charTimeAnimClip.DodgeAnimTime;
        _hurtAnimTime = charTimeAnimClip.HurtAnimTime;
        _dieAnimTime = charTimeAnimClip.DieAnimTime;
    }
    protected virtual void LoadCharCtrl()
    {
        if (this._charCtrl != null) return;
        _charCtrl = transform.parent.GetComponent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }
    protected virtual void LoadCharTimeAnimClip()
    {
        if (this.charTimeAnimClip != null) return;
        charTimeAnimClip = GetComponent<CharTimeAnimClip>();
        Debug.LogWarning(transform.name + ": LoadCharTimeAnimClip", gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        state = GetState();
        if (state == _currentState) return;
        _currentState = state;
        _charCtrl.CharAnim.CrossFade(_currentState, 0, 0);
    }
    protected virtual void SetTimeDelayAnim(float Time2Delay)
    {
        timeDelay = Time2Delay;
        _charCtrl.CharState.SetFalseSomeThing();
    }

    private int GetState()
    {

        if(!CanGetState()) return _currentState;

        if (_charCtrl.CharState.IsDying)
        {
            return SetNewState(Die, _dieAnimTime);
        }
        else if (_charCtrl.CharState.IsHurting)
        {
            return SetNewState(Hurt, _hurtAnimTime);
        }
        else if (_charCtrl.CharState.IsAttacking)
        {
            return SetNewState(Attack, _attackAnimTime);
        }
        else if (_charCtrl.CharState.IsDodging)
        {
            return SetNewState(Dodge, _dodgeAnimTime);
        }
        else if (canGetState) return Idle;
        
        return _currentState;
    }

    private bool CanGetState()
    {
        if (canGetState == true) return true;
        timer += Time.deltaTime;
        if (timer < timeDelay) return false;
        timeDelay = 0;
        timer = 0;
        canGetState = true;
        return true;
    }
    private int SetNewState(int newState , float animTime)
    {
        canGetState = false;

        SetTimeDelayAnim(animTime);
        return newState;
    }

    public void SetTrueCanGetState()
    {
        canGetState = true;
    }
}
