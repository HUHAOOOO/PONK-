using UnityEngine;

public class DamSenderBallDefault : DamSender
{
    protected override void EffectSpecialBall(Collider2D collision)
    {
        CharCtrl charCtrl = collision.transform.parent.GetComponent<CharCtrl>();
        if (charCtrl == null)
        {
            //Debug.Log("charCtrl null !");
            return;
        }
        charCtrl.DamReceive.TakeDam(damSender);
        ballCtrl.BallRotate.SetDefaultSpeed();
    }
    //{
    //    //CharCtrl charCtrl = collision.gameObject.transform.parent.GetComponent<CharCtrl>();

    //    //DamReceive playerDamReceive = charCtrl.DamReceive.GetComponent<DamReceive>();
    //    DamReceive playerDamReceive = collision.gameObject.GetComponent<DamReceive>();
    //    if (playerDamReceive == null)
    //    {
    //        Debug.Log("player DamReceive null !");
    //        return;
    //    }

    //    playerDamReceive.TakeDam(damSender);
    //    ballCtrl.BallRotate.SetDefaultSpeed();

    //charCtrl.CharMeleeAttack.CancelInvokeAttack();


    //    charCtrl.CharMeleeAttack.IsCanOverlapCircleMeleeAttack = true;

    //}

}
