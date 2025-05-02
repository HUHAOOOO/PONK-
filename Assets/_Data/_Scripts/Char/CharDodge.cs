using UnityEngine;
using UnityEngine.UIElements;

public class CharDodge : CoreMonoBehaviour
{
    [SerializeField] protected CharCtrl _charCtrl;

    [SerializeField] protected float _dodgeAnimTime = 1.0f;
    [SerializeField] protected BoxCollider2D boxCollider2D;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCharCtrl();

        LoadData();
    }
    protected virtual void LoadCharCtrl()
    {
        if (this._charCtrl != null) return;
        _charCtrl = transform.parent.GetComponent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }
    protected virtual void LoadData()
    {
        _dodgeAnimTime = _charCtrl.CharAnimatorCtrl.DodgeAnimTime;

        boxCollider2D = _charCtrl.DamReceive.gameObject.GetComponent<BoxCollider2D>();
        if (boxCollider2D == null) Debug.Log("boxCollider2D null", gameObject);
    }
    void Update()
    {
        if (_charCtrl.DamReceive.IsDie == true) return;

        Dodge();
    }
    private void Dodge()
    {
        if (_charCtrl.CharState.IsDodging)
        {
            Immortal(_dodgeAnimTime);
        }
    }
    public void Immortal(float timeImmortal)
    {
        boxCollider2D.enabled = false;
        Invoke(nameof(OnDamReceiver), timeImmortal);
    }
    private void OnDamReceiver()
    {
        boxCollider2D.enabled = true;
    }
}
