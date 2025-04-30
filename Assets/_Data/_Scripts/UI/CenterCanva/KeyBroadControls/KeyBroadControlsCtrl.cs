using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class KeyBroadControlsCtrl : CoreMonoBehaviour
{
    [SerializeField] protected CenterCanva centerCanva;

    [SerializeField] protected PlayerIndexType playerIndexType = PlayerIndexType.None;
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

    public CenterCanva CenterCanva => centerCanva;
    public KeySkillType CurrentKeyType { get => currentKeyType; set => currentKeyType = value; }
    public KeyCode CurrentKeyCode { get => currentKeyCode; set => currentKeyCode = value; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCenterCanva();

        LoadGoContens();

        LoadInFoPlayer();
        LoadCtrlInputs();
        LoadBtnResetAllKey();
        LoadBtnExit();
        LoadPanelWaitPressTheNewKey();
        LoadTxtPressTheNewKey();
    }

    protected virtual void LoadCenterCanva()
    {
        if (this.centerCanva != null) return;
        centerCanva = this.transform.parent.parent.GetComponent<CenterCanva>();
        Debug.LogWarning(transform.name + ": LoadCenterCanva", gameObject);
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
        //SetPlayerIndex(playerIndexType);// sd de test thoi 
    }
    //protected override void Start()
    //{
    //    LoadDataPlayerFromSO();
    //}

    protected override void OnEnable()
    {
        base.OnEnable();
        playerIndexType = centerCanva.PlayerIndexType;
        LoadNewDataPlayerFromSO(playerIndexType);
    }


    //// [ ] an nut chon P thi se goi 1 lan de lay deffault key
    //public void SetDefaultKey()//(PlayerIndexType playerIndexType)
    //{

    //    int index = GetIndexByPlayerIndexType(playerIndexType);


    //    if (index >= 0 && index < InputManager.Instance.PlayerKC.Count)
    //    {
    //        keyPair = InputManager.Instance.PlayerKC[index].Clone();
    //        //#if UNITY_EDITOR
    //        //            UnityEditor.EditorUtility.SetDirty(this);
    //        //#endif
    //        //keyPair.keyAttack = InputManager.Instance.PlayerKC[index].keyAttack;//.Clone();
    //        //keyPair.keyDodge = InputManager.Instance.PlayerKC[index].keyDodge;//.Clone();
    //    }
    //    else
    //    {
    //        keyPair = new KeyPair(KeyCode.F, KeyCode.F);
    //    }
    //    UpdateDataToUI(keyPair);
    //}

    //
    private void SetKeyForPlayer(KeyCode keyAttack, KeyCode keyDodge)
    {
        CharCtrl charCtrl = GetCharByPlayerIndexType(playerIndexType);
        if (charCtrl == null) return;

        charCtrl.CharInput.KeyAttack = keyAttack;
        charCtrl.CharInput.KeyDodge = keyDodge;

        UpdateDataToUI(keyPair);
    }

    private void UpdateDataToUI(KeyPair newKeyPair)
    {
        // Key
        ctrlInputs.InputCtrls[0].TxtKey.text = newKeyPair.keyAttack.ToString();
        ctrlInputs.InputCtrls[1].TxtKey.text = newKeyPair.keyDodge.ToString();

        ctrlInputs.InputCtrls[0].KeyDefault.KeyCode = newKeyPair.keyAttack;
        ctrlInputs.InputCtrls[1].KeyDefault.KeyCode = newKeyPair.keyDodge;
    }
    private void SetPlayerIndex(PlayerIndexType playerIndexType)
    {
        this.playerIndexType = playerIndexType;
        LoadNewDataPlayerFromSO(playerIndexType); // set phat load cai moi luon 

    }

    //// Da co Extantion -> [ ] XOA
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
        //SetDataForPlayer(keyPair.keyAttack, keyPair.keyDodge);
        //SetKeyForPlayer(keyPair.keyAttack, keyPair.keyDodge);
        SetNewData2SO(keyPair);
        LoadNewDataPlayerFromSO(playerIndexType);
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


    /// <summary>
    /// SO
    /// </summary>
    /// 
    private void SetNewData2SO(KeyPair keyPair)
    {
        SaveLoadManager.Instance.SaveNewInfoKeyToSO(playerIndexType, keyPair);
    }

    private KeyPair LoadNewDataPlayerFromSO(PlayerIndexType playerIndexType)
    {
        if (SaveLoadManager.Instance == null)
        {
            Debug.Log(" SaveLoadManager.Instance == null ");
            return null;
        }
        SOInfoPlayer newSOInfoPlayer = ScriptableObject.CreateInstance<SOInfoPlayer>();
        if (SaveLoadManager.Instance.GetDataByPlayerIndexType(playerIndexType) == null)
        {
            //Debug.Log("NULL newSOInfoPlayer");
            return null;
        }
        //newSOInfoPlayer = (SaveLoadManager.Instance.GetDataByPlayerIndexType(playerIndexType));
        newSOInfoPlayer.CopyDataFromAnotherSO(SaveLoadManager.Instance.GetDataByPlayerIndexType(playerIndexType));

        SetDataForPlayer(newSOInfoPlayer.spriteP, newSOInfoPlayer.nameP, newSOInfoPlayer.keyPairP);

        keyPair = newSOInfoPlayer.keyPairP;

        return newSOInfoPlayer.keyPairP;
    }

    private void SetDataForPlayer(Sprite sprite, string name, KeyPair keyPair)
    {
        //Char
        CharCtrl charCtrl = GetCharByPlayerIndexType(playerIndexType);
        if (charCtrl == null) return;

        //charCtrl.CharSpriteRenderer.sprite = sprite;

        charCtrl.CharInput.KeyAttack = keyPair.keyAttack;
        charCtrl.CharInput.KeyDodge = keyPair.keyDodge;

        if (name != "") charCtrl.NamePlayer.text = name;

        //UI
        UpdateNewDataToUI4Player(sprite, name, keyPair);
    }
    private void UpdateNewDataToUI4Player(Sprite sprite, string name, KeyPair newKeyPair)
    {
        //sprite
        inFoPlayer.ImagePlayer.sprite = sprite;
        // name
        if (name != "") inFoPlayer.TxtNamePlayer.text = name;
        UpdateKey2UI(newKeyPair);
    }


    private void UpdateKey2UI(KeyPair newKeyPair)
    {
        // Key
        ctrlInputs.InputCtrls[0].TxtKey.text = newKeyPair.keyAttack.ToString();
        ctrlInputs.InputCtrls[1].TxtKey.text = newKeyPair.keyDodge.ToString();

        ctrlInputs.InputCtrls[0].KeyDefault.KeyCode = newKeyPair.keyAttack;
        ctrlInputs.InputCtrls[1].KeyDefault.KeyCode = newKeyPair.keyDodge;
    }

    private KeyPair LoadefaultDataPlayerFromSO(PlayerIndexType playerIndexType)
    {
        if (SaveLoadManager.Instance == null)
        {
            Debug.Log(" SaveLoadManager.Instance == null ");
            return null;
        }
        SOInfoPlayer defaultSOInfoPlayer = ScriptableObject.CreateInstance<SOInfoPlayer>();
        if (SaveLoadManager.Instance.GetDataDefaultByPlayerIndexType(playerIndexType) == null)
        {
            //Debug.Log("NULL newSOInfoPlayer");
            return null;
        }
        //newSOInfoPlayer = (SaveLoadManager.Instance.GetDataByPlayerIndexType(playerIndexType));
        defaultSOInfoPlayer.CopyDataFromAnotherSO(SaveLoadManager.Instance.GetDataDefaultByPlayerIndexType(playerIndexType));

        SetDataForPlayer(defaultSOInfoPlayer.spriteP, "", defaultSOInfoPlayer.keyPairP);
        SaveLoadManager.Instance.SaveNewInfoKeyToSO(playerIndexType, defaultSOInfoPlayer.keyPairP);
        keyPair = defaultSOInfoPlayer.keyPairP;

        return defaultSOInfoPlayer.keyPairP;
    }

    //////////////BTN
    public void SetDefaultAllKeyBTN()
    {
        keyPair = LoadefaultDataPlayerFromSO(playerIndexType);

 
        Debug.Log("SetDefaultKeyBTN called");
        //int index = GetIndexByPlayerIndexType(playerIndexType);
        //keyPair.keyAttack = InputManager.Instance.PlayerKC[index].keyAttack;//charCtrl.CharInput.KeyAttack;
        //keyPair.keyDodge = InputManager.Instance.PlayerKC[index].keyDodge;//charCtrl.CharInput.KeyAttack;

        //Debug.Log("keyPair : KeyAttack = " + keyPair.keyAttack + ", KeyDodge = " + keyPair.keyDodge);
        //SetDataForPlayer(keyPair.keyAttack, keyPair.keyDodge);

        //SetKeyForPlayer(keyPair.keyAttack, keyPair.keyDodge);
        UpdateKey2UI(keyPair);
    }

    public void SetDefaultOneKeyBTN(KeySkillType keySkillType)
    {
        //LoadDataPlayerFromSO();

        // set ddang nut nao
        currentKeyType = keySkillType;
        // lay gia tri tu Inputmanager

        int index = GetIndexByPlayerIndexType(playerIndexType);
        KeyPair newKey = new KeyPair(SaveLoadManager.Instance.GetDataDefaultByPlayerIndexType(playerIndexType).keyPairP.keyAttack,
         SaveLoadManager.Instance.GetDataDefaultByPlayerIndexType(playerIndexType).keyPairP.keyDodge);
        if (currentKeyType == KeySkillType.SkillAttack)
        {
            keyPair.keyAttack = newKey.keyAttack;
        }
        if (currentKeyType == KeySkillType.SkillDodge)
        {
            keyPair.keyDodge = newKey.keyDodge;//charCtrl.CharInput.KeyAttack;
        }
        //SetDataForPlayer(keyPair.keyAttack, keyPair.keyDodge);

        //SetKeyForPlayer(keyPair.keyAttack, keyPair.keyDodge);

        // Cap nhat data len cac cho [ ] roi qua 
        SOInfoPlayer defaultSOInfoPlayer = ScriptableObject.CreateInstance<SOInfoPlayer>();
        defaultSOInfoPlayer.CopyDataFromAnotherSO(SaveLoadManager.Instance.GetDataDefaultByPlayerIndexType(playerIndexType));

        SetDataForPlayer(defaultSOInfoPlayer.spriteP, "", keyPair);
        SaveLoadManager.Instance.SaveNewInfoKeyToSO(playerIndexType, keyPair);


        UpdateKey2UI(keyPair);
    }
}
