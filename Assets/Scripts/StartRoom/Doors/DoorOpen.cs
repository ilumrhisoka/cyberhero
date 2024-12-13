using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private DoorAnimation _doorAnimation;
    [SerializeField] private GameObject _buttonOpen;
    [SerializeField] private int _indexScene;
    [SerializeField] private int _roomNumber;
    [SerializeField] private AudioClip openDoorSound;
    [SerializeField] private float doorVolume = 0.5f; 

    private Fade _fade;
    private AudioSource _audioSource;

    private void OnValidate()
    {
        _doorAnimation = GetComponent<DoorAnimation>();
        _fade = FindAnyObjectByType<Fade>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = openDoorSound;
        _audioSource.volume = doorVolume;
    }
    private void Start()
    {
        if (IsRoomWon())
        {
            _buttonOpen.SetActive(false);
            _doorAnimation.AnimationClose();
        }
    }
    public void ButtonDoorOpen()
    {
        if (CanOpenDoor())
        {
            _doorAnimation.AnimationOpen();
            _audioSource.Play(); 
            StartCoroutine(_LoadScene(_indexScene));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !IsRoomWon())
            _buttonOpen.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _buttonOpen.SetActive(false);
    }
    IEnumerator _LoadScene(int index)
    {
        _fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(index);
    }
    private bool CanOpenDoor()
    {
        return _roomNumber switch
        {
            1 => !GM.Room1Win,
            2 => !GM.Room2Win,
            3 => !GM.Room3Win,
            4 => !GM.Room4Win,
            _ => false,
        };
    }
    private bool IsRoomWon()
    {
        return _roomNumber switch
        {
            1 => GM.Room1Win,
            2 => GM.Room2Win,
            3 => GM.Room3Win,
            4 => GM.Room4Win,
            _ => false,
        };
    }
}