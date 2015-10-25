using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public GameObject enemy;
    public Transform[] spawnPoints;
    protected float spawnCooldown;
    protected float timeUntilSpawn;

    
    public float spawnMin;
    public float spawnMax;

    public float spawnMinFloor;
    public float spawnMaxFloor;
    public float spawnDecrement;

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
            spawnCooldown = Random.Range(spawnMin * 1.0f, spawnMax * 1.0f);
            timeUntilSpawn = spawnCooldown;
            if(spawnMin > spawnMinFloor)
                spawnMin -= spawnDecrement;

            if (spawnMin < spawnMinFloor)
                spawnMin = spawnMinFloor;

            if(spawnMax > spawnMaxFloor)
                spawnMax -= spawnDecrement;

            if (spawnMax < spawnMinFloor)
                spawnMax = spawnMinFloor;
        }
	}
}
