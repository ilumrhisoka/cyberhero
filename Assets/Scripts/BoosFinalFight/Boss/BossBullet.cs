using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [Space(10)]
    [SerializeField] public GameObject _prefabBullet;

    [Space(10)]
    [SerializeField] private float _speedBullet;
    [SerializeField] private float _lifetime;

    public IEnumerator MoveBullet(GameObject prefabBullet, Transform playerPosition)
    {
        Transform prefabBulletTransform = prefabBullet.transform;
        float elapsedTime = 0f;

        while (prefabBullet != null)
        {
            Vector2 targetPoint = playerPosition.position;
            prefabBulletTransform.position = Vector2.MoveTowards(prefabBulletTransform.position, targetPoint, _speedBullet * Time.deltaTime);
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= _lifetime)
            {
                Explode(prefabBullet);
                yield break;
            }
            yield return null;
        }
        Explode(prefabBullet);
    }
    private void Explode(GameObject grenade) => Destroy(grenade);
}