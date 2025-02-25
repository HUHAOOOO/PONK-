using UnityEngine;

public class CharAnimatorCtrl : CoreMonoBehaviour
{
    [SerializeField] protected CharCtrl _charCtrl;
    [SerializeField] protected CharTimeAnimClip charTimeAnimClip;

    //cho ra 1 class rieng dc nhi
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Dodge = Animator.StringToHash("Dodge");
    private static readonly int Die = Animator.StringToHash("Die");

    [SerializeField] protected float _attackAnimTime = 1.0f;
    [SerializeField] protected float _dodgeAnimTime = 1.0f;
    [SerializeField] protected float _dieAnimTime = 1.0f;

    [SerializeField] protected int state;
    [SerializeField] protected int _currentState;

    [SerializeField] protected float timeDelay = 0;
    [SerializeField] protected float timer = 0;


    public CharCtrl CharCtrl
    {
        get => _charCtrl;
        private set => _charCtrl = value;
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
        _charCtrl.CharInput.SetFalseSomeThing();
    }

    private int GetState()
    {
        timer += Time.deltaTime;
        if (timer < timeDelay) return _currentState;
        timer = 0;

        if (_charCtrl.CharInput.IsAttack)
        {
            Debug.Log("_isAttack ");
            SetTimeDelayAnim(_attackAnimTime);
            return Attack;
        }
        else if (_charCtrl.CharInput.IsDodge)
        {
            Debug.Log("_isDodge ");
            SetTimeDelayAnim(_dodgeAnimTime);
            return Dodge;
        }
        else if (_charCtrl.CharInput.IsDie)
        {
            Debug.Log("_isDie ");
            SetTimeDelayAnim(_dieAnimTime);
            return Die;
        }
        //Debug.Log("return Idle !");
        return Idle;//? bi lien tuc goi idle //=> Chi khi het khoa thi Idle moi dc chay
    }
}
