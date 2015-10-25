using UnityEngine;
using System.Collections;

public class ProjectileEnemy : BaseEnemy {
    public GameObject projectile;
    public float spawnCooldown;
    private float timeUntilSpawn;
    // Use this for initialization
    void Awake()
    {
        
        timeUntilSpawn = 0;
    }

    // Update is called once per frame
    void Update () {
        timeUntilSpawn -= Time.deltaTime;
        if (timeUntilSpawn <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeUntilSpawn = spawnCooldown;
        }
        base.Update();
	}
}
