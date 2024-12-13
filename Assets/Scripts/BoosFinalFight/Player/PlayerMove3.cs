using UnityEngine;
using UnityEngine.UI;

public class PlayerMove3 : PlayerMain3
{
    [Space(10)]
    [SerializeField] private Button _jumpButton;

    [Space(10)]
    [SerializeField, Range(0, 10)] private float _speed;
    [SerializeField, Range(0, 20)] private float _forceJump;

    private Vector2 _lastDirection;
    public Vector3 _moveVector { get; private set; }

    private bool isGrounded = false;
    private float groundCheckDistance = 0.1f;
    private float dirX;

    private void OnValidate()
    {
        _jumpButton.onClick.AddListener(Jump);
    }
    private void Update() => SetDerectionMove();

    private void FixedUpdate()
    {
        Move();
        CheckGround();
    }
    private void SetDerectionMove()
    {
        if (GM.IsPlayingRoomBossFinalFight)
            dirX = _joystick.Horizontal * _speed;
    }
    private void Move()
    {
        if (GM.IsPlayingRoomBossFinalFight)
        {
            _moveVector = new Vector2(dirX, 0);

            if (_moveVector.magnitude > 0)
            {
                _lastDirection = _moveVector;
                _spriteRenderer.flipX = _moveVector.x < 0;
            }
            else
            {
                _spriteRenderer.flipX = _lastDirection.x < 0;
            }

            transform.position += _moveVector * Time.deltaTime;   
        }
    }
    private void Jump()
    {
        if (isGrounded && GM.IsPlayingRoomBossFinalFight)
        {
            _rigidbody.AddForce(Vector2.up * _forceJump, ForceMode2D.Impulse);
        }
    }
    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(_lowerLegsPoint.transform.position, Vector2.down, groundCheckDistance, LayerMask.GetMask("Ground"));
        isGrounded = hit.collider != null;
    }
}
