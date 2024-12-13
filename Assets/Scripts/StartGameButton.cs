using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;   

public class StartGameButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Fade _fade;
    
    private void Awake() => _button.onClick.AddListener(StartGame);
    private void Start() => _fade.FadeOut();
    public void StartGame()
    {
        if (_fade.canvasGroup.alpha == 0)
            StartCoroutine(WaitForFade());
    }
    private IEnumerator WaitForFade()
    {
        _fade.FadeIn();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }
}
