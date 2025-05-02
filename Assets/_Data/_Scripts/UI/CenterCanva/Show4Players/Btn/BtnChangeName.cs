using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnChangeName : BtnCore
{
    [SerializeField] protected PanelPlayerCtrl panelPlayerCtrl;
    [SerializeField] protected Button btnNameP;
    [SerializeField] protected TextMeshProUGUI txtNameP;

    public Button BtnNameP { get => btnNameP; set => btnNameP = value; }
    public TextMeshProUGUI TxtNameP { get => txtNameP; set => txtNameP = value; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPanelPlayerCtrl();
        LoadBtnNameP();
        LoadTextNameP();
    }
    public override void BtnAddOnClickEvent()
    {
        btnNameP.onClick.RemoveAllListeners();
        btnNameP.onClick.AddListener(() => panelPlayerCtrl.BTN_Rename());
    }
    private void LoadPanelPlayerCtrl()
    {
        if (panelPlayerCtrl != null) return;
        panelPlayerCtrl = transform.parent.GetComponent<PanelPlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPanelPlayerCtrl", gameObject);
    }
    private void LoadBtnNameP()
    {
        if (btnNameP != null) return;
        btnNameP = GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnNameP", gameObject);
    }
    private void LoadTextNameP()
    {
        if (txtNameP != null) return;
        txtNameP = transform.Find("TextNameP").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadTextNameP", gameObject);
    }
}
