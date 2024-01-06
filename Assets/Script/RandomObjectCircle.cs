using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectCircle : MonoBehaviour
{
    public GameObject[] myObjects;
    public GameObject plane;
    public GameObject player;

    public float spawnDelay = 2.0f;
    public int numPoints = 3;
    
    public LayerMask enemyLayer;

    public float circleRadius = 5f;  

    // Create a dictionary to store the positions of the objects
    Dictionary<Vector3, GameObject> objectPositions = new Dictionary<Vector3, GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnObjectsWithDelay());
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Create a ray from the touch position
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayer))
            {
                // Check if an object was hit
                if (hit.transform != null)
                {
                    // Remove the hit object's position from the dictionary
                    objectPositions.Remove(hit.transform.position);

                    // Destroy the hit object
                    Destroy(hit.transform.parent.gameObject);
                }
            }
        }
    }

    IEnumerator SpawnObjectsWithDelay()
    {
        while (true)
        {
            // Only spawn a new object if there are available positions
            if (objectPositions.Count < numPoints)
            {
                SpawnRandomObject();
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnRandomObject()
    {
        float radius = 10f;
        int randomPointIndex = Random.Range(0, numPoints);
        float angle = 2 * Mathf.PI * randomPointIndex / numPoints;

        float x = radius * Mathf.Cos(angle);
        float z = radius * Mathf.Sin(angle);

        Vector3 randomSpawnPos = new Vector3(x, -1, z);

        // Check if the position is already occupied
        if (!objectPositions.ContainsKey(randomSpawnPos))
        {
            int randomObjectIndex = Random.Range(0, myObjects.Length);

            GameObject newObj = Instantiate(myObjects[randomObjectIndex], randomSpawnPos, Quaternion.identity);
            newObj.GetComponentInChildren<BulletSpawner>().playerCollider = player;
           
            newObj.transform.LookAt(player.transform);

            objectPositions.Add(randomSpawnPos, newObj);
        }

        // If all points are occupied, stop spawning
        if (objectPositions.Count >= numPoints)
        {
            StopCoroutine(SpawnObjectsWithDelay());
        }
    }
}
