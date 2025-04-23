using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CtrlInputs : CoreMonoBehaviour
{
    [SerializeField] protected KeyBroadControlsCtrl keyBroadControlsCtrl;

    [SerializeField] protected List<InputCtrl> inputCtrls = new();
    public List<InputCtrl> InputCtrls { get => inputCtrls; set => inputCtrls = value; }
    public KeyBroadControlsCtrl KeyBroadControlsCtrl { get => keyBroadControlsCtrl; set => keyBroadControlsCtrl = value; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadKeyBroadControlsCtrl();
        LoadInputCtrl();
    }
    protected virtual void LoadKeyBroadControlsCtrl()
    {
        if (this.keyBroadControlsCtrl != null) return;
        keyBroadControlsCtrl = transform.parent.parent.GetComponent<KeyBroadControlsCtrl>();
        Debug.LogWarning(transform.name + ": LoadKeyBroadControlsCtrl", gameObject);
    }
    protected virtual void LoadInputCtrl()
    {
        if (this.inputCtrls.Count > 0) return;
        foreach(Transform child in this.transform)
        {
            InputCtrl inputCtrl = child.GetComponent<InputCtrl>();
            if(inputCtrl == null)
            {
                Debug.LogWarning(transform.name + ": GO nay ko co Component InputCtrl", gameObject);
                return;
            }
            inputCtrls.Add(inputCtrl);
            Debug.Log($"inputCtrls.Add ({inputCtrl.name})");
        }
        Debug.LogWarning(transform.name + ": LoadInputCtrl", gameObject);
    }
}
