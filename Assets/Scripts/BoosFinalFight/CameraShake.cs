using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Animator _animCamera;
    private Animator _animFade;
    private CanvasGroup _canvasGroup;
    [SerializeField] GameObject _fade;

    private void OnValidate() 
    {
        _animCamera = GetComponent<Animator>();
        _fade = GameObject.FindGameObjectWithTag("Fade");
        _animFade = _fade.GetComponent<Animator>();
        _canvasGroup = _fade.GetComponent<CanvasGroup>();
    }
    public void Shake(bool isShaking)
    {
        _canvasGroup.alpha = 1;
        _animFade.SetBool("IsShaking", isShaking);
        _animCamera.SetBool("IsShaking", isShaking);
    }
}