using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private string[] _lines;
    [SerializeField] private float _speedText;
    [SerializeField] private Image[] _images;
    [SerializeField] private GameObject _dialogObject;
    [SerializeField] private Fade _fade;
    [SerializeField] private AudioClip[] _voiceLines;

    private AudioSource _audioSource; 
    private Text _dialogText;

    private int _index;
    private void Awake()
    {
        _dialogText = GetComponentInChildren<Text>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
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
            GameObject.FindGameObjectWithTag("DialogEvent").SetActive(false);
            Boss.isDialogueEnd = true;
            HideDialogue();
        }
    }
    public void SkipText()
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
    IEnumerator TypeLine()
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
    private void PlayVoiceLine()
    {
        if (_index < _voiceLines.Length && !_voiceLines[_index])
        {
            _audioSource.clip = _voiceLines[_index];
            _audioSource.Play();
        }
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
        foreach (var image in _images)
            image.gameObject.SetActive(false);
        
        _dialogObject.SetActive(false);
    }
    public void OnDialogue() => _dialogObject.SetActive(true);
}
