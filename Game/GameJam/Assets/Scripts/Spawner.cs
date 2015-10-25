using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public GameObject enemy;
    public Transform[] spawnPoints;
    protected float spawnCooldown;
    protected float timeUntilSpawn;
    public float spawnMin;
    public float spawnMax;

	// Use this for initialization
	void Awake () {
        spawnCooldown = Random.Range(spawnMin, spawnMax);
        timeUntilSpawn = spawnCooldown;
	}
	
	// Update is called once per frame
	void Update () {
        timeUntilSpawn -= Time.deltaTime;
        if (timeUntilSpawn <= 0)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            Instantiate(enemy, spawnPoints[spawnPointIndex].position, Quaternion.identity);
            spawnCooldown = Random.Range(spawnMin, spawnMax);
            timeUntilSpawn = spawnCooldown;
        }
	}
}
