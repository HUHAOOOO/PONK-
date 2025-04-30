using UnityEngine;

public class ItemFireTrigger : ItemTrigger
{
    //[Header("Item Fire Trigger")]

    protected override void ResetValue()
    {
        indexBall = 2;
        timeReset = 5;
        addSpeedBall = 10;
    }

    protected override void EffectItem(BallCtrl ballCtrl)
    {
        ballCtrl.SetActiveBallsByTime(TypeBall.FireBall, timeReset);
        ballCtrl.BallRotate.SpeedSpecialBall(addSpeedBall, timeReset);

        this.itemCtrl.ItemDespawn.SetItemCanDespawn(true);
    }
}
