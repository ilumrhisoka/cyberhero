using UnityEngine;
using UnityEngine.UI;

public class ButtonApplayCard : MonoBehaviour
{
    [SerializeField] private Button _applayCard;
    [SerializeField] private CheckCard_ _checkCard;
    [SerializeField] private DialogueCheckCard _dialogue;
    [SerializeField] private AudioClip clickSound; 

    private AudioSource audioSource; 

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); 
        audioSource.clip = clickSound; 
    }
    private void OnValidate() => _applayCard.onClick.AddListener(CheckResult);
    private void CheckResult()
    {
        audioSource.Play(); 

        if (_checkCard != null && GM.IsPlayingRoomCheckCard)
        {
            if (_checkCard._numberCard == string.IsNullOrEmpty(_checkCard._numberCard.text) ||
                _checkCard._numberCard.text.Length < 19 ||
                _checkCard._userNameOfCard == string.IsNullOrEmpty(_checkCard._userNameOfCard.text) ||
                _checkCard._userNameOfCard.text.Length < 4 ||
                _checkCard._dateOfCard == string.IsNullOrEmpty(_checkCard._dateOfCard.text) ||
                _checkCard._userNameOfCard.text.Length < 5)
            {
                _dialogue.ShowLineExtra(indexExtrsLine: 2, indexImage: 2, indexVoice: 2);
            }
            else
            {
                if (string.IsNullOrEmpty(_checkCard._cvv.text))
                {
                    _dialogue.ShowLineExtra(indexExtrsLine: 0, scene: 0, indexImage: 0, indexVoice: 0);
                    GM.Room4Win = true;
                    PlayerMain.CountKeys++;
                }
                else
                    _dialogue.ShowLineExtra(indexExtrsLine: 1, scene: 7, indexImage: 1, indexVoice: 1);
            }
        }
    }
}