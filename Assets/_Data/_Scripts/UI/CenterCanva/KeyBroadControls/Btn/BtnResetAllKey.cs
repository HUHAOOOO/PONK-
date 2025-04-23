using UnityEngine;
using UnityEngine.UI;

public class BtnResetAllKey : BtnCore
{
    [SerializeField] protected KeyBroadControlsCtrl keyBroadControlsCtrl;

    [SerializeField] protected Button btn;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadKeyBroadControlsCtrl();
        LoadBtn();
    }
    protected virtual void LoadKeyBroadControlsCtrl()
    {
        if (this.keyBroadControlsCtrl != null) return;
        keyBroadControlsCtrl = transform.parent.parent.GetComponent<KeyBroadControlsCtrl>();
        Debug.LogWarning(transform.name + ": LoadKeyBroadControlsCtrl", gameObject);
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
        btn.onClick.AddListener(() => keyBroadControlsCtrl.SetDefaultAllKeyBTN());
    }
}
