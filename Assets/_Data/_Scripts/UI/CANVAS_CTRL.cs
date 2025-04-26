using UnityEngine;

public class CANVAS_CTRL : MonoBehaviour
{
    private static CANVAS_CTRL instance;
    public static CANVAS_CTRL Instance => instance;
    [SerializeField] protected GameObject goStartGame;
    [SerializeField] protected GameObject Canvas4Player;
    [SerializeField] protected GameObject CanvasKeyBinding;
    [SerializeField] protected GameObject CanvasMENU;
    private  void Awake()
    {
        if (instance != null) Debug.LogError("only allow 1 GameManager | Singleton");
        CANVAS_CTRL.instance = this;

        //[ ]
        //InitGame();
    }
    private void Start()
    {
        SetFalseAll();
        //MENU();
        Debug.Log("CANVAS_CTRL Start");
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
    }

    public void OptionsCanva()
    {
        SetFalseAll();
        //Canvas4Player.gameObject.SetActive(true);
    }
    public void SetttingONCanvas4Player()
    {
        SetFalseAll();
        Canvas4Player.gameObject.SetActive(true);
    }
    private void SetFalseAll()
    {
        goStartGame.gameObject.SetActive(false);
        Canvas4Player.gameObject.SetActive(false);
        CanvasKeyBinding.gameObject.SetActive(false);
        CanvasMENU.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
