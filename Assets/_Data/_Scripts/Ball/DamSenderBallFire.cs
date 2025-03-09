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
    //{
    //    // Ko bi attack anh huong
    //    Debug.Log("collision : " + collision.name);

    //    CharCtrl charCtrl = collision.transform.parent.GetComponent<CharCtrl>();
    //    if (charCtrl == null)
    //    {
    //        Debug.Log("charCtrl null !");
    //        return;
    //    }

    //    //DamReceive playerDamReceive = collision.gameObject.GetComponent<DamReceive>();
    //    //if (playerDamReceive == null)
    //    //{
    //    //    //Debug.Log("player DamReceive null !");
    //    //    return;
    //    //}

    //    //charCtrl.CharMeleeAttack.CancelInvokeAttack();
    //    charCtrl.CharMeleeAttack.IsCanOverlapCircleMeleeAttack = false; //dat false ... Vay khi nao thi true ... khi ball khac 
    //    //playerDamReceive.TakeDam(damSender);
    //    charCtrl.DamReceive.TakeDam(damSender);
    //    ballCtrl.BallRotate.SetDefaultSpeed();

    //    Debug.Log("DamSenderBallFire SetDefaultSpeed() !");
    //}
}
