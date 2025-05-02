using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldName : CoreMonoBehaviour
{
    [SerializeField] protected PanelPlayerCtrl panelPlayerCtrl;

    [SerializeField] protected TMP_InputField tMP_InputField;
    [SerializeField] protected string inputNewName;
    public TMP_InputField TMP_InputField => tMP_InputField;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPanelPlayerCtrl();

        LoadTMP_InputField();
    }
    protected override void Awake()
    {
        base.Awake();
        AddEvent();
        this.gameObject.SetActive(false);
    }
    private void LoadPanelPlayerCtrl()
    {
        if (panelPlayerCtrl != null) return;
        panelPlayerCtrl = transform.parent.GetComponent<PanelPlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPanelPlayerCtrl", gameObject);
    }
    private void LoadTMP_InputField()
    {
        if (tMP_InputField != null) return;
        tMP_InputField = GetComponent<TMP_InputField>();
        Debug.LogWarning(transform.name + ": LoadTMP_InputField", gameObject);
    }

    // nhap xong input an enter la bien mat luon di

    private void AddEvent()
    {
        tMP_InputField.onEndEdit.AddListener(OnNameInputEndEdit);
        //tMP_InputField.onValueChanged.AddListener((text) =>
        //{
        //    tMP_InputField.text = text;
        //});
        //tMP_InputField.onSubmit.AddListener((text) =>
        //{
        //    tMP_InputField.text = text;
        //});
    }

    private void OnNameInputEndEdit(string inputText)
    {
        if (inputText == "")
        {
            this.gameObject.SetActive(false);
            return;
        }
        panelPlayerCtrl.BtnChangeName.TxtNameP.text = inputText;

        tMP_InputField.text = "";
        panelPlayerCtrl.UpdateDataForPlayer();
        this.gameObject.SetActive(false);
    }

    public void TextNow()
    {
        StartCoroutine(SelectInputNextFrame());
    }
    private IEnumerator SelectInputNextFrame()
    {
        yield return null; // waite 1 frame
        tMP_InputField.Select();// chon o input 
        tMP_InputField.ActivateInputField();//kich hoat -> go ngay
    }

}
