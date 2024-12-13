using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeysText : MonoBehaviour
{
    private Text _keysText;

    private void Awake()
    {
        _keysText = GetComponent<Text>();
    }
    private void Update()
    {
        _keysText.text = PlayerMain.CountKeys.ToString();
    }
}
