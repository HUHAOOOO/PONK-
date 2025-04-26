using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelPlayerCtrl : CoreMonoBehaviour
{
    [SerializeField] protected PanelPlayersCtrl panelPlayersCtrl;
    [SerializeField] protected PlayerIndexType playerIndexType;
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
        LoadPanelPlayersCtrl();
        LoadPlayerIndexType();
        LoadBtnChangeChar();
        LoadBtnChangeName();
        LoadBtnChangeInputSkill();
        LoadInputFieldName();
    }
    private void LoadPanelPlayersCtrl()
    {
        if (panelPlayersCtrl != null) return;
        panelPlayersCtrl = this.transform.parent.GetComponent<PanelPlayersCtrl>();
        Debug.LogWarning(transform.name + ": LoadPanelPlayersCtrl", gameObject);
    }
    private void LoadPlayerIndexType()
    {
        if (playerIndexType != PlayerIndexType.None) return;
        int indexPlayer = StringGetLastNumber.ExtractLastNumber(this.gameObject.name);
        playerIndexType = PlayerIndexTypeExtensions.IndexToPlayerIndexType(indexPlayer);
        Debug.LogWarning(transform.name + ": LoadPlayerIndexType", gameObject);
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
    // NAY NANG CAO v ... ma hay vl .-. MUON 


    // Rename
    // an vao ten thi se hien inputField de nhap 
    // nhap xong enter -> cap nhat ten va tat GO inputField
    public void BTN_Rename()
    {
        inputFieldName.gameObject.SetActive(true);
        inputFieldName.TextNow();
    }

    public void UpdateDataForPlayer()
    {
        SaveLoadManager.Instance.SaveNewInfoToSO(playerIndexType, btnChangeName.TxtNameP.text, btnChangeInputSkill.KeyPair);
    }

    public void BTN_ReInputAD()
    {
        panelPlayersCtrl.InFor4PlayerCtrl.CenterCanva.PlayerIndexType = playerIndexType;
        panelPlayersCtrl.InFor4PlayerCtrl.CenterCanva.SetActiveGOKeyBroadControlsCtrl();
        //panelPlayersCtrl.InFor4PlayerCtrl.CenterCanva.;//SetActive(true);
        //inputFieldName.TextNow();
    }
    // Change input Attack and Dodge 
    // Done UI 
    // [ ] gan 

}
