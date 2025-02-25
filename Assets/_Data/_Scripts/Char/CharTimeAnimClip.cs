using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class CharTimeAnimClip : CoreMonoBehaviour
{
    [SerializeField] protected CharAnimatorCtrl charAnimatorCtrl;

    [SerializeField] protected float _attackAnimTime;
    [SerializeField] protected float _dodgeAnimTime;
    [SerializeField] protected float _dieAnimTime;

    public float AtackAnimTime
    {
        get => _attackAnimTime;
        private set => _attackAnimTime = value;
    }
    public float DodgeAnimTime
    {
        get => _dodgeAnimTime;
        private set => _dodgeAnimTime = value;
    }
    public float DieAnimTime
    {
        get => _dieAnimTime;
        private set => _dieAnimTime = value;
    }

    protected override void LoadComponents()
    {
        LoadCharAnimatorCtrl();
    }

    protected override void Reset()
    {
        base.Reset();
        LoadAnimClipTime();
    }

    protected virtual void LoadCharAnimatorCtrl()
    {
        if (this.charAnimatorCtrl != null) return;
        charAnimatorCtrl = GetComponent<CharAnimatorCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharAnimatorCtrl", gameObject);
    }

    protected virtual void LoadAnimClipTime()
    {
        Animator _charAnim = charAnimatorCtrl.CharCtrl.CharAnim;

        if (_charAnim != null)
        {
            AtackAnimTime = GetAnimationClipLength(_charAnim, "CharaAttack");
            Debug.Log("time animation Attack: " + AtackAnimTime);

            _dodgeAnimTime = GetAnimationClipLength(_charAnim, "CharaDodge");
            Debug.Log("time animation Dodge: " + _dodgeAnimTime);

            _dieAnimTime = GetAnimationClipLength(_charAnim, "CharaDie");
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
