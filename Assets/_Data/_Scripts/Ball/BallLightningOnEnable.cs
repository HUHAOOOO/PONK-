using UnityEngine;

public class BallLightningOnEnable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        GameManager.Instance.SetPlayerIsCanOverlapCircleMeleeAttack(true);
    }
}
