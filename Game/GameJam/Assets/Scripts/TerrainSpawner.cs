using UnityEngine;
using System.Collections;

public class TerrainSpawner : Spawner {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timeUntilSpawn -= Time.deltaTime;
        if (timeUntilSpawn <= 0)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            Instantiate(enemy, spawnPoints[spawnPointIndex].position, Quaternion.identity);
            spawnCooldown = Random.Range(2, 15);
            timeUntilSpawn = spawnCooldown;
        }
    }
}
