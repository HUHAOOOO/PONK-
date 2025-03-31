using UnityEngine;

public class BotOption : MonoBehaviour
{
    [SerializeField] protected BotTypeOption currentBotTyleOption = BotTypeOption.noOption;

    public BotTypeOption CurrentBotTyleOption { get => currentBotTyleOption; set => currentBotTyleOption = value; }

    private void OnEnable()
    {
        RanDomNewOption();
    }

    public void RanDomNewOption()
    {
        CurrentBotTyleOption = BotTypeOption.noOption;

        float rand = Random.Range(0f, 1f);

        if (rand < 0.3f)
        {
            //Debug.Log("TH1 (30%)");
            CurrentBotTyleOption = BotTypeOption.Dodge;
        }
        else
        {
            //Debug.Log("TH2 (70%)");
            CurrentBotTyleOption = BotTypeOption.MeleeAttack;
        }
    }
}

public enum BotTypeOption
{
    noOption = 0,

    Dodge = 1,
    MeleeAttack = 2,
}
