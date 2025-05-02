using UnityEngine;
using UnityEngine.UI;

public class HPBarCtrl : CoreMonoBehaviour
{
    [SerializeField] protected CharCtrl charCtrl;
    [SerializeField] protected Slider slider;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCharCtrl();
        LoadSlider();
    }
    protected virtual void LoadCharCtrl()
    {
        if (this.charCtrl != null) return;
        charCtrl = transform.parent.parent.GetComponent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }
    protected virtual void LoadSlider()
    {
        if (this.slider != null) return;
        slider = GetComponent<Slider>();
        Debug.LogWarning(transform.name + ": LoadSlider", gameObject);
    }
    protected override void Start()
    {
        SetMaxHealth(charCtrl.DamReceive.MaxHealthPoints);
    }

    private void Update()
    {
        if (charCtrl.DamReceive.CurrentHealthPoints != slider.value)
        {
            SetHealth(charCtrl.DamReceive.CurrentHealthPoints);
        }
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
