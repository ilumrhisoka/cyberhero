using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossAtack : BossMain
{
    [Space(5)]
    [SerializeField] private PlayerMain3 _player;
    
    [Space(10)]
    [SerializeField] private BossBullet _bossBullet;
    [SerializeField] private BossGrenade _bossGrenade;
    [SerializeField] private BossBoomerang _bossBoomerang;
    [SerializeField] private BossSpike _bossSpike;

    [Space(10)]
    [SerializeField] private Transform[] _pointsFire;
    [SerializeField] private Transform[] _pointsBezier;
    [SerializeField] private Transform[] _pointsSpike;
    [SerializeField] private Transform _thorwPoint;

    [Space(10)]
    [SerializeField] private float _fireAttackInterval;
    [SerializeField] private float _grenageAttckInterval;
    [SerializeField] private float _boomerangAttackInterval;
    [SerializeField] private float _spikeAttackInterval;

    private GameObject BulletPrefab => _bossBullet._prefabBullet;
    private GameObject GrenadePrefab => _bossGrenade._prefabGrenade;
    private GameObject BoomerangPrefab => _bossBoomerang._prefabBoomerang;
    private GameObject SpikePrefab => _bossSpike._prefabSpike;

    private readonly List<GameObject> _activeBullets = new List<GameObject>();

    private bool _isAttcak;

    private void OnDisable() => _player.OnTakeDamaged -= ApplayDamage;
    private void OnEnable() => _player.OnTakeDamaged += ApplayDamage;

    private void Awake()
    {
        _isAttcak = true;
    }
    private void Update()
    {
        if (GM.IsPlayingRoomBossFinalFight && _isAttcak)
        {
            StartCoroutine(FireRoutine());
            StartCoroutine(GrenadeRoutine());
            StartCoroutine(BoomerangRoutine());
            StartCoroutine(SpikeRoutine());

            _isAttcak = false;
        }
        else if (!GM.IsPlayingRoomBossFinalFight)
        {
            StopAllCoroutines();
            DestroyActiveBullets();
        }
    }
    private void ApplayDamage() => PlayerMain3.Hp--;
    private IEnumerator FireRoutine()
    {

        while (GM.IsPlayingRoomBossFinalFight)
        {
            Transform selectedFirePoint = _pointsFire[Random.RandomRange(0, _pointsFire.Length)];
            FireAttack(selectedFirePoint);
            yield return new WaitForSeconds(_fireAttackInterval);
        }
    }
    private void FireAttack(Transform firePoint)
    {
        if (BulletPrefab)
        {
            Vector3 firePointPosition = firePoint.position;
            Quaternion firePointRotation = firePoint.rotation;
            RaycastHit2D hit = Physics2D.Raycast(firePointPosition, -firePoint.right, 40);

            GameObject prefabBullet = Instantiate(BulletPrefab, firePointPosition, firePointRotation);
            _activeBullets.Add(prefabBullet);
            StartCoroutine(_bossBullet.MoveBullet(prefabBullet, _player.transform));
        }
    }
    private IEnumerator GrenadeRoutine()
    {
        while (GM.IsPlayingRoomBossFinalFight)
        {
            GrenadeAttack();
            yield return new WaitForSeconds(_grenageAttckInterval);
        }
    }
    private void GrenadeAttack()
    {
        if (GrenadePrefab)
        {
            Vector3 throwPointPosition = _thorwPoint.position;
            Quaternion throwPointRotation = _thorwPoint.rotation;

            GameObject prefabGrenade = Instantiate(GrenadePrefab, throwPointPosition, throwPointRotation);
            prefabGrenade.GetComponent<BossGrenade>().PlaySound();

            _activeBullets.Add(prefabGrenade);
            StartCoroutine(_bossGrenade.ThrowGrenade(prefabGrenade, throwPointRotation * Vector2.up));
        }
    }
    private IEnumerator BoomerangRoutine()
    {
        while (GM.IsPlayingRoomBossFinalFight)
        {
            BoomerangAttack();
            yield return new WaitForSeconds(_boomerangAttackInterval);
        }
    }
    private void BoomerangAttack()
    {
        if (BoomerangPrefab)
        {
            Vector3 StartThrowBoomerangPointPosition = _pointsBezier[0].position;
            Quaternion StartThrowPointRotation = _pointsBezier[0].rotation;

            GameObject prefabBoomerang = Instantiate(BoomerangPrefab, StartThrowBoomerangPointPosition, StartThrowPointRotation);
            prefabBoomerang.GetComponent<BossBoomerang>().PlaySound();
    
            _bossBoomerang.t = 0;
            _bossBoomerang.isMoving = true;
            _activeBullets.Add(prefabBoomerang);
            
            StartCoroutine(_bossBoomerang.MoveBoomerang(prefabBoomerang, _pointsBezier));
        }
    }
    private IEnumerator SpikeRoutine()
    {
        while (GM.IsPlayingRoomBossFinalFight)
        {
            for (int i = 0; i < _pointsSpike.Length; i++)
                SpikeAttack(i);
            yield return new WaitForSeconds(_spikeAttackInterval);
        }
    }
    private void SpikeAttack(int index)
    {
        GameObject prefabSpike = Instantiate(SpikePrefab, _pointsSpike[index]);
        prefabSpike.GetComponent<BossSpike>().PlaySound();

        _activeBullets.Add(prefabSpike);
        StartCoroutine(_bossSpike.SpikeUp(prefabSpike));
    }
    private void DestroyActiveBullets()
    {
        foreach (var bullet in _activeBullets)
        {
            if (!bullet)
                Destroy(bullet);
        }
        _activeBullets.Clear();
    }
    private void OnDrawGizmos()
    {
        int segmentsNumber = 20;
        Vector3 previousPoint = _pointsBezier[0].position;

        for (int i = 0; i <= segmentsNumber; i++)
        {
            float parameter = (float)i / segmentsNumber;

            Vector3 point = Bezier.GetPoint
            (_pointsBezier[0].position,
            _pointsBezier[1].position,
            _pointsBezier[2].position,
            _pointsBezier[3].position,
            parameter);

            Gizmos.DrawLine(previousPoint, point);
            previousPoint = point;
        }
    }
}