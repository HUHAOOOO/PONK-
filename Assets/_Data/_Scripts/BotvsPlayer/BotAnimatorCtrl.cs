using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class BotAnimatorCtrl : CoreMonoBehaviour
{
    [SerializeField] protected BotCtrl botCtrl;
    [SerializeField] protected BotTimeAnimClip botTimeAnimClip;

    //cho ra 1 class rieng dc nhi
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


    public BotCtrl BotCtrl
    {
        get => botCtrl;
        private set => botCtrl = value;
    }
    public float AttackAnimTime
    {
        get => _attackAnimTime;
    }
    public float DodgeAnimTime => _dodgeAnimTime;
    public float HurtAnimTime => _hurtAnimTime;
    //public bool CanGetState { get => canGetState; private set => canGetState = value; }
    protected override void LoadComponents()
    {
        LoadBotCtrl();
        LoadBotTimeAnimClip();

        GetAnimClipTime();
    }

    protected virtual void GetAnimClipTime()
    {
        _attackAnimTime = botTimeAnimClip.AtackAnimTime;
        _dodgeAnimTime = botTimeAnimClip.DodgeAnimTime;
        _hurtAnimTime = botTimeAnimClip.HurtAnimTime;
        _dieAnimTime = botTimeAnimClip.DieAnimTime;
    }
    protected virtual void LoadBotCtrl()
    {
        if (this.botCtrl != null) return;
        botCtrl = transform.parent.GetComponent<BotCtrl>();
        Debug.LogWarning(transform.name + ": LoadBotCtrl", gameObject);
    }
    protected virtual void LoadBotTimeAnimClip()
    {
        if (this.botTimeAnimClip != null) return;
        botTimeAnimClip = GetComponent<BotTimeAnimClip>();
        Debug.LogWarning(transform.name + ": LoadBotTimeAnimClip", gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        state = GetState();
        if (state == _currentState) return;
        _currentState = state;
        botCtrl.BotAnim.CrossFade(_currentState, 0, 0);
    }
    protected virtual void SetTimeDelayAnim(float Time2Delay)
    {
        timeDelay = Time2Delay;
        botCtrl.BotState.SetFalseSomeThing();
    }

    private int GetState()
    {
        if(!CanGetState()) return _currentState;

        if (botCtrl.BotState.IsDying)
        {
            //Debug.Log("IsDying", gameObject);
            SetTimeDelayAnim(_dieAnimTime);
            Debug.Log("Player has been DIE !", gameObject);
            return Die;
        }
        else if (botCtrl.BotState.IsHurting)
        {
            //Debug.Log("IsHurting", gameObject);
            return SetNewState(Hurt, _hurtAnimTime);
        }
        else if (botCtrl.BotState.IsAttacking)
        {
            //Debug.Log("IsAttacking", gameObject);
            return SetNewState(Attack, _attackAnimTime);
        }
        else if (botCtrl.BotState.IsDodging)
        {
            //Debug.Log("IsDodging", gameObject);
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
