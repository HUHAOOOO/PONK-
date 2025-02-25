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

        UpSpeed();//tang toc do dan
    }
    public void UpSpeed()
    {
        if (speedRotate >= 0)
        {
            speedRotate += upSpeedPoint;
        }
        else speedRotate -= upSpeedPoint;
    }

    // Player goi khi danh trung
    public void ChangeDirection()
    {
        speedRotate *= -1;
    }

    public void SetDefaultSpeed()//moi lan Ponk Player se set default
    {
        speedRotate = 40;
    }
}