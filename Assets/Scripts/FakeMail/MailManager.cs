using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailManager : MonoBehaviour
{
    [SerializeField] private Button[] _questionButtons;
    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;
    [SerializeField] private Image[] _images;
    [SerializeField] private bool[] answers = { false, true, false, true };

    private List<bool> _playerAnswers = new List<bool>();
    private Fade _fade;
    private DialogueFakeMail _dialogueFakeMail;

    private void Awake()
    {
        _dialogueFakeMail = GameObject.FindGameObjectWithTag("DialogEvent").GetComponent<DialogueFakeMail>();
        _fade = FindAnyObjectByType<Fade>();
    }
    private void Start()
    {
        GM.IsPlayingRoomMail = false;
        Application.targetFrameRate = 120;
        
        _fade.FadeOut();

        foreach (Button button in _questionButtons)
            button.gameObject.SetActive(true);
        foreach (Image image in _images)
            image.gameObject.SetActive(false);
        foreach (Button button in _questionButtons)
            button.onClick.AddListener(() => ShowAnswerButtons());

        _yesButton.onClick.AddListener(() => Answer(true));
        _noButton.onClick.AddListener(() => Answer(false));
    }
    public void ShowImage(int index)
    {
        if (GM.IsPlayingRoomMail)
        {
            foreach (Image image in _images)
                image.gameObject.SetActive(false);

            _images[index].gameObject.SetActive(true);
            _questionButtons[index].gameObject.GetComponent<Image>().color = Color.gray;
        }
    }
    public void ShowAnswerButtons()
    {
        if (GM.IsPlayingRoomMail)
        {
            _yesButton.gameObject.SetActive(true);
            _noButton.gameObject.SetActive(true);
        }
    }
    public void Answer(bool userAnswer)
    {
        if (GM.IsPlayingRoomMail)
        {
            _playerAnswers.Add(userAnswer);

            _yesButton.gameObject.SetActive(false);
            _noButton.gameObject.SetActive(false);

            for (int i = 0; i < _images.Length; i++)
            {
                if (_images[i].gameObject.activeSelf)
                {
                    _questionButtons[i].gameObject.SetActive(false);
                    _images[i].gameObject.SetActive(false);
                }
            }

            if (_playerAnswers.Count == answers.Length)
                CheckAnswers();
        }
    }
    private void CheckAnswers()
    {
        bool allAnswersCorrect = true;

        for (int i = 0; i < answers.Length; i++)
        {
            if (_playerAnswers[i] != answers[i])
            {
                allAnswersCorrect = false;
                break;
            }
        }
        if (allAnswersCorrect)
        {
            GM.Room3Win = true;
            PlayerMain.CountKeys++;

            _dialogueFakeMail.OnDialogue();
            _dialogueFakeMail.ShowLineExtra(indexExtrsLine: 0, scene: 0, indexImage: 0, indexVoice: 0);
        }
        else
        {
            _dialogueFakeMail.OnDialogue();
            _dialogueFakeMail.ShowLineExtra(indexExtrsLine: 1, scene: 3, indexImage: 1,indexVoice: 1);
        }
    }
}