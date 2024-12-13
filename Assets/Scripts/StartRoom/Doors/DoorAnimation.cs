using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    private Animator _animator;
    private void Awake() => _animator = GetComponent<Animator>();
    public void AnimationOpen() => _animator.SetTrigger("Open");
    public void AnimationClose() => _animator.SetTrigger("Close");
}
