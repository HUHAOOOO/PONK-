using UnityEngine;

public class BotTriggerBall : MonoBehaviour
{
    [SerializeField] protected bool detectedBall;
    public bool DetectedBall { get => detectedBall; set => detectedBall = value; }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamSender ballDamSender = collision.GetComponent<DamSender>();
        if (ballDamSender == null) Debug.Log("BotTrigger ballDamSender = null !");

        DetectedBall = true;
        Debug.Log("DetectedBall = true;");

        // true phat bien mat luon can gi false :V 
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        DamSender ballDamSender = collision.GetComponent<DamSender>();
        if (ballDamSender == null) Debug.Log("BotTrigger ballDamSender = null !");

        DetectedBall = false;// false de lan sau sd tiep thoi
        Debug.Log("DetectedBall = false;");
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    DamSender ballDamSender = collision.GetComponent<DamSender>();
    //    if (ballDamSender == null) Debug.Log("BotTrigger ballDamSender = null !");

    //    DetectedBall = false;// false de lan sau sd tiep thoi
    //    Debug.Log("DetectedBall = false;");
    //}
}
