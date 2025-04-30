using UnityEngine;

public class CharInput : CoreMonoBehaviour
{
    [SerializeField] protected CharCtrl _charCtrl;

    [SerializeField] protected bool inputAttack;
    [SerializeField] protected bool inputDodge;

    [SerializeField] protected int timeDelaySetFalseInput = 1;
    [SerializeField] protected KeyCode keyAttack = KeyCode.A;
    [SerializeField] protected KeyCode keyDodge = KeyCode.S;


    public bool InputAttack { get => inputAttack; }
    public bool InputDodge { get => inputDodge; }
    public KeyCode KeyAttack { get => keyAttack; set => keyAttack = value; }
    public KeyCode KeyDodge { get => keyDodge; set => keyDodge = value; }


    void Update()
    {
        if (_charCtrl.DamReceive.IsDie == true) return;

        GetInput();
    }
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
    protected virtual void GetInput()
    {
        inputAttack = Input.GetKeyDown(keyAttack);
        inputDodge = Input.GetKeyDown(keyDodge);
    }

    public void SetFalseInput()
    {
        Invoke(nameof(SetFalse), timeDelaySetFalseInput);
    }
    public void SetFalse()
    {
        inputAttack = false;
        inputDodge = false;
    }
}
