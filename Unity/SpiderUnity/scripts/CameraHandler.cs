using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    public float speed;

    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Smoothly interpolate the camera's position towards the target's position
        pos = Vector3.Lerp(pos, target.position, Time.deltaTime * speed);
        
        // Update the camera's position by adding the offset
        transform.position = pos + offset;
    }

    private void Awake()
    {
        // Calculate the initial offset based on the current position of the camera and the target
        offset = transform.position - target.position;
    }
}
