using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotation : MonoBehaviour
{
    // Start is called before the first frame update
    private string pathLogFile = "Assets/Resources/LogCameraRotation.txt";
    public float delayedLog = 1f; // Delay in seconds for logging
    Logger logger;

    void Start()
    {
        logger = new Logger();
        logger.createText(pathLogFile);
        logger.writeTitle(pathLogFile);

        // Start repeating the logging function with a specified delay
        InvokeRepeating("writeValue", delayedLog, delayedLog);  //1s delay, repeat every 1s
    }

    void writeValue()
    {
        var rot = transform.rotation;
        logger.writeValue(pathLogFile, rot.ToString());

    }
}
