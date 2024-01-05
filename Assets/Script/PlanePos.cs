using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePos : MonoBehaviour
{
    public Transform cameraTransform; // Reference to the AR camera transform
    public float xOffset = 0f; // Offset on the X-axis
    public float zOffset = 0f; // Offset on the Z-axis

    void Update()
    {
        if (cameraTransform != null)
        {
            // Get the current position of the camera
            Vector3 cameraPos = cameraTransform.position;

            // Set the plane's position with a constant Y-coordinate and offsets on X and Z axes
            transform.position = new Vector3(cameraPos.x + xOffset, -10f, cameraPos.z + zOffset);

            // Keep the original rotation of the plan
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }
}