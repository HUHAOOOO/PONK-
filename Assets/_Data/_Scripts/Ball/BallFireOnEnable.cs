using UnityEngine;

public class BallFireOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.SetPlayerIsCanOverlapCircleMeleeAttack(false);
    }
}
