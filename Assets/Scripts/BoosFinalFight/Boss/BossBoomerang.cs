using System.Collections;
using UnityEngine;

public class BossBoomerang : MonoBehaviour
{
    [Space(10)]
    [SerializeField] public GameObject _prefabBoomerang;
    [SerializeField] private AudioClip _sound;

    [Space(10)]
    [SerializeField] private float speed = 0.5f;

    [Space(10)]
    [Range(0, 1)]
    public float t;

    private AudioSource _audioSource;

    public bool isMoving = true;

    public IEnumerator MoveBoomerang(GameObject prefabBoomerang, Transform[] _pointsBezier)
    {
        while (isMoving)
        {
            prefabBoomerang.transform.position = Bezier.GetPoint(
            _pointsBezier[0].position,
            _pointsBezier[1].position,
            _pointsBezier[2].position,
            _pointsBezier[3].position,
            t);

            t += Time.deltaTime * speed;

            if (t >= 1f)
            {
                t = 1f;
                isMoving = false; 
                Explode(prefabBoomerang);
            }
            yield return null;
        }
    }
    private void Explode(GameObject spike) => Destroy(spike);
    public void PlaySound()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _sound;
        _audioSource.Play();
    }
}