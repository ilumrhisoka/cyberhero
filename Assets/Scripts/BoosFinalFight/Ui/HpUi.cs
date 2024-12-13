using UnityEngine;
using UnityEngine.UI;

public class HpUi : MonoBehaviour
{
    [Space(5)]
    [SerializeField] private PlayerMain3 _player;

    private Text _countHp;

    private void Awake()
    {
        _countHp = GetComponent<Text>();
        UpdateHpText();
    }
    private void OnEnable() => _player.OnTakeDamaged += UpdateHpText;
    private void OnDisable() => _player.OnTakeDamaged -= UpdateHpText;
    private void UpdateHpText() => _countHp.text = PlayerMain3.Hp.ToString(); 
}
