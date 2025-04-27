using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnChangeInputSkill : BtnCore
{
    [SerializeField] protected PanelPlayerCtrl panelPlayerCtrl;

    [SerializeField] protected Button btnChangeInputSkll;
    [SerializeField] protected KeyPair keyPair;
    [SerializeField] protected TextMeshProUGUI txtAttack;
    [SerializeField] protected TextMeshProUGUI txtDodge;
    public KeyPair KeyPair
    {
        get
        {
            return keyPair;
        }
        set
        {
            keyPair = value;
            txtAttack.text = "Attack " + keyPair.keyAttack.ToString();
            txtDodge.text = "Dodge " + keyPair.keyDodge.ToString();
        }
    }
    public TextMeshProUGUI TxtAttack { get => txtAttack; set => txtAttack = value; }
    public TextMeshProUGUI TxtDodge { get => txtDodge; set => txtDodge = value; }

    public override void BtnAddOnClickEvent()
    {
        btnChangeInputSkll.onClick.RemoveAllListeners();
        btnChangeInputSkll.onClick.AddListener(() => panelPlayerCtrl.BTN_ReInputAD());
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPanelPlayerCtrl();
        LoadBtnChangeInputSkll();
        LoadTextAttack();
        LoadTextDoddge();
    }
    private void LoadPanelPlayerCtrl()
    {
        if (panelPlayerCtrl != null) return;
        panelPlayerCtrl = transform.parent.GetComponent<PanelPlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPanelPlayerCtrl", gameObject);
    }
    private void LoadBtnChangeInputSkll()
    {
        if (btnChangeInputSkll != null) return;
        btnChangeInputSkll = GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnChangeInputSkll", gameObject);
    }
    private void LoadTextAttack()
    {
        if (txtAttack != null) return;
        txtAttack = transform.Find("TextAttack").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadTextAttack", gameObject);
    }
    private void LoadTextDoddge()
    {
        if (txtDodge != null) return;
        txtDodge = transform.Find("TextDodge").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadTextDoddge", gameObject);
    }

}
