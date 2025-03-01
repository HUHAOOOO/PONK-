using UnityEngine;
using UnityEngine.UIElements;

public class CharDodge : CoreMonoBehaviour
{

    [SerializeField] protected CharCtrl charCtrl;

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
        if (this.charCtrl != null) return;
        charCtrl = transform.parent.GetComponent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }
    protected virtual void LoadData()
    {
        _dodgeAnimTime = charCtrl.CharAnimatorCtrl.DodgeAnimTime;

        boxCollider2D = charCtrl.DamReceive.gameObject.GetComponent<BoxCollider2D>();
        if (boxCollider2D == null) Debug.Log("boxCollider2D null", gameObject);
    }
    void Update()
    {
        Dodge();
    }
    private void Dodge()
    {
        if (charCtrl.CharState.IsDodging)
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
