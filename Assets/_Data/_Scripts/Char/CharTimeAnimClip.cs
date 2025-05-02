using UnityEngine;

public class CharTimeAnimClip : CoreMonoBehaviour
{
    [SerializeField] protected CharAnimatorCtrl charAnimatorCtrl;

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
        RuntimeAnimatorController controllerCharAnim = _charAnim.runtimeAnimatorController;
        string controllerName = controllerCharAnim != null ? controllerCharAnim.name : "None";
        string resultControllerName = controllerName.Replace("Ctrl", "");  // => "Chara"

        if (_charAnim != null)
        {
            AtackAnimTime = GetAnimationClipLength(_charAnim, resultControllerName + "Attack");
            Debug.Log(resultControllerName +" time animation Attack: " + AtackAnimTime);

            _dodgeAnimTime = GetAnimationClipLength(_charAnim, resultControllerName+ "Dodge");
            Debug.Log(resultControllerName + " time animation Dodge: " + _dodgeAnimTime);

            _hurtAnimTime = GetAnimationClipLength(_charAnim, resultControllerName + "Hurt");
            Debug.Log(resultControllerName + " time animation Hurt: " + _hurtAnimTime);

            _dieAnimTime = GetAnimationClipLength(_charAnim, resultControllerName + "Die");
            Debug.Log(resultControllerName + " time animation Die: " + _dieAnimTime);
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
