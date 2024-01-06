using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectCircle : MonoBehaviour
{
    public GameObject[] myObjects;
    public GameObject plane;
    public float spawnDelay = 2.0f;
    public int numPoints = 6;
    public GameObject player;
    public LayerMask enemyLayer;

    // public Transform circleCenter;  // Reference to the center of the circle
    public float circleRadius = 5f;  // Radius of the circle

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
        // Define the radius of the circular path
        float radius = 10f;

        // Define the number of points
        

        // Generate a random index for the points
        int randomPointIndex = Random.Range(0, numPoints);

        // Calculate the angle for each point
        float angle = 2 * Mathf.PI * randomPointIndex / numPoints;

        // Calculate the x and z coordinates using the angle and radius
        float x = radius * Mathf.Cos(angle);
        float z = radius * Mathf.Sin(angle);

        // Create the spawn position vector
        Vector3 randomSpawnPos = new Vector3(x, -5, z);

        // Check if the position is already occupied
        if (!objectPositions.ContainsKey(randomSpawnPos))
        {
            // Generate a random index for the objects
            int randomObjectIndex = Random.Range(0, myObjects.Length);

            // Instantiate the object at the calculated position
            GameObject newObj = Instantiate(myObjects[randomObjectIndex], randomSpawnPos, Quaternion.identity);
            newObj.GetComponentInChildren<BulletSpawner>().body = player;
            // Make the object face the player
            newObj.transform.LookAt(player.transform);

            // Add the new object's position to the dictionary
            objectPositions.Add(randomSpawnPos, newObj);
        }

        // If all points are occupied, stop spawning
        if (objectPositions.Count >= numPoints)
        {
            StopCoroutine(SpawnObjectsWithDelay());
        }
    }
}
