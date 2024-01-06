using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
//using UnityEngine.GeometryUtility;

public class CitySpawner : MonoBehaviour
{
    private ARPlaneManager planeManager;
    private GameObject[] cityObjects;

    private void Start()
    {
        planeManager = FindObjectOfType<ARPlaneManager>();
        planeManager.planesChanged += OnPlanesChanged;

        // Assign city objects here (adjust based on your setup)
        cityObjects = GameObject.FindGameObjectsWithTag("CityObject");

        // Ensure all city objects are inactive initially
        foreach (GameObject cityObject in cityObjects)
        {
            cityObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        planeManager.planesChanged -= OnPlanesChanged;
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        foreach (ARPlane plane in args.added)
        {
            HandlePlaneAdded(plane);
        }

        foreach (ARPlane plane in args.updated)
        {
            HandlePlaneUpdated(plane);
        }

        foreach (ARPlane plane in args.removed)
        {
            HandlePlaneRemoved(plane);
        }
    }

    private void HandlePlaneAdded(ARPlane plane)
    {
        foreach (GameObject cityObject in cityObjects)
        {
            if (IsObjectWithinPlaneBounds(cityObject, plane))
            {
                cityObject.SetActive(true);
            }
        }
    }

    private void HandlePlaneUpdated(ARPlane plane)
    {
        // Re-check visibility of all city objects using updated plane bounds
        foreach (GameObject cityObject in cityObjects)
        {
            cityObject.SetActive(IsObjectWithinPlaneBounds(cityObject, plane));
        }
    }

    private void HandlePlaneRemoved(ARPlane plane)
    {
        // Disable all city objects, as it's unclear which were on the removed plane
        foreach (GameObject cityObject in cityObjects)
        {
            cityObject.SetActive(false);
        }
    }

    private bool IsObjectWithinPlaneBounds(GameObject obj, ARPlane plane)
    {
        Vector3 objectPositionOnPlane = plane.transform.InverseTransformPoint(obj.transform.position);
        //Vector3 worldPos = obj.transform.position;
        //Bounds worldBounds = plane.GetComponent<Collider>().bounds;
        //return worldBounds.Contains(worldPos);
        //Vector3 objectPositionInPlane = plane.transform.ProjectPoint(obj.transform.position);
        //Vector3 objectPositionOnPlane = GeometryUtility.ProjectPoint(obj.transform.position, plane.transform.up);
        //return Mathf.Abs(objectPositionOnPlane.x) <= plane.extents.x &&
        //Mathf.Abs(objectPositionOnPlane.z) <= plane.extents.y;

        //Vector3 objectPositionOnPlane = Vector3.ProjectOnPlane(obj.transform.position, plane.transform.up);

        return Mathf.Abs(objectPositionOnPlane.x) <= plane.extents.x && Mathf.Abs(objectPositionOnPlane.z) <= plane.extents.y;
    }
}
