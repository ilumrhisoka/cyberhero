using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : PlayerMain
{
    private Animator _animator;
    private void Awake() => _animator = GetComponent<Animator>();
    public void AnimationRun(float speed) => _animator.SetFloat("Speed", speed);
}
