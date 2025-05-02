using UnityEngine;

public class BotInput : CoreMonoBehaviour
{
    [SerializeField] protected BotCtrl botCtrl;

    [SerializeField] protected bool inputAttack;
    [SerializeField] protected bool inputDodge;
    [SerializeField] protected int timeDelaySetFalseInput = 1;
    public bool InputAttack { get => inputAttack; set => inputAttack = value; }
    public bool InputDodge { get => inputDodge; set => inputDodge = value; }

    protected override void OnEnable()
    {
        SetFalse();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBotCtrl();
    }
    protected virtual void LoadBotCtrl()
    {
        if (this.botCtrl != null) return;
        botCtrl = GetComponent<BotCtrl>();
        Debug.LogWarning(transform.name + ": LoadBotCtrl", gameObject);
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
