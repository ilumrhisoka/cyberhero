using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimRoomVirus : MonoBehaviour 
{
    private Animator _animator;
    void Start() => _animator = GetComponent<Animator>();

    public void RunAnim(float speed)
    {
        _animator.SetFloat("Speed", speed);
    }
    public void AtackAnim()
    {
        _animator.SetBool("Atack", true);
        StartCoroutine(StopAttackAnimAfterDelay(1f));
    }
    private IEnumerator StopAttackAnimAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _animator.SetBool("Atack", false);
        Debug.Log("Attack animation stopped");
    }
}
