using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToObject : MonoBehaviour
{
    
    public GameObject Bullet;
    public GameObject Body;
    public float speed;
    private Vector3 initialCameraPosition;
    private bool isBulletMoving = false;

    void Start()
    {
        // Store the initial position of the camera
        if (Body != null)
        {
            initialCameraPosition = Body.transform.position;
            isBulletMoving = true; // Start moving the bullet
        }
    }

    void Update()
    {
        // Check if the Bullet is not null before attempting to move
        if (Bullet != null && isBulletMoving)
        {
            // Move the Bullet towards the initial position of the camera over time
            Bullet.transform.position = Vector3.MoveTowards(Bullet.transform.position, initialCameraPosition, speed * Time.deltaTime);

            // Check if the bullet has reached the initial camera position
            if (Vector3.Distance(Bullet.transform.position, initialCameraPosition) < 0.01f)
            {
                // Stop moving the bullet without destroying it
                isBulletMoving = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Trigger entered!");

            // Reduce the health of the body
            if (Body != null)
            {
                HealthScript healthScript = Body.GetComponent<HealthScript>();
                if (healthScript != null)
                {
                    healthScript.TakeDamage(5); // Assuming TakeDamage function exists in HealthScript
                    Debug.Log("Health reduced! Current Health: " + healthScript.GetCurrentHealth());
                }
            }

            Destroy(Bullet);
            //isBulletMoving = false;
        }
    }
}
