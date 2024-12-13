using UnityEngine;

public class VirusBoom : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private AudioClip destroySound;

    private Vector2 targetPosition;
    private Animator _animator;
    private AudioSource audioSource;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (GM.IsPlayingRoomVirus)
        {
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            transform.Translate(direction * _speed * Time.deltaTime);
        }
    }
    public void DestroyAnimation()
    {
        PlayDestroySound();
        _animator.SetBool("IsDestroy", true);

        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        Invoke(nameof(WaitDestroy), 0.5f);
    }
    private void PlayDestroySound()
    {
        if (destroySound != null)
        {
            audioSource.clip = destroySound;
            audioSource.Play();
        }
    }
    private void WaitDestroy() => Destroy(this.gameObject);
    public void Initialize(Vector2 playerPosition) => targetPosition = playerPosition;
}