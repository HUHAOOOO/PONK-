using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelPlayerCtrl : CoreMonoBehaviour
{
    [SerializeField] protected BtnChangeChar btnChangeChar;
    [SerializeField] protected BtnChangeName btnChangeName;
    [SerializeField] protected BtnChangeInputSkill btnChangeInputSkill;
    [SerializeField] protected InputFieldName inputFieldName;

    //public KeyCode KeyDodge { get => keyDodge; set => keyDodge = value; }
    public BtnChangeChar BtnChangeChar { get => btnChangeChar; set => btnChangeChar = value; }
    public BtnChangeName BtnChangeName { get => btnChangeName; set => btnChangeName = value; }
    public BtnChangeInputSkill BtnChangeInputSkill { get => btnChangeInputSkill; set => btnChangeInputSkill = value; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBtnChangeChar();
        LoadBtnChangeName();
        LoadBtnChangeInputSkill();
        LoadInputFieldName();
    }

    private void LoadBtnChangeChar()
    {
        if (btnChangeChar != null) return;
        btnChangeChar = GetComponentInChildren<BtnChangeChar>();
        Debug.LogWarning(transform.name + ": LoadBtnChangeChar", gameObject);
    }
    private void LoadBtnChangeName()
    {
        if (btnChangeName != null) return;
        btnChangeName = GetComponentInChildren<BtnChangeName>();
        Debug.LogWarning(transform.name + ": LoadBtnChangeName", gameObject);
    }
    private void LoadBtnChangeInputSkill()
    {
        if (btnChangeInputSkill != null) return;
        btnChangeInputSkill = GetComponentInChildren<BtnChangeInputSkill>();
        Debug.LogWarning(transform.name + ": LoadBtnChangeInputSkill", gameObject);
    }

    private void LoadInputFieldName()
    {
        if (inputFieldName != null) return;
        inputFieldName = GetComponentInChildren<InputFieldName>();
        Debug.LogWarning(transform.name + ": LoadInputFieldName", gameObject);
        inputFieldName.gameObject.SetActive(false);
    }


    public void SetDataPlayer(PlayerIndexType playerIndexType, Sprite spriteP, string namrP, KeyPair keyPairP)
    {
        btnChangeChar.ImageP.sprite = spriteP;
        btnChangeName.TxtNameP.text = namrP;
        btnChangeInputSkill.KeyPair = keyPairP;
    }


    //BTN
    //Chose new Char
    // khi an vao avatar thi se hien anh cac nhan vat ... co id anh de load cho Player khi vao tran
    // 


    // Rename
    // an vao ten thi se hien inputField de nhap 
    // nhap xong enter -> cap nhat ten va tat GO inputField
    public void BTN_Rename()
    {
        inputFieldName.gameObject.SetActive(true);
        inputFieldName.TextNow();
    }



    // Change input Attack and Dodge 
    // Done UI 
    // [ ] gan 

}
