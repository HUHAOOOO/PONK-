using System.Collections.Generic;
using UnityEngine;

public class CenterCanva : CoreMonoBehaviour
{
    [SerializeField] protected InFor4PlayerCtrl inFor4PlayerCtrl;
    [SerializeField] protected KeyBroadControlsCtrl keyBroadControlsCtrl;

    [SerializeField] protected PlayerIndexType playerIndexType;

    public PlayerIndexType PlayerIndexType { get => playerIndexType; set => playerIndexType = value; }

    public InFor4PlayerCtrl InFor4PlayerCtrl => inFor4PlayerCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadInFor4PlayerCtrl();
        LoadKeyBroadControlsCtrl();
    }
    protected virtual void LoadInFor4PlayerCtrl()
    {
        if (this.inFor4PlayerCtrl != null) return;
        inFor4PlayerCtrl = GetComponentInChildren<InFor4PlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadInFor4PlayerCtrl", gameObject);
    }
    protected virtual void LoadKeyBroadControlsCtrl()
    {
        if (this.keyBroadControlsCtrl != null) return;
        keyBroadControlsCtrl = GetComponentInChildren<KeyBroadControlsCtrl>();
        Debug.LogWarning(transform.name + ": LoadKeyBroadControlsCtrl", gameObject);
    }

    protected override void Awake()
    {
        //SetActiveGOKInFor4PlayerCtrl();
    }

    public void SetActiveGOKeyBroadControlsCtrl()
    {
        SetActiveFalseAll();
        keyBroadControlsCtrl.gameObject.SetActive(true);
    }
    public void SetActiveGOKInFor4PlayerCtrl()
    {
        SetActiveFalseAll();
        inFor4PlayerCtrl.gameObject.SetActive(true);
    }

    private void SetActiveFalseAll()
    {
        inFor4PlayerCtrl.gameObject.SetActive(false);
        keyBroadControlsCtrl.gameObject.SetActive(false);
    }
}
