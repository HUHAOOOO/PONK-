using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputCtrl : CoreMonoBehaviour
{
    [SerializeField] protected CtrlInputs ctrlInputs;

    [SerializeField] protected TextMeshProUGUI txtSkill;
    [SerializeField] protected Key keyDefault;
    [SerializeField] protected TextMeshProUGUI txtKey;
    [SerializeField] protected Button btnResetKey;

    public CtrlInputs CtrlInputs { get => ctrlInputs; set => ctrlInputs = value; }
    public Key KeyDefault { get => keyDefault; set => keyDefault = value; }
    public TextMeshProUGUI TxtKey { get => txtKey; set => txtKey = value; }

    //public int PosIndex { get => posIndex; set => posIndex = value; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCtrlInputs();

        LoadTxtSkill();
        LoadKeyDefault();
        LoadbtnResetKey();

        RenameTxtSkill();
        RenameGO();
    }
    protected virtual void LoadCtrlInputs()
    {
        if (this.ctrlInputs != null) return;
        ctrlInputs = transform.parent.GetComponent<CtrlInputs>();
        Debug.LogWarning(transform.name + ": LoadCtrlInputs", gameObject);
    }
    protected virtual void LoadTxtSkill()
    {
        if (this.txtSkill != null) return;
        txtSkill = GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadTxtSkill", gameObject);
    }
    protected virtual void LoadKeyDefault()
    {
        if (this.keyDefault != null) return;
        keyDefault = GetComponentInChildren<Key>();
        txtKey = keyDefault.GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadKeyDefault", gameObject);
        Debug.LogWarning(transform.name + ": LoadTxtKey", gameObject);

    }
    protected virtual void LoadbtnResetKey()
    {
        if (this.btnResetKey != null) return;
        btnResetKey = transform.Find("ButtonResetKey").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadbtnResetKey", gameObject);
    }

    private void RenameTxtSkill()
    {
        txtSkill.text = keyDefault.NameKey();
    }
    private void RenameGO()
    {
        this.gameObject.name = "Input" + keyDefault.NameKey();
        keyDefault.gameObject.name = "Key" + keyDefault.NameKey();
    }
    public void SetTxtKeyDefault(string key)
    {
        txtKey.text = key;
    }
}
