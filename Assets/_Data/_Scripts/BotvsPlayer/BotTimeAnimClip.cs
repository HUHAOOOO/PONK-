using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class BotTimeAnimClip : CoreMonoBehaviour
{
    [SerializeField] protected BotAnimatorCtrl botAnimatorCtrl;

    [SerializeField] protected float _attackAnimTime;
    [SerializeField] protected float _dodgeAnimTime;
    [SerializeField] protected float _hurtAnimTime;
    [SerializeField] protected float _dieAnimTime;

    public float AtackAnimTime { get => _attackAnimTime; private set => _attackAnimTime = value; }
    public float DodgeAnimTime { get => _dodgeAnimTime; }
    public float HurtAnimTime { get => _hurtAnimTime; }
    public float DieAnimTime { get => _dieAnimTime; }

    protected override void LoadComponents()
    {
        LoadBotAnimatorCtrl();
    }

    protected override void Reset()
    {
        base.Reset();
        LoadAnimClipTime();
    }

    protected virtual void LoadBotAnimatorCtrl()
    {
        if (this.botAnimatorCtrl != null) return;
        botAnimatorCtrl = GetComponent<BotAnimatorCtrl>();
        Debug.LogWarning(transform.name + ": LoadBotAnimatorCtrl", gameObject);
    }

    protected virtual void LoadAnimClipTime()
    {
        Animator botAnim = botAnimatorCtrl.BotCtrl.BotAnim;

        if (botAnim != null)
        {
            AtackAnimTime = GetAnimationClipLength(botAnim, "CharaAttack");
            Debug.Log("time animation Attack: " + AtackAnimTime);

            _dodgeAnimTime = GetAnimationClipLength(botAnim, "CharaDodge");
            Debug.Log("time animation Dodge: " + _dodgeAnimTime);

            _hurtAnimTime = GetAnimationClipLength(botAnim, "CharaHurt");
            Debug.Log("time animation Hurt: " + _hurtAnimTime);

            _dieAnimTime = GetAnimationClipLength(botAnim, "CharaDie");
            Debug.Log("time animation Die: " + _dieAnimTime);
        }
    }

    private float GetAnimationClipLength(Animator animator, string clipName)
    {
        RuntimeAnimatorController AnimCtrl = animator.runtimeAnimatorController;
        if (AnimCtrl == null) Debug.LogError("RuntimeAnimatorController AnimCtrl NULL! | ");

        foreach (AnimationClip clip in AnimCtrl.animationClips)
        {
            if (clip.name == clipName) return clip.length;
        }
        return 0f;
    }
}
