using UnityEngine;

public class BotTriggerBall : MonoBehaviour
{
    [SerializeField] protected bool detectedBall;
    public bool DetectedBall { get => detectedBall; set => detectedBall = value; }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamSender ballDamSender = collision.GetComponent<DamSender>();
        //if (ballDamSender == null) Debug.Log("BotTrigger ballDamSender = null !");
        if (ballDamSender == null)
        {
            return;
        }
        DetectedBall = true;
        //Debug.Log("OnTriggerEnter2D DetectedBall = true collision : " + collision.name);
        //Debug.Log("DetectedBall = true;");

        // true phat bien mat luon can gi false :V 
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (DetectedBall == false) return;
        DamSender ballDamSender = collision.GetComponent<DamSender>();
        //if (ballDamSender == null) Debug.Log("BotTrigger ballDamSender = null !");
        if (ballDamSender == null)
        {
            return;
        }
        DetectedBall = false;// false de lan sau sd tiep thoi
        //Debug.Log("OnTriggerEnter2D DetectedBall = false collision : " + collision.name);
        //Debug.Log("DetectedBall = false;");
    }
}
