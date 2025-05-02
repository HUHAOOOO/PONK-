using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public abstract class Key : CoreMonoBehaviour
{
    [Header("Key")]
    [SerializeField] protected InputCtrl inputCtrl;

    [SerializeField] protected KeySkillType keySkillDefault;
    [SerializeField] protected KeyCode keyCode = KeyCode.None;
    [SerializeField] protected Button  btn;
    [SerializeField] protected TextMeshProUGUI keyTxt;

    public TextMeshProUGUI KeyTxt { get => keyTxt; set => keyTxt = value; }
    public InputCtrl InputCtrl { get => inputCtrl; set => inputCtrl = value; }
    public KeySkillType KeySkillType { get => keySkillDefault; set => keySkillDefault = value; }
    public KeyCode KeyCode { get => keyCode; set => keyCode = value; }
    protected override void LoadComponents()
    {
        LoadInputCtrl();
        LoadBtn();
        LoadKeyTxt();

        BtnAddOnClickEvent();
    }
    protected override void ResetValue()
    {
        SetKeyDefault();
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
    protected virtual void LoadKeyTxt()
    {
        if (this.keyTxt != null) return;
        keyTxt = GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadKeyTxt", gameObject);
    }
    protected virtual void SetKeyCode(KeyCode keyCode)
    {
        this.keyCode = keyCode;
    }
    public string NameKey()
    {
        return KeySkillTypeExtensions.ToDisplayString(keySkillDefault);
    }

    public virtual void BtnAddOnClickEvent()
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => inputCtrl.CtrlInputs.KeyBroadControlsCtrl.EventClickKeyBtn(keySkillDefault,keyCode));
    }
    public abstract void SetKeyDefault();
}
