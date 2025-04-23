using UnityEngine;
using UnityEngine.UI;

public class BtnResetOneKey : BtnCore
{
    [SerializeField] protected InputCtrl inputCtrl;

    [SerializeField] protected Button btn;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadInputCtrl();
        LoadBtn();
    }
    protected virtual void LoadInputCtrl()
    {
        if (this.inputCtrl != null) return;
        inputCtrl = transform.parent.GetComponent<InputCtrl>();
        Debug.LogWarning(transform.name + ": LoadInputCtrl", gameObject);
    }
    protected virtual void LoadBtn()
    {
        if (this.btn != null) return;
        btn = GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtn", gameObject);
    }
    public override void BtnAddOnClickEvent()
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => inputCtrl.CtrlInputs.KeyBroadControlsCtrl.SetDefaultOneKeyBTN(inputCtrl.KeyDefault.KeySkillType));
    }
}
