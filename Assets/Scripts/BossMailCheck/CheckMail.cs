using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CheckMail : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private Camera _cameraStatic;
    [SerializeField] private GameObject _cameraMoving;

    [Space(10)]
    [SerializeField] private Button _firstMail;
    [SerializeField] private Button _secondMail;

    [Space(10)]
    [SerializeField] private Image _timerBar;

    [Space(10)]
    [SerializeField] private DialogueCheckMail _dialogueCheckMail;

    [Space(10)]
    [SerializeField] private Animator _mail0Anim;
    [SerializeField] private Animator _mail1Anim;
    [SerializeField] private Animator _bossImageAnim;
    [SerializeField] private Animator _cameraAnim;

    private float currentTime;
    private bool isEnebaledMails;

    private void OnValidate()
    {
        _firstMail.onClick.AddListener(FirstMailCheck);
        _secondMail.onClick.AddListener(SecondMailCheck);
    }
    private void Start() 
    { 
        isEnebaledMails = false;
        _cameraMoving.transform.position = _cameraStatic.transform.position;
        _cameraMoving.SetActive(false);
    }
    private void Update()
    {
        if(DialogueCheckMail._isDialogueFinised && !isEnebaledMails)
        {
            StartCoroutine(WaitForEnableMails(0.5f));
            isEnebaledMails = true;
        }
    }
    private void FirstMailCheck()
    {
        if (GM.IsPlayingRoomBossCheckMail)
            StartCoroutine(WaitForDisableMails(seconds: 2, indexCameraAnim: 2, indexScene: 5)); 
    }
    private void SecondMailCheck()
    {
        if (GM.IsPlayingRoomBossCheckMail)
            StartCoroutine(WaitForDisableMails(seconds: 2, indexCameraAnim: -2, indexScene: 4));
    }
    public IEnumerator WaitForEnableMails(float seconds)
    {
         yield return new WaitForSeconds(seconds);

        _mail0Anim.SetBool("Up", true);
        _mail1Anim.SetBool("Up", true);
    }
    private IEnumerator WaitForDisableMails(float seconds, int indexCameraAnim, int indexScene)
    {
        _cameraStatic.gameObject.SetActive(false);
        _cameraMoving.SetActive(true);

        _timerBar.gameObject.SetActive(false);

        _mail0Anim.SetBool("Fall", true);
        _mail1Anim.SetBool("Fall", true);
        //_bossImageAnim.SetTrigger("Fall");

        yield return new WaitForSeconds(seconds);

        _cameraAnim.SetFloat("Choose", indexCameraAnim);
        _dialogueCheckMail.ShowLineExtra(indexScene);
   

    }
}
