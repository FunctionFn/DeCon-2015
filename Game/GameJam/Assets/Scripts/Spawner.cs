using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public GameObject enemy;
    public Transform[] spawnPoints;
    public float spawnCooldown = 1;
    private float timeUntilSpawn = 0;

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
            timeUntilSpawn = spawnCooldown;
        }
	}
}
