using UnityEngine;

public class ItemLightningTrigger : ItemTrigger
{
    protected override void ResetValue()
    {
        indexBall = 1;
        timeReset = 5;
        addSpeedBall = 20;
    }
    protected override void EffectItem(BallCtrl ballCtrl)
    {
        ballCtrl.SetActiveBallsByTime(TypeBall.LightningBall, timeReset);
        ballCtrl.BallRotate.SpeedSpecialBall(addSpeedBall, timeReset);

        this.itemCtrl.ItemDespawn.SetItemCanDespawn(true);
    }
}
