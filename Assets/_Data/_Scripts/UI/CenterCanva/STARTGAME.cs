using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class STARTGAME : MonoBehaviour
{
    [SerializeField] protected float timer = 1f;
    [SerializeField] protected float timeDelay = 4f;
    [SerializeField] protected GameObject goCountDown;
    [SerializeField] protected TextMeshProUGUI txtCountDown;
    public int soluongPlayer;


    [SerializeField] protected GameObject goTargetBall;

    void OnEnable()
    {
        if (CANVAS_CTRL.Instance == null) return;
        soluongPlayer = CANVAS_CTRL.Instance.soLuongPlayer;

        if (GameManager.Instance == null) return;
        GameManager.Instance.SetDefaultData();
        GameManager.Instance.SetActivePlayer(soluongPlayer);
        GameManager.Instance.InitGame();

        goCountDown.gameObject.SetActive(true);
        StartCoroutine(DemNguocVaoGame());
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    IEnumerator DemNguocVaoGame()
    {
        yield return null; // cho 1 frame de unity setup moi thu
        Time.timeScale = 0f;
        int count = 3;

        while (count > 0)
        {
            goTargetBall.gameObject.SetActive(true);
            AudioManager.Instance.PlaySFX("CountDown");
            txtCountDown.text = count.ToString();
            yield return new WaitForSecondsRealtime(1f);
            count--;
        }

        txtCountDown.text = "GO!";
        AudioManager.Instance.PlaySFX("GOTUYTT");
        yield return new WaitForSecondsRealtime(0.5f);

        txtCountDown.text = "";
        goCountDown.gameObject.SetActive(false);
        goTargetBall.gameObject.SetActive(false);
        InputManager.Instance.SetInputForPlayer();
        Time.timeScale = 1f;
    }
}
