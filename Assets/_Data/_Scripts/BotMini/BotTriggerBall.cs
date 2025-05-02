using UnityEngine;

public class BotTriggerBall : MonoBehaviour
{
    [SerializeField] protected bool detectedBall;
    public bool DetectedBall { get => detectedBall; set => detectedBall = value; }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamSender ballDamSender = collision.GetComponent<DamSender>();
        if (ballDamSender == null)
        {
            return;
        }
        DetectedBall = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (DetectedBall == false) return;
        DamSender ballDamSender = collision.GetComponent<DamSender>();
        if (ballDamSender == null)
        {
            return;
        }
        DetectedBall = false;
    }
}
