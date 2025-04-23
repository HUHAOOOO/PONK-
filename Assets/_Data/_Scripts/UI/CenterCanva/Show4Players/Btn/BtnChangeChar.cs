using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnChangeChar : BtnCore
{
    [SerializeField] protected Image imageP;
    public Image ImageP { get => imageP; set => imageP = value; }

    public override void BtnAddOnClickEvent()
    {

    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadImageP();
    }

    private void LoadImageP()
    {
        if (imageP != null) return;
        imageP = transform.Find("ImageP").GetComponent<Image>();
        Debug.LogWarning(transform.name + ": LoadImageP", gameObject);
    }
}
