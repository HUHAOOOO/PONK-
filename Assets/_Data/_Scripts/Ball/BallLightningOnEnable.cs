using UnityEngine;

public class BallLightningOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.SetPlayerIsCanOverlapCircleMeleeAttack(true);
    }
}
