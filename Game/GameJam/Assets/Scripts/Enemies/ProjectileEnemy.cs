using UnityEngine;
using System.Collections;

public class ProjectileEnemy : BaseEnemy {
    public GameObject projectile;
    protected float spawnCooldown;
    protected float timeUntilSpawn;
    // Use this for initialization
    void Awake()
    {
        spawnCooldown = 1;
        timeUntilSpawn = spawnCooldown;
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
