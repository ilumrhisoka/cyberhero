using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    [SerializeField] protected List<Image> _hp;
    private DialogueRoomVirus _dialogueRoomVirus;

    private void Awake()
    {
        _dialogueRoomVirus = GameObject.FindGameObjectWithTag("DialogEvent").GetComponent<DialogueRoomVirus>();
    }
    public void TakeDamage()
    {
        Debug.Log(_hp.Count);

        if (_hp.Count > 0)
        {
            Destroy(_hp.Last());
            _hp.RemoveAt(_hp.Count - 1);
        }
        else
        {
            Destroy(gameObject);

            _dialogueRoomVirus.OnDialogue();
            _dialogueRoomVirus.ShowLineExtra(indexExtrsLine: 0, scene: 2, indexImage: 1, indexVoice: 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GM.IsPlayingRoomVirus)
        {
            if (collision.gameObject.tag == "Virus" || collision.gameObject.tag == "VirusBoom")
                TakeDamage();
        }
    }
}
