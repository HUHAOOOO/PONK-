using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
public class LoadingManager : MonoBehaviour
{
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Slider loadingBar;
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private float duration = 4f;
    [SerializeField] private float timer = 0f;
    private void Start()
    {
        loadingPanel.SetActive(true);
        StartCoroutine(LoadGame());
    }
    private IEnumerator LoadGame()
    {
        while (timer < duration)
        {
            timer += Time.deltaTime;
            // C1 thang tien
            //float progress = Mathf.Clamp01(timer / duration);
            // C2 start fast . end slow
            float t = Mathf.Clamp01(timer / duration);
            float progress = 1 - (1 - t) * (1 - t);// Easing kieu "Ease Out"
            loadingBar.value = progress;

            if (loadingText != null)
                loadingText.text = (progress * 100f).ToString("F0") + "%";
            yield return null;
        }
        loadingPanel.SetActive(false);
        CANVAS_CTRL.Instance.MENU();
    }
}
