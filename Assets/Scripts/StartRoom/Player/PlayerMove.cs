using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerMain
{
    [SerializeField] private float _speed;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private AudioClip stepSound; 
    [SerializeField] private float stepVolume = 0.5f; 

    public Rigidbody2D _rb;
    public AudioSource _audioSource; 
    private Vector2 _lastDirection;
    public Vector2 movement;
    private float dirX;
    private bool isMoving = false; 

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _joystick = GameObject.FindAnyObjectByType<Joystick>();
        _audioSource = gameObject.AddComponent<AudioSource>(); 
        _audioSource.clip = stepSound;
        _audioSource.volume = stepVolume; 
    }

    private void Update()
    {
        if (GM.IsPlayingRoomStart)
            dirX = _joystick.Horizontal * _speed;
    }

    private void FixedUpdate()
    {
        if (GM.IsPlayingRoomStart)
        {
            movement = new Vector2(dirX, 0);

            if (movement.magnitude > 0)
            {
                _lastDirection = movement;
                _spriteRenderer.flipX = movement.x < 0;
                _playerAnim.AnimationRun(1);

                if (!isMoving)
                {
                    _audioSource.Play();
                    isMoving = true; 
                }
            }
            else
            {
                _spriteRenderer.flipX = _lastDirection.x < 0;
                _playerAnim.AnimationRun(0);

                if (isMoving)
                {
                    _audioSource.Stop();
                    isMoving = false; 
                }
            }
            _rb.velocity = movement;
        }
    }
}