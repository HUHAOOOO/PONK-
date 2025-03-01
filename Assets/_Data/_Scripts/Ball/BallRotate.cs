using UnityEngine;

public class BallRotate : GORotateParent
{
    [Header("Ball Rotale")]
    [SerializeField] protected int upSpeedPoint = 10;
    [SerializeField] protected int defaultSpeed = 100;

    protected override void Reset()
    {
        base.Reset();
        speedRotate = 100;
        typeRorate = TypeRotate.z;
    }

    //public void SetSpeedRotate(int speed)
    //{
    //    speedRotate = speed;

    //    //UpSpeed();//tang toc do dan
    //}
    public void UpSpeed()
    {
        if (speedRotate >= 0)
        {
            speedRotate += upSpeedPoint;
        }
        else speedRotate -= upSpeedPoint;
    }

    // Player goi khi danh trung
    public void ChangeDirection(int minus = -1)
    {
        speedRotate *= minus;
        UpSpeed();
    }

    public void SetDefaultSpeed()//moi lan Ponk Player se set default
    {

        if (speedRotate >= 0)
        {
            speedRotate = defaultSpeed;
        }
        else if (speedRotate < 0)
        {
            speedRotate = -defaultSpeed;
        }
    }
}