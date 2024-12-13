using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSpawner : MonoBehaviour
{
    public Virus virus;
    public Transform _computer;
    public GameObject virusPrefab;
    public PlayerController playerController;

    public float spawnInterval = 3f;
    public float spawnRadius = 10f;
    public int maxViruses = 10;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnVirus), 0f, spawnInterval);
    }
    public void SpawnVirus()
    {
        if (GM.IsPlayingRoomVirus)
        {
            if (GameObject.FindGameObjectsWithTag("Virus").Length < maxViruses)
            {
                Vector2 spawnPosition2D = Random.insideUnitCircle * spawnRadius;
                Vector3 spawnPosition = new Vector3(spawnPosition2D.x, spawnPosition2D.y, -1);

                GameObject newVirus = Instantiate(virusPrefab, spawnPosition, Quaternion.identity);
                virus.GetComponent<Virus>().Initialize(_computer.position);
            }
        }
    }
}