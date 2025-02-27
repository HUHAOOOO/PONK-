using UnityEngine;

public class DamSender : MonoBehaviour
{
    [SerializeField] protected int damSender = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamReceive player = collision.gameObject.GetComponent<DamReceive>();
        if (player == null)
        {
            Debug.Log("player DamReceive null !");
            return;
        } 
        player.TakeDam(damSender);
    }
}
