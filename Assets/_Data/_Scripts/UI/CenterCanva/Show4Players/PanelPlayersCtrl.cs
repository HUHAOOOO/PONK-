using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PanelPlayersCtrl : CoreMonoBehaviour
{
    [SerializeField] protected InFor4PlayerCtrl inFor4PlayerCtrl;
    [SerializeField] protected List<PanelPlayerCtrl> panelPlayerCtrls = new();
    public InFor4PlayerCtrl InFor4PlayerCtrl => inFor4PlayerCtrl;

    public List<PanelPlayerCtrl> PanelPlayerCtrls => panelPlayerCtrls;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPanelPlayerCtrl();
        LoadPanelPlayerCtrls();
    }
    private void LoadPanelPlayerCtrl()
    {
        if (inFor4PlayerCtrl != null) return;
        inFor4PlayerCtrl = transform.parent.parent.GetComponent<InFor4PlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPanelPlayerCtrl", gameObject);
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
