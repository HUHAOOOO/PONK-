using UnityEngine;

public class BotTriggerPoint : CoreMonoBehaviour
{
    [SerializeField] protected BotCtrl botCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBotCtrl();
    }
    protected virtual void LoadBotCtrl()
    {
        if (this.botCtrl != null) return;
        botCtrl = transform.parent.GetComponent<BotCtrl>();
        Debug.LogWarning(transform.name + ": LoadBotCtrl", gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (botCtrl.BotOption.CurrentBotTyleOption != BotTypeOption.MeleeAttack) return;
        
        DamSender ballDamSender = collision.GetComponent<DamSender>();
        if (ballDamSender == null) Debug.Log("BotTriggerPoint ballDamSender = null !");

        BallCtrl ballctrl = ballDamSender.transform.parent.parent.parent.GetComponent<BallCtrl>();
        if (ballctrl == null) Debug.Log("BotTriggerPoint ballctrl = null !");

        ballctrl.BallRotate.ChangeDirection();

        Transform fx = FXSpawner.Instance.Spawn(FXSpawner.FX_vetchem1, this.transform.position, Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
        fx.gameObject.SetActive(true);

    }

}
