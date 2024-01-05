using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObject : MonoBehaviour
{
    public GameObject[] myObjects;
    public GameObject plane;
    public float spawnDelay = 1.0f;

    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            StartCoroutine(SpawnObjectsWithDelay());
        }
    }

    IEnumerator SpawnObjectsWithDelay()
    {
        while (true)
        {
            SpawnRandomObject();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnRandomObject()
    {
        int randomIndex = Random.Range(0, myObjects.Length);
        Renderer planeRenderer = plane.GetComponent<Renderer>();
       
        float maxX = planeRenderer.bounds.max.x;
        float minX = planeRenderer.bounds.min.x;
        float minZ = planeRenderer.bounds.min.z;
        float maxZ = planeRenderer.bounds.max.z;

        // Calculate a random position within the bounds of the plane
        Vector3 randomSpawnPos = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));


        Instantiate(myObjects[randomIndex], randomSpawnPos, Quaternion.identity);
    }
}

