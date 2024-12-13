using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private DialogueCheckMail _dialogue;
    [Space(10)]
    [SerializeField] private Image timerBar;
    [Space(10)]
    [SerializeField] private float timerDuration = 15f;

    public static bool isStart;
    private bool isFinish;
    private float currentTime;

    private void Start()
    {
        isFinish = false;
        isStart = false;
        currentTime = timerDuration;
        timerBar.fillAmount = 1f;
    }
    private void Update()
    {
        if (isStart)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                timerBar.fillAmount = currentTime / timerDuration;
            }
            else if (!isFinish)
            {
                isFinish = true;
                GM.IsPlayingRoomBossCheckMail = false;
                _dialogue.ShowLineExtra(scene: 4);
            }
        }
    }
}
