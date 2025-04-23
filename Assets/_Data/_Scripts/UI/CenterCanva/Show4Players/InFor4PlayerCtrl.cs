using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InFor4PlayerCtrl : CoreMonoBehaviour
{
    [SerializeField] protected PanelPlayersCtrl panelPlayersCtrl;
    [SerializeField] protected List<SOInfoPlayer> soDefaultInfoPlayers;
    [SerializeField] protected List<SOInfoPlayer> soNewInfoPlayers;

    public PanelPlayersCtrl PanelPlayersCtrl => panelPlayersCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPanelPlayersCtrl();
        LoadSODefaultInfoPlayers();
        LoadSONewInfoPlayers();
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
        SOInfoPlayer[] soDefaultInfoPlayers = Resources.LoadAll<SOInfoPlayer>("SO/DefaultInFoSO");

        this.soDefaultInfoPlayers = soDefaultInfoPlayers.ToList();
        Debug.LogWarning(transform.name + ": LoadSODefaultInfoPlayers", gameObject);
    }
    protected virtual void LoadSONewInfoPlayers()
    {
        if (this.soNewInfoPlayers.Count > 0) return;
        SOInfoPlayer[] soDefaultInfoPlayers = Resources.LoadAll<SOInfoPlayer>("SO/NewInFoSO");

        this.soNewInfoPlayers = soDefaultInfoPlayers.ToList();
        //soNewInfoPlayers = (List<SOInfoPlayer>)soDefaultInfoPlayers.Clone();
        Debug.LogWarning(transform.name + ": LoadSONewInfoPlayers", gameObject);
    }



    protected override void OnEnable()
    {
        base.OnEnable();
        LoadDataPlayer();
    }

    private void LoadDataPlayer()
    {
        for (int i = 0; i < soDefaultInfoPlayers.Count; i++)
        {
            SOInfoPlayer soInfoPlayer = soDefaultInfoPlayers[i];
            panelPlayersCtrl.PanelPlayerCtrls[i].SetDataPlayer(soInfoPlayer.playerIndexType, soInfoPlayer.spriteP, soInfoPlayer.nameP, soInfoPlayer.keyPairP);
        }

    }
}
