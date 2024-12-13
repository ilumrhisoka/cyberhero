using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMain2 : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform bulletSpawnPoint;
    [SerializeField] protected Button _attackButton;
    [SerializeField] protected Joystick _joystick;
    [SerializeField] protected float fireRate = 0.5f;

    private DialogueRoomVirus _dialogueRoomVirus;
    protected float nextFireTime = 0f;

    private void Awake()
    {
        Application.targetFrameRate = 120;

        GM.IsPlayingRoomVirus = false;
        GM.CountOfKilledViruses = 0;
    }
    private void Start()
    {
        _attackButton = GameObject.FindGameObjectWithTag("AttackButton").GetComponent<Button>();
        _joystick = GameObject.FindAnyObjectByType<Joystick>().GetComponent<Joystick>();
        _dialogueRoomVirus = GameObject.FindGameObjectWithTag("DialogEvent").GetComponent<DialogueRoomVirus>();
    }
    public void IsPlaying() => GM.IsPlayingRoomVirus = true;
}
