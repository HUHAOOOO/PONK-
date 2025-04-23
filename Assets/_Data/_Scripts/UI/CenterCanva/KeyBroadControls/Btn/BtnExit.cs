using UnityEngine;
using UnityEngine.UI;

public class BtnExit : BtnCore
{
    [SerializeField] protected Transform goParent;

    [SerializeField] protected Button btn;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadGoParent();
        LoadBtn();
    }
    protected virtual void LoadGoParent()
    {
        if (this.goParent != null) return;
        goParent = transform.parent.parent.GetComponent<Transform>();
        Debug.LogWarning(transform.name + ": LoadGoParent", gameObject);
    }
    protected virtual void LoadBtn()
    {
        if (this.btn != null) return;
        btn = GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtn", gameObject);
    }
    public override void BtnAddOnClickEvent()
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => SetActiveGoParent());
    }
    //BTN
    private void SetActiveGoParent()
    {
        goParent.gameObject.SetActive(false);
    }
}

