using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : PlayerMain2
{
    [SerializeField] private float _speed;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip moveSound;

    private Rigidbody2D _rb;
    public Transform weapon;
    private SpriteRenderer _spriteRenderer;
    protected PlayerAnimRoomVirus _playerAnim;
    private AudioSource audioSource;

    private Vector2 swordPosition;
    private Vector2 lastDirection;
    public LayerMask enemyLayer;
    private Vector3 _moveVector;

    private float nextAttackTime = 0f;
    public float attackRange = 5f; 
    public float attackRate = 1f; 
    private float dirX, dirY;

    private void Awake()
    {
        _attackButton.onClick.AddListener(AttackButton);
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerAnim = GetComponent<PlayerAnimRoomVirus>();
        audioSource = GetComponent<AudioSource>(); 
    }
    private void Update()
    {
        if (GM.IsPlayingRoomVirus)
        {
            dirX = _joystick.Horizontal * _speed;
            dirY = _joystick.Vertical * _speed;
        }
    }
    private void FixedUpdate()
    {
        if (GM.IsPlayingRoomVirus)
        {
            Vector2 movement = new Vector2(dirX, dirY);

            if (dirX != 0 || dirY != 0)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = moveSound;
                    audioSource.loop = true;
                    audioSource.Play();
                }
            }
            else
                audioSource.Stop();

            if (movement.magnitude > 0)
            {
                lastDirection = movement;
                _spriteRenderer.flipX = movement.x < 0;
                _playerAnim.RunAnim(1);
            }
            else
            {
                _spriteRenderer.flipX = lastDirection.x < 0;
                _playerAnim.RunAnim(0);
            }
            _rb.velocity = movement;
        }
    }
    public void AttackButton()
    {
        if (Time.time >= nextAttackTime && GM.IsPlayingRoomVirus)
        {
            Transform target = FindClosestEnemy();

            if (target != null)
            {
                Attack(target);
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    private void Attack(Transform targetPosition)
    {
        Vector2 direction = (targetPosition.position - weapon.position).normalized;
        weapon.up = direction;

        _playerAnim.AtackAnim();
        PlayAttackSound();
        StartCoroutine(PerformAttack(targetPosition.position));
    }

    private void PlayAttackSound()
    {
        audioSource.clip = attackSound;
        audioSource.Play();
    }
    private Transform FindClosestEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }
        return closestEnemy;
    }
    private IEnumerator PerformAttack(Vector2 targetPosition)
    {
        yield return new WaitForSeconds(0.5f);
        SpawnBullet(targetPosition);
    }
    private void SpawnBullet(Vector2 targetPosition)
    {
        if (bulletPrefab != null && weapon != null)
        {
            Vector2 swordPosition = weapon.position;
            Vector2 _direction = (targetPosition - swordPosition).normalized;
            GameObject bullet = Instantiate(bulletPrefab, swordPosition, Quaternion.identity);
            bullet.GetComponent<Bullet>().SetDirection(_direction);
        }
    }
}