using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneBoss : MonoBehaviour
{
    private Fade _fade;
    private void Awake()
    {
        _fade = GameObject.FindAnyObjectByType<Fade>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            StartCoroutine(_LoadScene(4));
    }
    IEnumerator _LoadScene(int index)
    {
        _fade.FadeIn(); 
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(index);
    }
}
