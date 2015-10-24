using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public GameObject enemy;
    public Transform[] spawnPoints;
    protected float spawnCooldown = 1;
    protected float timeUntilSpawn = 0;
    public float spawnMin = .5f;
    public float spawnMax = 2;

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
            spawnCooldown = Random.Range(spawnMin, spawnMax);
            timeUntilSpawn = spawnCooldown;
        }
	}
}
