using UnityEngine;
using UnityEngine.UI;

public class PasswordSelection : MonoBehaviour
{
    public Button[] passwordButtons;
    public RoomPasswordDialogue dialogueScript;
    public AudioClip clickSound;
    private AudioSource audioSource; 

    private int correctButtonIndex;
    private int errorIndex;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = clickSound; 

        for (int i = 0; i < passwordButtons.Length; i++)
        {
            passwordButtons[i].gameObject.SetActive(false);
            int buttonIndex = i;
            passwordButtons[i].onClick.AddListener(() => PlayClickSoundAndCheckPasswordSelection(buttonIndex));
        }
    }
    public void CheckPasswordSelection(int buttonIndex)
    {
        if (GM.IsPlayingRoomPassword)
        {
            bool isCorrect = buttonIndex == correctButtonIndex;

            if (isCorrect)
            {
                errorIndex = 6;
                ShowError(sceneIndex: 1, imageIndex: 0, indexVoice: 6);

                GM.Room1Win = true;
                GM.IsPlayingRoomPassword = false;
                PlayerMain.CountKeys++;
            }
            else
            {
                errorIndex = 7;
                ShowError(sceneIndex: 2, imageIndex: 1, indexVoice: 7);
                GM.IsPlayingRoomPassword = false;
            }
        }
    }
    void ShowError(int sceneIndex, int imageIndex, int indexVoice) => dialogueScript.ShowErrorEnd(errorIndex, sceneIndex, imageIndex, indexVoice);
    private void PlayClickSoundAndCheckPasswordSelection(int buttonIndex)
    {
        audioSource.Play(); 
        CheckPasswordSelection(buttonIndex); 
    }
}