using UnityEngine;

public class DamSenderBallLightning : DamSender
{
    protected override void EffectSpecialBall(Collider2D collision)
    {
        CharCtrl charCtrl = collision.transform.parent.GetComponent<CharCtrl>();
        if (charCtrl == null)
        {
            Debug.Log("charCtrl null !");
            return;
        }
        charCtrl.DamReceive.TakeDam(damSender);
        ballCtrl.BallRotate.SetDefaultSpeed();
    }
    //{
    //    DamReceive playerDamReceive = collision.gameObject.GetComponent<DamReceive>();
    //    if (playerDamReceive == null)
    //    {
    //        Debug.Log("player DamReceive null !");
    //        return;
    //    }
    //    playerDamReceive.TakeDam(damSender);
    //    ballCtrl.BallRotate.SetDefaultSpeed();
    //}
}
