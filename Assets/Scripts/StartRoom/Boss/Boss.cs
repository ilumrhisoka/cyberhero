using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject _dialogue;
    [SerializeField] private SpriteRenderer _sp;
    [SerializeField] private float _speed;


    [SerializeField] private PlayerAnimation _playerAnimation;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private BossAnim _bossAnim;

    private float timer = 0f;
    private const float timeToDestroy = 6f;

    public static bool isDialogueEnd = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _dialogue.SetActive(true);
            _playerMove._audioSource.Stop();
            GM.IsPlayingRoomStart = false;
            _playerMove._rb.velocity = new Vector2(0, 0);
            _playerAnimation.AnimationRun(0);
        }
    }
    private void Update()
    {
        if (isDialogueEnd)
        {
            GM.IsPlayingRoomStart = true;

            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - _speed * Time.deltaTime, transform.position.y, transform.position.z), Time.deltaTime);

            _sp.flipX = true;
            _bossAnim.BossRunAnim();
            
            if (timer >= timeToDestroy)
                Destroy(gameObject);
        }
    }
}
