using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    [SerializeField] private GameObject _boss;
    [SerializeField] private GameObject wall;

    protected SpriteRenderer _spriteRenderer;
    protected PlayerAnimation _playerAnim;
    protected Vector3 _moveVector;

    private bool isTrue = true;

    public static int CountKeys = 0;

    private void Awake()
    {
        Application.targetFrameRate = 120;
        GM.IsPlayingRoomStart = true;
    }
    private void OnValidate()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>(); 
        _playerAnim = GetComponentInChildren<PlayerAnimation>();   
    }
    private void Update()
    {
        if (CountKeys == 4 && isTrue)
        {        
            _boss.SetActive(true);
            wall.SetActive(false);
            isTrue = false;
        }
    }
}
