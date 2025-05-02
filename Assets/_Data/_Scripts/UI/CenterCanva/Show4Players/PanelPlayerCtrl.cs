using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class PanelPlayerCtrl : CoreMonoBehaviour
{
    [SerializeField] protected PanelPlayersCtrl panelPlayersCtrl;
    [SerializeField] protected PlayerIndexType playerIndexType;
    [SerializeField] protected BtnChangeChar btnChangeChar;
    [SerializeField] protected BtnChangeName btnChangeName;
    [SerializeField] protected BtnChangeInputSkill btnChangeInputSkill;
    [SerializeField] protected InputFieldName inputFieldName;

    [SerializeField] protected Sprite sprite_runtime;

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

    private AsyncOperationHandle<Sprite> _spriteHandle;

    public void SetDataPlayer(PlayerIndexType playerIndexType, AssetReference spriteRef, string namrP, KeyPair keyPairP)
    {
        if (_spriteHandle.IsValid())
            Addressables.Release(_spriteHandle);

        // Load sprite
        //_spriteHandle = spriteRef.LoadAssetAsync<Sprite>();
        _spriteHandle = Addressables.LoadAssetAsync<Sprite>(spriteRef);

        _spriteHandle.Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                sprite_runtime = handle.Result;
                btnChangeChar.ImageP.sprite = sprite_runtime;
            }
            else
            {
                Debug.LogWarning("Failed to load sprite");
            }
        };

        btnChangeName.TxtNameP.text = namrP;
        btnChangeInputSkill.KeyPair = keyPairP.Clone();
    }

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
    }
}
