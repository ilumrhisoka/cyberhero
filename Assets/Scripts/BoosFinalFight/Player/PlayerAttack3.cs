using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack3 : PlayerMain3
{
    [Space(10)]
    [SerializeField] private float _speedBullet;
    [SerializeField] private float _attackCooldown;

    [Space(10)]
    [SerializeField] private Transform _sword;
    [SerializeField] private Transform _bossTransform;
    [SerializeField] private GameObject _prefabBullet;
    [SerializeField] private BossMain _bossMain;
    [SerializeField] private PlayerAnim _playerAnim;

    [Space(10)]
    [SerializeField] private Button _fireButton;
    private bool _canAttack = true;

    private void OnDisable() => _bossMain.OnTakeDamaged -= ApplayDamage;
    private void OnEnable() => _bossMain.OnTakeDamaged += ApplayDamage;
    private void Awake() => _fireButton.onClick.AddListener(Fire);
    private void ApplayDamage() => BossMain.Hp--;
    private void Fire()
    {
        if (_canAttack && GM.IsPlayingRoomBossFinalFight)
        {
            _canAttack = false;
            _playerAnim.AttackAnim();
            StartCoroutine(WaitCoolAnimAttack());
            StartCoroutine(AttackCooldown());
        }
    }
    private IEnumerator MoveBullet(GameObject bullet)
    {
        while (bullet != null && bullet.transform.position != _bossTransform.position)
        {
            bullet.transform.position = Vector2.MoveTowards(bullet.transform.position, _bossTransform.position, _speedBullet * Time.deltaTime);
            yield return null;
        }
        Explode(bullet);
    }
    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(_attackCooldown);
        _playerAnim.AttackAnim(false);
        _canAttack = true;
    }
    private IEnumerator WaitCoolAnimAttack()
    {
        yield return new WaitForSeconds(1f);
        GameObject bullet = Instantiate(_prefabBullet, _sword.position, Quaternion.identity);
        StartCoroutine(MoveBullet(bullet));
    }
    private void Explode(GameObject spike) => Destroy(spike);

}