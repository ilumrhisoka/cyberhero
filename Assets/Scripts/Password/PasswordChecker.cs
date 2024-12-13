using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;

public class PasswordChecker : MonoBehaviour
{
    public Button[] passwordButtons;
    public InputField passwordInputField;
    public Button checkButton;
    public Button browserButton;
    public RoomPasswordDialogue dialogueScript;
    public Image browserBg;
    public AudioClip clickSound; 
    private AudioSource audioSource; 

    private int errorIndex = -1;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>(); 
        audioSource.clip = clickSound;

        browserButton.onClick.AddListener(() => PlayClickSoundAndEnter());
        checkButton.onClick.AddListener(() => PlayClickSoundAndCheckPassword());

        browserBg.gameObject.SetActive(false);
        checkButton.gameObject.SetActive(false);
        passwordInputField.gameObject.SetActive(false);
    }
   private void HideAll()
    {
        checkButton.gameObject.SetActive(false);
        passwordInputField.gameObject.SetActive(false);
        browserBg.gameObject.SetActive(false);
    }
    public void CheckPassword()
    {
        if (GM.IsPlayingRoomPassword)
        {
            string password = passwordInputField.text;
            bool isValid = true;

            if (password.Length < 10)
            {
                errorIndex = 0;
                ShowError(0);
                isValid = false;
            }
            // проверка на заглавные буквы
            if (!password.Any(char.IsUpper))
            {
                errorIndex = 1;
                ShowError(1);
                isValid = false;
            }
            // проверка на строчные буквы
            else if (!password.Any(char.IsLower))
            {
                errorIndex = 2;
                ShowError(2);
                isValid = false;
            }
            // проверка на цифры
            else if (!password.Any(char.IsDigit))
            {
                errorIndex = 3;
                ShowError(3);
                isValid = false;
            }
            // проверка на спец символы
            else if (!password.Any(c => !char.IsLetterOrDigit(c)))
            {
                errorIndex = 4;
                ShowError(4);
                isValid = false;
            }
            if (isValid)
            {
                errorIndex = 5;
                ShowError(5);
                StartCoroutine(DelayedHideAll(2f));
                GM.IsPlayingRoomPassword = false;
            }
        }
    }
    void Enter()
    {
        if (GM.IsPlayingRoomPassword)
        {
            browserBg.gameObject.SetActive(true);
            checkButton.gameObject.SetActive(true);
            passwordInputField.gameObject.SetActive(true);
            browserButton.gameObject.SetActive(false);
        }
    }
    void ShowError(int indexVoice) => dialogueScript.ShowError(errorIndex, 2, indexVoice);
    IEnumerator DelayedHideAll(float delay)
    {
        yield return new WaitForSeconds(2f);
        HideAll();

        for (int i = 0; i < passwordButtons.Length; i++)
            passwordButtons[i].gameObject.SetActive(true);

        GM.IsPlayingRoomPassword = true;
    }

    private void PlayClickSoundAndEnter()
    {
        audioSource.Play(); 
        Enter(); 
    }
    private void PlayClickSoundAndCheckPassword()
    {
        audioSource.Play(); 
        CheckPassword(); 
    }
}