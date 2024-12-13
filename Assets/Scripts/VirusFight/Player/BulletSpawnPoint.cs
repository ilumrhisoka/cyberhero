using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnPoint : MonoBehaviour
{
    private DialogueRoomVirus _dialogueRoomVirus;
    private bool isWining;

    private void Awake()
    {
        isWining = true;
        _dialogueRoomVirus = GameObject.FindGameObjectWithTag("DialogEvent").GetComponent<DialogueRoomVirus>();
    }
    private void Update()
    {
        if (GM.CountOfKilledViruses >= 3 && isWining)
        {
            isWining = false;
            GM.IsPlayingRoomVirus = false;
            GM.Room2Win = true;
            PlayerMain.CountKeys++;

            _dialogueRoomVirus.OnDialogue();
            _dialogueRoomVirus.ShowLineExtra(indexExtrsLine: 1, scene: 0, indexImage: 0, indexVoice: 0);
        }
    }
}
