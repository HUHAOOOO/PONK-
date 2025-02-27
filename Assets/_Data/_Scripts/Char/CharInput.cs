using UnityEngine;

public class CharInput : CoreMonoBehaviour
{
    [SerializeField] protected CharCtrl charCtrl;

    [SerializeField] protected bool inputAttack;
    [SerializeField] protected bool inputDodge;

    [SerializeField] protected int timeDelaySetFalseInput = 1;


    public bool InputAttack { get => inputAttack; }
    public bool InputDodge { get => inputDodge; }

    void Update()
    {
        GetInput();
    }
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
    protected virtual void GetInput()
    {
        inputAttack = Input.GetKeyDown(KeyCode.A);
        inputDodge = Input.GetKeyDown(KeyCode.S);
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
