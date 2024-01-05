using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float minSpawnInterval = 100.0f;
    public float maxSpawnInterval = 120.0f;
    public float bulletLifespan = 10.0f;
    public GameObject body;

    private float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Check if it's time to spawn a new bullet
        if (timer >= Random.Range(minSpawnInterval, maxSpawnInterval))
        {
            // Spawn a bullet at the spawn point
            SpawnBullet();

            // Reset the timer
            timer = 0.0f;
        }
    }

    void SpawnBullet()
    {
        // Instantiate a new bullet at the spawn point
        GameObject newBullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        newBullet.GetComponent<MoveToObject>().Body = body;
        // Set a lifespan for the bullet
        Destroy(newBullet, bulletLifespan);

    }
}
