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
        //// gia lap Load 2s
        //float duration = 2f;
        //float timer = 0f;

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
            // F : float , 0 : so luong so sau dau phay
            // vd 50,53453% -> 50%

            yield return null;
        }

        // Xong an loadingPanel
        loadingPanel.SetActive(false);
        CANVAS_CTRL.Instance.MENU();
    }


    // Load that
        // SceneManager.LoadSceneAsync("GameScene");
    //private IEnumerator LoadGame()
    //{
    //    AsyncOperation operation = SceneManager.LoadSceneAsync("GameScene");

    //    while (!operation.isDone)
    //    {
    //        float progress = Mathf.Clamp01(operation.progress / 0.9f);
    //        loadingBar.value = progress;

    //        if (loadingText != null)
    //            loadingText.text = (progress * 100f).ToString("F0") + "%";

    //        yield return null;
    //    }
    //}

}
