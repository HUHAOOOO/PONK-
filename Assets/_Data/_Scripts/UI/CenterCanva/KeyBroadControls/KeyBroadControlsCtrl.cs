using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KeyBroadControlsCtrl : CoreMonoBehaviour
{
    [SerializeField] protected PlayerIndexType playerIndex = PlayerIndexType.None;
    [SerializeField] protected KeySkillType currentKeyType = KeySkillType.None;
    [SerializeField] protected KeyCode currentKeyCode = KeyCode.None;

    [SerializeField] protected Transform goContents;
    [SerializeField] protected InFoPlayerDum inFoPlayer;
    [SerializeField] protected CtrlInputs ctrlInputs;
    [SerializeField] protected Button btnResetAllKey;
    [SerializeField] protected Button btnExit;

    [SerializeField] protected Transform panelWaitPressTheNewKey;
    [SerializeField] protected TextMeshProUGUI txtPressTheNewKey;
    [SerializeField] protected KeyPair keyPair;


    public KeySkillType CurrentKeyType { get => currentKeyType; set => currentKeyType = value; }
    public KeyCode CurrentKeyCode { get => currentKeyCode; set => currentKeyCode = value; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadGoContens();

        LoadInFoPlayer();
        LoadCtrlInputs();
        LoadBtnResetAllKey();
        LoadBtnExit();
        LoadPanelWaitPressTheNewKey();
        LoadTxtPressTheNewKey();
    }
    protected virtual void LoadGoContens()
    {
        if (this.goContents != null) return;
        goContents = transform.Find("Contents").GetComponent<Transform>();
        Debug.LogWarning(transform.name + ": LoadGoContens", gameObject);
    }

    protected virtual void LoadInFoPlayer()
    {
        if (this.inFoPlayer != null) return;
        inFoPlayer = GetComponentInChildren<InFoPlayerDum>();
        Debug.LogWarning(transform.name + ": LoadInFoPlayer", gameObject);
    }
    protected virtual void LoadCtrlInputs()
    {
        if (this.ctrlInputs != null) return;
        ctrlInputs = GetComponentInChildren<CtrlInputs>();
        Debug.LogWarning(transform.name + ": LoadImagLoadCtrlInputsePlayer", gameObject);
    }

    protected virtual void LoadBtnResetAllKey()
    {
        if (this.btnResetAllKey != null) return;
        btnResetAllKey = goContents.Find("btnResetAllKey").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnResetAllKey", gameObject);
    }
    protected virtual void LoadBtnExit()
    {
        if (this.btnExit != null) return;
        btnExit = goContents.Find("btnExit").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnExit", gameObject);
    }

    protected virtual void LoadPanelWaitPressTheNewKey()
    {
        if (this.panelWaitPressTheNewKey != null) return;
        panelWaitPressTheNewKey = goContents.Find("PanelWaitingForKey").GetComponent<Transform>();
        Debug.LogWarning(transform.name + ": LoadPanelWaitPressTheNewKey", gameObject);
    }
    protected virtual void LoadTxtPressTheNewKey()
    {
        if (this.txtPressTheNewKey != null) return;
        txtPressTheNewKey = goContents.Find("PanelWaitingForKey").Find("TextPressTheNewKey").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadTxtPressTheNewKey", gameObject);
    }
    private void Update()
    {
        SetPlayerIndex(playerIndex);

        //SetDefaultKey();
    }
    protected override void Start()
    {
        SetDefaultKey();
    }
    // [ ] an nut chon P thi se goi 1 lan de lay deffault key
    public void SetDefaultKey()//(PlayerIndexType playerIndexType)
    {

        int index = GetIndexByPlayerIndexType(playerIndex);


        if (index >= 0 && index < InputManager.Instance.PlayerKC.Count)
        {
            keyPair = InputManager.Instance.PlayerKC[index].Clone();
//#if UNITY_EDITOR
//            UnityEditor.EditorUtility.SetDirty(this);
//#endif
            //keyPair.keyAttack = InputManager.Instance.PlayerKC[index].keyAttack;//.Clone();
            //keyPair.keyDodge = InputManager.Instance.PlayerKC[index].keyDodge;//.Clone();
        }
        else
        {
            keyPair = new KeyPair(KeyCode.F, KeyCode.F);
        }
        UpdateDataToUI(keyPair);
    }
    private void UpdateDataToUI(KeyPair newKeyPair)
    {
        ctrlInputs.InputCtrls[0].TxtKey.text = newKeyPair.keyAttack.ToString();
        ctrlInputs.InputCtrls[1].TxtKey.text = newKeyPair.keyDodge.ToString();

        ctrlInputs.InputCtrls[0].KeyDefault.KeyCode = newKeyPair.keyAttack;
        ctrlInputs.InputCtrls[1].KeyDefault.KeyCode = newKeyPair.keyDodge;
    }
    //
    private void SetKeyForPlayer(KeyCode keyAttack, KeyCode keyDodge)
    {
        CharCtrl charCtrl = GetCharByPlayerIndexType(playerIndex);
        if (charCtrl == null) return;

        charCtrl.CharInput.KeyAttack = keyAttack;
        charCtrl.CharInput.KeyDodge = keyDodge;

        UpdateDataToUI(keyPair);
    }
    private void SetPlayerIndex(PlayerIndexType playerIndexType)
    {
        this.playerIndex = playerIndexType;
    }
    private int GetIndexByPlayerIndexType(PlayerIndexType playerIndexNow)
    {
        int index = -1;
        if (playerIndexNow == PlayerIndexType.P0) index = 0;
        else if (playerIndexNow == PlayerIndexType.P1) index = 1;
        else if (playerIndexNow == PlayerIndexType.P2) index = 2;
        else if (playerIndexNow == PlayerIndexType.P3) index = 3;
        return index;
    }

    private void OnGUI()
    {
        if (currentKeyCode == KeyCode.None) return;
        if (Event.current.type != EventType.KeyDown) return;

        Event e = Event.current;
        if (!e.isKey) return;

        if (currentKeyType == KeySkillType.SkillAttack)
        {
            keyPair.keyAttack = e.keyCode;
            //ctrlInputs.InputCtrls[0].TxtKey.text = e.keyCode.ToString();
        }
        if (currentKeyType == KeySkillType.SkillDodge)
        {
            keyPair.keyDodge = e.keyCode;
            //ctrlInputs.InputCtrls[1].TxtKey.text = e.keyCode.ToString();
        }
        SetKeyForPlayer(keyPair.keyAttack, keyPair.keyDodge);
        SetDefautlNoneData();
    }


    public void EventClickKeyBtn(KeySkillType keySkillType, KeyCode newKeyCode)
    {
        CurrentKeyType = keySkillType;
        CurrentKeyCode = newKeyCode;

        //UI
        panelWaitPressTheNewKey.gameObject.SetActive(true);
        txtPressTheNewKey.text = "Press the new key for [ " + currentKeyCode.ToString() + " ]";
    }
    private void SetDefautlNoneData()
    {
        currentKeyCode = KeyCode.None;
        panelWaitPressTheNewKey.gameObject.SetActive(false);
    }

    private CharCtrl GetCharByPlayerIndexType(PlayerIndexType playerIndexType)
    {
        int index;

        if (playerIndexType == PlayerIndexType.P0) index = 0;
        else if (playerIndexType == PlayerIndexType.P1) index = 1;
        else if (playerIndexType == PlayerIndexType.P2) index = 2;
        else if (playerIndexType == PlayerIndexType.P3) index = 3;
        else return null;

        return GameManager.Instance.Players[index];
    }



    //////////////BTN
    public void SetDefaultAllKeyBTN()
    {
        Debug.Log("SetDefaultKeyBTN called");
        int index = GetIndexByPlayerIndexType(playerIndex);
        keyPair.keyAttack = InputManager.Instance.PlayerKC[index].keyAttack;//charCtrl.CharInput.KeyAttack;
        keyPair.keyDodge = InputManager.Instance.PlayerKC[index].keyDodge;//charCtrl.CharInput.KeyAttack;
        //Debug.Log(""
        //    + "key Attack : " + InputManager.Instance.PlayerKC[index].keyAttack + " "
        //    + "key Dodge : " + InputManager.Instance.PlayerKC[index].keyDodge);

        //Debug.Log("keyPair : KeyAttack = " + keyPair.keyAttack + ", KeyDodge = " + keyPair.keyDodge);
        SetKeyForPlayer(keyPair.keyAttack, keyPair.keyDodge);
    }

    public void SetDefaultOneKeyBTN(KeySkillType keySkillType)
    {
        // set ddang nut nao
        currentKeyType = keySkillType;
        // lay gia tri tu Inputmanager

        int index = GetIndexByPlayerIndexType(playerIndex);
        if (currentKeyType == KeySkillType.SkillAttack)
        {
            keyPair.keyAttack = InputManager.Instance.PlayerKC[index].keyAttack;
        }
        if (currentKeyType == KeySkillType.SkillDodge)
        {
            keyPair.keyDodge = InputManager.Instance.PlayerKC[index].keyDodge;//charCtrl.CharInput.KeyAttack;
        }
        SetKeyForPlayer(keyPair.keyAttack, keyPair.keyDodge);
    }
}
