using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnResetAllDataPlayer : BtnCore
{
    [SerializeField] protected InFor4PlayerCtrl panelPlayerCtrl;
    [SerializeField] protected Button btnResetAll;
    public Button BtnResetAll { get => btnResetAll; set => btnResetAll = value; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPanelPlayerCtrl();
        LoadBtnResetAll();
    }
    public override void BtnAddOnClickEvent()
    {
        btnResetAll.onClick.RemoveAllListeners();
        btnResetAll.onClick.AddListener(() => panelPlayerCtrl.BTN_ResetDefaultInfoPlayer());
    }
    private void LoadPanelPlayerCtrl()
    {
        if (panelPlayerCtrl != null) return;
        panelPlayerCtrl = transform.parent.parent.GetComponent<InFor4PlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPanelPlayerCtrl", gameObject);
    }
    private void LoadBtnResetAll()
    {
        if (btnResetAll != null) return;
        btnResetAll = GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnResetAll", gameObject);
    }
}
