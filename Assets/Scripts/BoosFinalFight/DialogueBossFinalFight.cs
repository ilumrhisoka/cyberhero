using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueBossFinalFight : MonoBehaviour
{
    public static bool IsDialogueEnd;

    [Space(10)]
    [SerializeField] private string[] _linesText;
    [SerializeField] private string[] _linesExtraText;

    [Space(10)]
    [SerializeField] private Image[] _images;
    [SerializeField] private Image[] _imagesExtra;

    [Space(10)]
    [SerializeField] private float _speedText;

    [Space(10)]
    [SerializeField] private AudioClip[] _voiceLines;
    [SerializeField] private AudioClip[] _voiceLinesExtra;

    private AudioSource _audioSource;

    private Text _dialogText;
    private Fade _fade;
    private Button _skipButton;
    private GameObject _dialogObject;

    private int _index;
    private bool _isSkipTextButton;
    
    private void OnValidate()
    {
        _dialogObject = GameObject.FindGameObjectWithTag("DialogEvent");
        _fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade>();
        _dialogText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<Text>();
        _skipButton = GameObject.FindGameObjectWithTag("SkipText").GetComponent<Button>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        IsDialogueEnd = false;
        _isSkipTextButton = true;
        _skipButton.gameObject.SetActive(true);
        _dialogText.text = string.Empty;
        StartDialog();
    }
    private void StartDialog()
    {
        _index = 0;
        StartCoroutine(TypeLine());
    }
    public void ShowLineExtra(int indexExtrsLine, int scene, int indexImage, int indexVoice)
    {
        _dialogText.text = string.Empty;
        _isSkipTextButton = false;
        GM.IsPlayingRoomBossFinalFight = false;

        _skipButton.gameObject.SetActive(false);
        _dialogObject.SetActive(true);
        _imagesExtra[indexImage].gameObject.SetActive(true);

        StartCoroutine(TypeLineExtra(indexExtrsLine, indexVoice));
        StartCoroutine(WaitAndFadeAndLoadScene(5f, scene));
    }
    private IEnumerator WaitAndFadeAndLoadScene(float waitTime, int scene)
    {
        yield return new WaitForSeconds(waitTime);
        _fade.FadeIn();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(scene);
    }
    
    #region NextAndSkipLine
    private void NextLine()
    {
        if (_index < _linesText.Length - 1)
        {
            _index++;
            _dialogText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            IsDialogueEnd = true;
            HideDialogue();
        }
    }
    public void SkipText()
    {
        if (_isSkipTextButton)
        {
            if (_dialogText.text == _linesText[_index])
                NextLine();
            else
            {
                StopAllCoroutines();
                _dialogText.text = _linesText[_index];
                PlayVoiceLine();
            }
        }
    }
    #endregion
    
    #region TypingLine
    IEnumerator TypeLine()
    {
        if (_index < _images.Length)
            _images[_index].gameObject.SetActive(true);

        PlayVoiceLine();

        foreach (var c in _linesText[_index].ToCharArray())
        {
            _dialogText.text += c;
            yield return new WaitForSeconds(_speedText);
        }
    }
    IEnumerator TypeLineExtra(int index, int indexVoice)
    {
        PlayVoiceLineExtra(indexVoice);

        foreach (var c in _linesExtraText[index].ToCharArray())
        {
            _dialogText.text += c;
            yield return new WaitForSeconds(_speedText);
        }
    }
    #endregion
    
    #region VoiceLine
    private void PlayVoiceLine()
    {
        if (_index < _voiceLines.Length && _voiceLines[_index] != null)
        {
            _audioSource.clip = _voiceLines[_index];
            _audioSource.Play();
        }
    }
    private void PlayVoiceLineExtra(int indexVoice)
    {
        if (indexVoice < _voiceLinesExtra.Length)
        {
            _audioSource.clip = _voiceLinesExtra[indexVoice];
            _audioSource.Play();
        }
    }
    #endregion

    #region OnAndOffDialogue
    public void HideDialogue()
    {
        foreach (var image in _images)
            image.gameObject.SetActive(false);
        foreach (var imageEx in _imagesExtra)
            imageEx.gameObject.SetActive(false);

        _dialogObject.SetActive(false);
    }
    public void OnDialogue() => _dialogObject.SetActive(true);
    #endregion
}