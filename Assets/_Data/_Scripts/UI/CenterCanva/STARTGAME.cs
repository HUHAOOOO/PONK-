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



    //protected virtual void TimeDelay()
    //{
    //    timer += Time.deltaTime;
    //    if (timer < timeDelay) return;
    //    timer = 0;
    //}

    //private void OnEnable()
    //{
    //    goCountDown.gameObject.SetActive(true);
    //    timeDelay = 4;
    //}

    //private void Update()
    //{
    //    timeDelay -= Time.deltaTime;

    //    int x = (int)timeDelay;
    //    txtCountDown.text = x.ToString();

    //    if (timer > timeDelay) return;
    //    //timer = 0;
    //    goCountDown.gameObject.SetActive(false);

    //}



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
        Time.timeScale = 1f; // Giai phong dong bang
    }

    IEnumerator DemNguocVaoGame()
    {
        yield return null; // cho 1 frame de unity setup moi thu
        Time.timeScale = 0f; // Dong bang game
        int count = 3;

        while (count > 0)
        {
            txtCountDown.text = count.ToString();
            yield return new WaitForSecondsRealtime(1f); // Dung Realtime
            count--;
        }

        txtCountDown.text = "GO!"; // Hoac "" neu muon an di

        // Cho 1 ti xiu de hien "Start!" roi an
        yield return new WaitForSecondsRealtime(0.5f);

        txtCountDown.text = "";
        goCountDown.gameObject.SetActive(false);
        InputManager.Instance.SetInputForPlayer();
        Time.timeScale = 1f; // Giai phong dong bang
    }



    //void OnEnable()
    //{
    //    Time.timeScale = 0;
    //    StartCoroutine(DemNguocVaoGame());
    //}

    //IEnumerator DemNguocVaoGame()
    //{
    //    int count = 3;

    //    while (count > -1)
    //    {
    //        txtCountDown.text = $"{count}";//$"cbi vao game trong {count}...";
    //        yield return new WaitForSeconds(1f);
    //        count--;
    //    }

    //    //txtCountDown.text = "Start game!";
    //    txtCountDown.text = "";

    //    // het dong bang game 
    //    Time.timeScale = 1;
    //}

}
