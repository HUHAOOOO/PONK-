using UnityEngine;

public class CharImmortalSheild : CoreMonoBehaviour
{
    [SerializeField] protected CharCtrl _charCtrl;
    [SerializeField] protected BoxCollider2D boxCollider2D;
    [SerializeField] protected Transform model;

    [SerializeField] protected bool isSheildOn;
    [SerializeField] protected float timeImmortalSheild = 2;

    [SerializeField] protected float GO_FXArmor;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCharCtrl();
        LoadModel();

        LoadData();
    }
    protected override void ResetValue()
    {
        OnOffFxSheild(false);
    }
    protected virtual void LoadCharCtrl()
    {
        if (this._charCtrl != null) return;
        _charCtrl = transform.parent.GetComponent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }

    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        model = transform.Find("Model").GetComponent<Transform>();
        Debug.LogWarning(transform.name + ": LoadModel", gameObject);
    }

    protected virtual void LoadData()
    {
        boxCollider2D = _charCtrl.DamReceive.gameObject.GetComponent<BoxCollider2D>();
        if (boxCollider2D == null) Debug.Log("boxCollider2D null", gameObject);
    }
    protected override void OnEnable()
    {
        isSheildOn = false;
    }
    protected override void OnDisable()
    {
        isSheildOn = false;
    }
    private void Update()
    {
        if (_charCtrl.DamReceive.IsDie == true) return;

        if (!isSheildOn) return;
        Immortal(timeImmortalSheild);
    }
    public void Immortal(float timeImmortal)
    {
        boxCollider2D.enabled = false;
        Invoke(nameof(EndImmotal), timeImmortal);
        SetBool_TimeImmortalSheild(false);
        OnOffFxSheild(true);
    }
    private void EndImmotal()
    {
        boxCollider2D.enabled = true;
        OnOffFxSheild(false);
    }

    public void SetBool_TimeImmortalSheild(bool boolSheild)
    {
        isSheildOn = boolSheild;
    }
    public void OnOffFxSheild(bool boolFXShield)
    {
        model.gameObject.SetActive(boolFXShield);
    }
}
