using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossSpike : MonoBehaviour
{
    [SerializeField] public GameObject _prefabSpike;
    [SerializeField] private AudioClip _sound;

    private Animator _anim;
    private AudioSource _audioSource;
    public IEnumerator SpikeUp(GameObject prefabSpike)
    {
        if (prefabSpike != null)
            yield return new WaitForSeconds(1.5f);
        Explode(prefabSpike);
    }
    private void Explode(GameObject spike) => Destroy(spike);
    public void PlaySound()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _sound;
        _audioSource.Play();
    }
}
