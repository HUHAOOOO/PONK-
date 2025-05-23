using System.Collections.Generic;
using UnityEngine;

public class InFor4PlayerCtrl : CoreMonoBehaviour
{
    [SerializeField] protected CenterCanva centerCanva;

    [SerializeField] protected PanelPlayersCtrl panelPlayersCtrl;
    [SerializeField] protected List<SOInfoPlayer> soDefaultInfoPlayers;
    [SerializeField] protected List<SOInfoPlayer> soNewInfoPlayers;

    public CenterCanva CenterCanva => centerCanva;
    public PanelPlayersCtrl PanelPlayersCtrl => panelPlayersCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCenterCanva();
        LoadPanelPlayersCtrl();
        LoadSODefaultInfoPlayers();
        LoadSONewInfoPlayers();
    }
    protected virtual void LoadCenterCanva()
    {
        if (this.centerCanva != null) return;
        centerCanva = this.transform.parent.parent.GetComponent<CenterCanva>();
        Debug.LogWarning(transform.name + ": LoadCenterCanva", gameObject);
    }
    protected virtual void LoadPanelPlayersCtrl()
    {
        if (this.panelPlayersCtrl != null) return;
        panelPlayersCtrl = GetComponentInChildren<PanelPlayersCtrl>();
        Debug.LogWarning(transform.name + ": LoadPanelPlayersCtrl", gameObject);
    }
    protected virtual void LoadSODefaultInfoPlayers()
    {
        if (this.soDefaultInfoPlayers.Count > 0) return;

        soDefaultInfoPlayers = SaveLoadManager.Instance.SODefaultInfoPlayers;
        Debug.LogWarning(transform.name + ": LoadSODefaultInfoPlayers", gameObject);
    }
    protected virtual void LoadSONewInfoPlayers()
    {
        if (this.soNewInfoPlayers.Count > 0) return;

        soNewInfoPlayers = SaveLoadManager.Instance.SONewInfoPlayers;
        Debug.LogWarning(transform.name + ": LoadSONewInfoPlayers", gameObject);
    }

    protected override void Awake()
    {
        base.Awake();
        LoadDataPlayer();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        LoadDataPlayer();
    }

    private void LoadDataPlayer()
    {
        if (SaveLoadManager.Instance == null) return;
        List<SOInfoPlayer> soNewInfoPlayers = SaveLoadManager.Instance.SONewInfoPlayers;
        for (int i = 0; i < soNewInfoPlayers.Count; i++)
        {
            SOInfoPlayer soInfoPlayer = soNewInfoPlayers[i];
            panelPlayersCtrl.PanelPlayerCtrls[i].SetDataPlayer(soInfoPlayer.playerIndexType, soInfoPlayer.spriteRef, soInfoPlayer.nameP, soInfoPlayer.keyPairP);
        }
    }
    public void BTN_ResetDefaultInfoPlayer()
    {
        List<SOInfoPlayer> soDefaultInfoPlayers = SaveLoadManager.Instance.SODefaultInfoPlayers;
        for (int i = 0; i < soNewInfoPlayers.Count; i++)
        {
            SOInfoPlayer soInfoPlayer = soDefaultInfoPlayers[i];
            panelPlayersCtrl.PanelPlayerCtrls[i].SetDataPlayer(soInfoPlayer.playerIndexType, soInfoPlayer.spriteRef, soInfoPlayer.nameP, soInfoPlayer.keyPairP);
        }
        SaveLoadManager.Instance.ResetDefaultInfoPlayer();
    }
}
