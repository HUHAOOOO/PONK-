using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PanelPlayersCtrl : CoreMonoBehaviour
{
    [SerializeField] protected List<PanelPlayerCtrl> panelPlayerCtrls = new();

    public List<PanelPlayerCtrl> PanelPlayerCtrls => panelPlayerCtrls;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPanelPlayerCtrls();
    }
    protected virtual void LoadPanelPlayerCtrls()
    {
        if (this.panelPlayerCtrls.Count > 0) return;
        Debug.LogWarning(transform.name + ": LoadPanelPlayerCtrls", gameObject);

        foreach (Transform child in this.transform)
        {
            PanelPlayerCtrl chilPanelPlayerCtrl = child.GetComponent<PanelPlayerCtrl>();
            panelPlayerCtrls.Add(chilPanelPlayerCtrl);
            Debug.Log($"pos4Players.Add ({chilPanelPlayerCtrl.name})");
        }
    }
}
