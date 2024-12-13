using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BossGrenade : MonoBehaviour
{
    [Space(10)]
    [SerializeField] public GameObject _prefabGrenade;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private AudioClip _sound;

    [Space(10)]
    [SerializeField] private float _forceToThrow = 10;

    private Rigidbody2D _rb;
    private AudioSource _audioSource;

    private void OnValidate() => _rb ??= GetComponent<Rigidbody2D>();
    public IEnumerator ThrowGrenade(GameObject prefabGrenade, Vector2 throwDirection)
    {
        if (!prefabGrenade)
        {
            _forceToThrow = 10;

            switch (Random.RandomRange(0, 3))
            {
                case 0:
                    _forceToThrow += 5;
                    break;
                case 1:
                    _forceToThrow -= 5;
                    break;
                case 2:
                    _forceToThrow += 2.5f;
                    break;
            }
            Rigidbody2D rb = prefabGrenade.GetComponent<Rigidbody2D>();
            rb.AddForce(throwDirection * _forceToThrow, ForceMode2D.Impulse);
            yield return new WaitForSeconds(3f); 
        }
        Explode(prefabGrenade);
    }
    private void Explode(GameObject grenade)
    {
        GameObject explosion = Instantiate(explosionPrefab, grenade.transform.position, Quaternion.identity);
        Invoke(nameof(WaitDestroy), 0.5f);
        Destroy(grenade);
        //Destroy(explosion);
    }
    private void WaitDestroy() => Destroy(this.gameObject);
    public void PlaySound()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _sound;
        _audioSource.Play();
    }
}