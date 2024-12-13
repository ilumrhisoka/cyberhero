using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public bool fadeIn = false;
    public bool fadeOut = false;

    public float TimeToFade;

    private void Update()
    {
        if(fadeIn == true)
        {
            if(canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += TimeToFade * Time.deltaTime;

                if(canvasGroup.alpha >= 1)
                {
                    fadeIn = false;
                } 
            }
        }
        if(fadeOut == true)
        {
            if(canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= TimeToFade * Time.deltaTime;

                if(canvasGroup.alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }
    }
    public void FadeIn() => fadeIn = true;
    public void FadeOut() => fadeOut = true;
}