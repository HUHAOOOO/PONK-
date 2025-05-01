using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public Camera mainCamera;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 chuot trai
        {
            AudioManager.Instance.PlaySFX("Click");

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f;

            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

            Transform fx = FXSpawner.Instance.Spawn(FXSpawner.FX_MouseClick, worldPos, Quaternion.Euler(0,0,0) );

            fx.gameObject.SetActive(true);
        }
    }
}
