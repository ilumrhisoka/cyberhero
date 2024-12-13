using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class CheckCard_ : MonoBehaviour
{
    [SerializeField] public InputField _numberCard;
    [SerializeField] public InputField _dateOfCard;
    [SerializeField] public InputField _cvv;
    [SerializeField] public InputField _userNameOfCard;
    [SerializeField] private AudioClip clickSound; 

    private AudioSource audioSource; 

    private void Awake()
    {
        GM.IsPlayingRoomCheckCard = false;
        audioSource = gameObject.GetComponent<AudioSource>(); 
        audioSource.clip = clickSound; 
    }
    private void OnValidate()
    {
        _numberCard.onValueChanged.AddListener(ValidateNumberCard);
        _dateOfCard.onValueChanged.AddListener(ValidateDateOfCard);
        _cvv.onValueChanged.AddListener(ValidateCVV);
        _userNameOfCard.onValueChanged.AddListener(ValidateUserName);
    }
    private void ValidateNumberCard(string value)
    {
        value = Regex.Replace(value, @"[^0-9]", "");

        if (value.Length > 16)
            _numberCard.text = value.Substring(0, 16);
        else
        {
            string formattedValue = "";

            for (int i = 0; i < value.Length; i++)
            {
                if (i > 0 && i % 4 == 0)
                    formattedValue += " ";
                formattedValue += value[i];
            }
            _numberCard.text = formattedValue;
        }
    }
    private void ValidateDateOfCard(string value)
    {
        if (value.Length > 5)
            _dateOfCard.text = value.Substring(0, 5);
        else if (value.Length == 2 && !value.Contains("/"))
            _dateOfCard.text = value + "/";
    }
    public void ValidateCVV(string value)
    {
        if (!Regex.IsMatch(value, @"^\d+$") || value.Length > 3)
        {
            _cvv.text = Regex.Replace(value, @"[^0-9]", "");

            if (_cvv.text.Length > 3)
                _cvv.text = _cvv.text.Substring(0, 3);
        }
    }
    private void ValidateUserName(string value)
    {
        if (!Regex.IsMatch(value, @"^[a-zA-Z\s]+$"))
            _userNameOfCard.text = Regex.Replace(value, @"[^a-zA-Z\s]", "");
    }
}