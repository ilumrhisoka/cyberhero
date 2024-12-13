using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private PlayerMain3 _playerMain;

    private Animator _anim;

    private void OnEnable() => _playerMain.OnTakeDamaged += HitAnim;
    private void OnDisable() => _playerMain.OnTakeDamaged -= HitAnim;
    private void OnValidate() => _anim = GetComponent<Animator>();
    public void HitAnim() => _anim.SetBool("IsTakeDamage", true);
    public void HitAnimFalse() => _anim.SetBool("IsTakeDamage", false);
    public void AttackAnim(bool isAttack = true) => _anim.SetBool("IsAttack", isAttack);
}
