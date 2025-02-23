using UnityEngine;

public class BallRotate : GORotateParent
{
    [Header("Ball Rotale")]
    [SerializeField] protected int upSpeedPoint = 2;

    protected override void Reset()
    {
        base.Reset();
        speedRotate = 40;
        typeRorate = TypeRotate.z;
    }

    public void SetSpeedRotate(int speed)
    {
        speedRotate = speed;

        //ChangeSpeed();

    }

    //public void ChangeSpeed()
    //{
    //    if (speedRotate > 0)
    //    {
    //        speedRotate += upSpeedPoint;
    //    }
    //    else speedRotate -= upSpeedPoint;
    //}
}