using UnityEngine;

public class CANVAS_CTRL : MonoBehaviour
{
    private static CANVAS_CTRL instance;
    public static CANVAS_CTRL Instance => instance;
    [SerializeField] protected GameObject goStartGame;
    [SerializeField] protected GameObject Canvas4PlayerCtrl;
    [SerializeField] protected GameObject CanvasKeyBindingCtrl;
    [SerializeField] protected GameObject CanvasMENU;
    [SerializeField] protected GameObject CanvasOPTIONS;
    [SerializeField] protected CanvasENDGAME canvasENDGAME;
    public int soLuongPlayer;
    private  void Awake()
    {
        if (instance != null) Debug.LogError("only allow 1 GameManager | Singleton");
        CANVAS_CTRL.instance = this;
    }
    private void Start()
    {
        SetFalseAll();
    }
    public void MENU()
    {
        SetFalseAll();
        CanvasMENU.gameObject.SetActive(true);
    }
    public void StartGame()
    {
        SetFalseAll();
        goStartGame.gameObject.SetActive(true);

        GameManager.Instance.DataCHarIntoGame();
        InputManager.Instance.UpdateKey4Pkayer();
    }

    public void OptionsCanva()
    {
        SetFalseAll();
        CanvasOPTIONS.gameObject.SetActive(true);
    }
    public void SetttingONCanvas4Player()
    {
        SetFalseAll();
        Canvas4PlayerCtrl.gameObject.SetActive(true);
    }

    public void Nut1()
    {
        soLuongPlayer = 1;
        StartGame();
    }
    public void Nut2()
    {
        soLuongPlayer = 2;
        StartGame();
    }
    public void Nut3()
    {
        soLuongPlayer = 3;
        StartGame();
    }
    public void Nut4()
    {
        soLuongPlayer = 4;
        StartGame();
    }

    private void SetFalseAll()
    {
        goStartGame.gameObject.SetActive(false);
        Canvas4PlayerCtrl.gameObject.SetActive(false);
        CanvasKeyBindingCtrl.gameObject.SetActive(false);
        CanvasMENU.gameObject.SetActive(false);

        CanvasPauseManu.SetActive(false);
        canvasENDGAME.gameObject.SetActive(false);
        CanvasOPTIONS.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    ////////// PAUSE MENU
    [SerializeField] protected GameObject CanvasPauseManu;
    [SerializeField] protected bool isGamePaused = false;


    public void Resume()
    {
        CanvasPauseManu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }
    public void Pause()
    {
        CanvasPauseManu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }
    //// END GAME
    public void EndGame(string nameWINER)
    {
        canvasENDGAME.gameObject.SetActive(true);
        canvasENDGAME.txtNameWiner.text = nameWINER;
    }

    // SAVE DATA btn
    public void Btn_SAVEDATA()
    {
        SaveLoadManager.Instance.SaveEndNewSO();
    }

}
