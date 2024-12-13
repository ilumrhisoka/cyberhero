using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomPasswordDialogue : MonoBehaviour
{
    [SerializeField] private string[] _lines;
    [SerializeField] private string[] _errors;
    [SerializeField] private float _speedText;

    [SerializeField] private Button _skipButton;
    [SerializeField] private Image[] _images;
    [SerializeField] private Image[] _imagesExtra;

    [SerializeField] private Fade _fade;
    [SerializeField] private GameObject _dialogObject;
    [SerializeField] private AudioClip[] _voiceLines;
    [SerializeField] private AudioClip[] _voiceLinesExtra;

    private AudioSource _audioSource;
    private Text _dialogText;

    public static bool isDialogueFinished;
    private bool _isSkipTextButton;
    private int _index;

    private void Awake()
    {
        _dialogText = GetComponentInChildren<Text>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _isSkipTextButton = true;
        GM.IsPlayingRoomPassword = true;
        _skipButton.gameObject.SetActive(true);
        _dialogText.text = string.Empty;
        StartDialog();
    }
    private void StartDialog()
    {
        _index = 0;
        StartCoroutine(TypeLine());
    }
    private void NextLine()
    {
        if (_index < _lines.Length - 1)
        {
            _index++;
            _dialogText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            isDialogueFinished = true;
            HideDialogue();
        }
    }
    public void SkipText()
    {
        if (isDialogueFinished)
            HideDialogue();
        else
        {
            if (_isSkipTextButton)
            {
                if (_dialogText.text == _lines[_index])
                    NextLine();
                else
                {
                    StopAllCoroutines();
                    _dialogText.text = _lines[_index];
                    PlayVoiceLine();
                }
            }
        }
    }
    public IEnumerator TypeLine()
    {
        if (_index < _images.Length)
            _images[_index].gameObject.SetActive(true);

        PlayVoiceLine();

        foreach (var c in _lines[_index].ToCharArray())
        {
            _dialogText.text += c;
            yield return new WaitForSeconds(_speedText);
        }
    }
    public IEnumerator TypeLineError(int indexText, int indexVoice)
    {
        PlayVoiceLineExtra(indexVoice);

        foreach (var c in _errors[indexText].ToCharArray())
        {
            _dialogText.text += c;
            yield return new WaitForSeconds(_speedText);
        }
    }
    private void PlayVoiceLine()
    {
        if (_index < _voiceLines.Length)
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
    public void ShowErrorEnd(int errorIndex, int sceneIndex, int indexImage, int indexVoice)
    {
        _isSkipTextButton = false;
        _dialogObject.SetActive(true);
        _skipButton.gameObject.SetActive(false);

        _dialogText.text = string.Empty;
        StopAllCoroutines();

        _imagesExtra[indexImage].gameObject.SetActive(true);

        StartCoroutine(TypeLineError(errorIndex, indexVoice));
        StartCoroutine(WaitAndFadeAndLoadScene(3f, sceneIndex));
    }
    public void ShowError(int errorIndex, int indexImage, int indexVoice)
    {
        _isSkipTextButton = false;
        _dialogObject.SetActive(true);

        _dialogText.text = string.Empty;
        StopAllCoroutines();

        _imagesExtra[indexImage].gameObject.SetActive(true);

        StartCoroutine(TypeLineError(errorIndex, indexVoice));
    }
    private IEnumerator WaitAndFadeAndLoadScene(float waitTime, int scene)
    {
        yield return new WaitForSeconds(waitTime);
        _fade.FadeIn();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(scene);
    }
    public void HideDialogue()
    {
        GM.IsPlayingRoomPassword = true;

        foreach (var image in _images)
            image.gameObject.SetActive(false);
        foreach (var image in _imagesExtra)
            image.gameObject.SetActive(false); 

        _dialogObject.SetActive(false);
    }
}
