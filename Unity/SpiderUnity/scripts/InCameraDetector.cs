using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCameraDetector : MonoBehaviour
{
    public Camera camera;              // Reference to the camera
    private Collider collider;         // Reference to the collider
    private Plane[] cameraFrustum;     // Array to hold the camera frustum planes

    private bool isLooking = false;     // Flag indicating if the object is currently in view
    private bool wasLooking = false;     // Previous state of the looking flag
    private System.DateTime startLooking; // Timestamp when the object started being in view
    private System.DateTime stopLooking;  // Timestamp when the object stopped being in view
    private double totalTime = 0;       // Total time the object has been in view

    private string pathLogFile = "Assets/Resources/LogInCamera.txt"; // Path for logging
    private Logger logger;              // Instance of the Logger class

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>(); // Get the collider component
        logger = new Logger();
        logger.CreateText(pathLogFile);      // Create log file if it doesn't exist
        logger.WriteTitle(pathLogFile);       // Write title to the log file
    }

    // Update is called once per frame
    void Update()
    {
        // Get the bounding box of the collider
        var bounds = collider.bounds;
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(camera); // Calculate camera frustum planes
        
        // Check if the collider's bounds intersect with the camera frustum
        if (GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
        {
            isLooking = true; // Object is in camera view
        }
        else
        {
            isLooking = false; // Object is not in camera view
        }

        // Track the time the object is in view
        if (isLooking && !wasLooking)
        {
            startLooking = System.DateTime.UtcNow; // Start time when the object comes into view
            wasLooking = true;
        }
        if (!isLooking && wasLooking)
        {
            stopLooking = System.DateTime.UtcNow; // Stop time when the object goes out of view
            wasLooking = false;
            totalTime += (stopLooking - startLooking).TotalSeconds; // Update total viewing time
        }
    }

    // Called when the object is destroyed
    private void OnDestroy()
    {
        // Log the total time the object was in view
        logger.WriteValue(pathLogFile, totalTime.ToString());
    }
}
