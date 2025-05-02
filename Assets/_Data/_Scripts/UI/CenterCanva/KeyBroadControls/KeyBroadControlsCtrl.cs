using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
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
    [SerializeField] protected Sprite sprite_EMPTY;
    [SerializeField] protected Sprite sprite_runtime;

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
        LoadSprite_EMPTY();
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

    protected virtual void LoadSprite_EMPTY()
    {
        if (this.sprite_EMPTY != null) return;
        sprite_EMPTY = Resources.Load<Sprite>("Sprite_EMPTY");
        Debug.LogWarning(transform.name + ": LoadSprite_EMPTY", gameObject);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        playerIndexType = centerCanva.PlayerIndexType;
        LoadNewDataPlayerFromSO(playerIndexType);
    }
    protected override void OnDisable()
    {
        inFoPlayer.ImagePlayer.sprite = sprite_EMPTY;
    }

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
        ctrlInputs.InputCtrls[0].TxtKey.text = newKeyPair.keyAttack.ToString();
        ctrlInputs.InputCtrls[1].TxtKey.text = newKeyPair.keyDodge.ToString();

        ctrlInputs.InputCtrls[0].KeyDefault.KeyCode = newKeyPair.keyAttack;
        ctrlInputs.InputCtrls[1].KeyDefault.KeyCode = newKeyPair.keyDodge;
    }
    private void SetPlayerIndex(PlayerIndexType playerIndexType)
    {
        this.playerIndexType = playerIndexType;
        LoadNewDataPlayerFromSO(playerIndexType);

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
        }
        if (currentKeyType == KeySkillType.SkillDodge)
        {
            keyPair.keyDodge = e.keyCode;
        }
        SetNewData2SO(keyPair);
        LoadNewDataPlayerFromSO(playerIndexType);
        SetDefautlNoneData();
    }


    public void EventClickKeyBtn(KeySkillType keySkillType, KeyCode newKeyCode)
    {
        CurrentKeyType = keySkillType;
        CurrentKeyCode = newKeyCode;

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
            return null;
        }
        newSOInfoPlayer.CopyDataFromAnotherSO(SaveLoadManager.Instance.GetDataByPlayerIndexType(playerIndexType));
        SetDataForPlayer(newSOInfoPlayer.spriteRef, newSOInfoPlayer.nameP, newSOInfoPlayer.keyPairP);

        keyPair = newSOInfoPlayer.keyPairP;

        return newSOInfoPlayer.keyPairP;
    }

    private void SetDataForPlayer(AssetReference spriteRef, string name, KeyPair keyPair)
    {
        CharCtrl charCtrl = GetCharByPlayerIndexType(playerIndexType);
        if (charCtrl == null) return;

        charCtrl.CharInput.KeyAttack = keyPair.keyAttack;
        charCtrl.CharInput.KeyDodge = keyPair.keyDodge;

        if (name != "") charCtrl.NamePlayer.text = name;

        UpdateNewDataToUI4Player(spriteRef, name, keyPair);
    }
    private void UpdateNewDataToUI4Player(AssetReference spriteRef, string name, KeyPair newKeyPair)
    {
        if (spriteRef == null) Debug.Log("Sprite null", gameObject);

        SetDataPlayer(spriteRef);
        if (inFoPlayer.ImagePlayer.sprite == null) Debug.Log("inFoPlayer Sprite null", gameObject);
        if (name != "") inFoPlayer.TxtNamePlayer.text = name;
        UpdateKey2UI(newKeyPair);
    }
    private AsyncOperationHandle<Sprite> _spriteHandle;

    public void SetDataPlayer(AssetReference spriteRef)
    {
        if (_spriteHandle.IsValid())
            Addressables.Release(_spriteHandle);

        //_spriteHandle = spriteRef.LoadAssetAsync<Sprite>();
        _spriteHandle = Addressables.LoadAssetAsync<Sprite>(spriteRef);

        _spriteHandle.Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                sprite_runtime = handle.Result;
                inFoPlayer.ImagePlayer.sprite = sprite_runtime;
            }
            else
            {
                Debug.LogWarning("Failed to load sprite");
            }
        };
    }

    private void UpdateKey2UI(KeyPair newKeyPair)
    {
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
            return null;
        }
        defaultSOInfoPlayer.CopyDataFromAnotherSO(SaveLoadManager.Instance.GetDataDefaultByPlayerIndexType(playerIndexType));

        SetDataForPlayer(defaultSOInfoPlayer.spriteRef, "", defaultSOInfoPlayer.keyPairP);
        SaveLoadManager.Instance.SaveNewInfoKeyToSO(playerIndexType, defaultSOInfoPlayer.keyPairP);
        keyPair = defaultSOInfoPlayer.keyPairP;

        return defaultSOInfoPlayer.keyPairP;
    }

    //////////////BTN
    public void SetDefaultAllKeyBTN()
    {
        keyPair = LoadefaultDataPlayerFromSO(playerIndexType);
        Debug.Log("SetDefaultKeyBTN called");
        UpdateKey2UI(keyPair);
    }

    public void SetDefaultOneKeyBTN(KeySkillType keySkillType)
    {
        currentKeyType = keySkillType;

        KeyPair newKey = new KeyPair(SaveLoadManager.Instance.GetDataDefaultByPlayerIndexType(playerIndexType).keyPairP.keyAttack,
         SaveLoadManager.Instance.GetDataDefaultByPlayerIndexType(playerIndexType).keyPairP.keyDodge);
        if (currentKeyType == KeySkillType.SkillAttack)
        {
            keyPair.keyAttack = newKey.keyAttack;
        }
        if (currentKeyType == KeySkillType.SkillDodge)
        {
            keyPair.keyDodge = newKey.keyDodge;
        }
        SOInfoPlayer defaultSOInfoPlayer = ScriptableObject.CreateInstance<SOInfoPlayer>();
        defaultSOInfoPlayer.CopyDataFromAnotherSO(SaveLoadManager.Instance.GetDataDefaultByPlayerIndexType(playerIndexType));

        SetDataForPlayer(defaultSOInfoPlayer.spriteRef, "", keyPair);
        SaveLoadManager.Instance.SaveNewInfoKeyToSO(playerIndexType, keyPair);

        UpdateKey2UI(keyPair);
    }
}
