using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnChangeInputSkill : BtnCore
{

    [SerializeField] protected KeyPair keyPair;
    [SerializeField] protected TextMeshProUGUI txtAttack;
    [SerializeField] protected TextMeshProUGUI txtDodge;
    public KeyPair KeyPair
    {
        get
        {
            return keyPair;
        }
        set
        {
            keyPair = value;
            txtAttack.text = "Attack " + keyPair.keyAttack.ToString();
            txtDodge.text = "Dodge " + keyPair.keyDodge.ToString();
        }
    }
    public TextMeshProUGUI TxtAttack { get => txtAttack; set => txtAttack = value; }
    public TextMeshProUGUI TxtDodge { get => txtDodge; set => txtDodge = value; }

    public override void BtnAddOnClickEvent()
    {
        //throw new System.NotImplementedException();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTextAttack();
        LoadTextDoddge();
    }

    private void LoadTextAttack()
    {
        if (txtAttack != null) return;
        txtAttack = transform.Find("TextAttack").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadTextAttack", gameObject);
    }
    private void LoadTextDoddge()
    {
        if (txtDodge != null) return;
        txtDodge = transform.Find("TextDodge").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadTextDoddge", gameObject);
    }

}
