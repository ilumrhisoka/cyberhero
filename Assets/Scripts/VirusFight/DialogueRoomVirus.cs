
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class DialogueRoomVirus : MonoBehaviour
{
    [SerializeField] private string[] _linesText;
    [SerializeField] private Image[] _images;
    [SerializeField] private Image[] _imagesExtra;
    [SerializeField] private float _speedText;
    [SerializeField] private Button _skipButton;
    [SerializeField] private Toggle _activateVirus;

    [SerializeField] private AudioClip[] _voiceLines;
    [SerializeField] private AudioClip[] _voiceLinesExtra;

    private AudioSource _audioSource;
    private Text _dialogText;
    private GameObject _dialogObject;
    private Fade _fade;

    private int _index;
    private bool _isSkipTextButton;
    private bool _isActivVirusSpawner;
    public string[] _linesExtraText;

    private void Awake()
    {
        _fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade>();
        _dialogText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<Text>();
        _dialogObject = GameObject.FindGameObjectWithTag("DialogEvent");
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        _isActivVirusSpawner = true;
        _isSkipTextButton = true;
        _skipButton.gameObject.SetActive(true);
        _dialogText.text = string.Empty;
        StartDialog();
    }
    private void Update()
    {
        if (_activateVirus.isOn && _isActivVirusSpawner)
        {
            _skipButton.gameObject.SetActive(true);
        }
    }
    private void StartDialog()
    {
        _index = 0;
        StartCoroutine(TypeLine());
    }
    private void NextLine()
    {
        if (_index < _linesText.Length - 1)
        {
            if (_index == 2)
                _skipButton.gameObject.SetActive(false);

            _index++;
            _dialogText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
            HideDialogue();
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
    public void ShowLineExtra(int indexExtrsLine, int scene, int indexImage, int indexVoice)
    {
        _isSkipTextButton = false;
        _skipButton.gameObject.SetActive(false);
        GM.IsPlayingRoomVirus = false;
        _dialogText.text = string.Empty;
        _imagesExtra[indexImage].gameObject.SetActive(true);
        StartCoroutine(TypeLineExtra(indexExtrsLine, indexVoice));
        StartCoroutine(WaitAndFadeAndLoadScene(3f, scene));
    }
    private IEnumerator WaitAndFadeAndLoadScene(float waitTime, int scene)
    {
        yield return new WaitForSeconds(waitTime);
        _fade.FadeIn();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene+1);
    }
    public void HideDialogue()
    {
        foreach (var image in _images)
            image.gameObject.SetActive(false);

        _dialogObject.SetActive(false);
    }
    public void OnDialogue() => _dialogObject.SetActive(true);
}