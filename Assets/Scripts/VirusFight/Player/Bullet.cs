using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    
    private Vector2 direction;

    private void Update()
    {
        if(GM.IsPlayingRoomVirus) 
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GM.IsPlayingRoomVirus)
        {
            Virus virus = other.GetComponent<Virus>();

            if (other.CompareTag("Virus") && virus != null)
            {
                virus.DestroyAnimation();
                Destroy(gameObject);
                GM.CountOfKilledViruses++;
                FindObjectOfType<VirusSpawner>().SpawnVirus();
            }

            VirusBoom virusBoom = other.GetComponent<VirusBoom>();

            if (other.CompareTag("VirusBoom") && virusBoom != null)
            {
                virusBoom.DestroyAnimation();
                Destroy(gameObject);
                GM.CountOfKilledViruses++;
                FindObjectOfType<VirusBoomSpawner>().SpawnVirus();
            }
        }
    }
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        RotateTowardsDirection();
        Destroy(gameObject, 5f);
    }
    private void RotateTowardsDirection()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}