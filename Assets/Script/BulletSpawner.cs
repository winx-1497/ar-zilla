using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    //public float minSpawnInterval = 6.0f;
    //public float maxSpawnInterval = 7.0f;
    public float spawnInterval = 6.0f;
    public float bulletLifespan = 10.0f;

    public GameObject playerCollider;
    private float timer = 0.0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Destroy(transform.parent.gameObject);
        }
       
        timer += Time.deltaTime;

        // Check if it's time to spawn a new bullet
        if (timer >= spawnInterval)
        {
            SpawnBullet();
            timer = 0.0f;
        }
    }

    void SpawnBullet()
    {
        // Instantiate a new bullet at the spawn point
        GameObject newBullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        newBullet.GetComponent<MoveToObject>().Body = playerCollider;
        // Set a lifespan for the bullet
        Destroy(newBullet, bulletLifespan);

    }
}
