using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InFoPlayerDum : CoreMonoBehaviour
{
    [SerializeField] protected Image imagePlayer;
    [SerializeField] protected TextMeshProUGUI txtNamePlayer;

    //public int PosIndex { get => posIndex; set => posIndex = value; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTxtNamePlayer();
        LoadImagePlayer();
    }

    protected virtual void LoadTxtNamePlayer()
    {
        if (this.txtNamePlayer != null) return;
        txtNamePlayer = GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadTxtNamePlayer", gameObject);
    }
    protected virtual void LoadImagePlayer()
    {
        if (this.imagePlayer != null) return;
        imagePlayer = GetComponentInChildren<Image>();
        Debug.LogWarning(transform.name + ": LoadImagePlayer", gameObject);
    }
}
