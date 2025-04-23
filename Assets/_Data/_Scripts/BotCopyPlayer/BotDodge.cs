using UnityEngine;
using UnityEngine.UIElements;

public class BotDodge : CoreMonoBehaviour
{

    [SerializeField] protected BotCtrl botCtrl;

    [SerializeField] protected float _dodgeAnimTime = 1.0f;
    [SerializeField] protected BoxCollider2D boxCollider2D;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBotCtrl();

        LoadData();
    }
    protected virtual void LoadBotCtrl()
    {
        if (this.botCtrl != null) return;
        botCtrl = transform.parent.GetComponent<BotCtrl>();
        Debug.LogWarning(transform.name + ": LoadBotCtrl", gameObject);
    }
    protected virtual void LoadData()
    {
        _dodgeAnimTime = botCtrl.BotAnimatorCtrl.DodgeAnimTime;
    }
    void Update()
    {
        Dodge();
    }
    private void Dodge()
    {
        if (botCtrl.BotState.IsDodging)
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
