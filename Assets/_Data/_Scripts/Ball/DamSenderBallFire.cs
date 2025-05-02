using UnityEngine;

public class DamSenderBallFire : DamSender
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
}
