using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent(requiredComponent: typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class PlaceObject : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    private ARRaycastManager arRaycastManager;
    private ARPlaneManager arPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        arPlaneManager = GetComponent<ARPlaneManager>();
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    private void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        // Commented out to avoid starting generation on every finger down
        //EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    private void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        // Commented out to avoid potential memory leaks
        //EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }

    private void Start()
    {
        GenerateObjectsOnPlanes();
    }

    private void GenerateObjectsOnPlanes()
    {
        arRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon);

        foreach (ARRaycastHit hit in hits)
        {
            Pose pose = hit.pose;
            GameObject obj = Instantiate(prefab, pose.position, pose.rotation);

            if (arPlaneManager.GetPlane(hit.trackableId).alignment == PlaneAlignment.HorizontalUp)
            {
                // Adjust object rotation based on your requirements
                Vector3 position = obj.transform.position;
                Vector3 cameraPosition = Camera.main.transform.localPosition;
                Vector3 direction = cameraPosition - position;
                Vector3 targetRotationEuler = Quaternion.LookRotation(direction).eulerAngles;
                Vector3 scaledEuler = Vector3.Scale(targetRotationEuler, obj.transform.up.normalized);
                Quaternion targetRotation = Quaternion.Euler(scaledEuler);
                obj.transform.rotation = obj.transform.rotation * targetRotation;
            }
        }
    }
}
