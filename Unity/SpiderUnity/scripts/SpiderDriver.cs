using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpiderDriver : MonoBehaviour
{
    public Transform target;
    public float radius1, radius2;
    private Locomotion locomotion;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Generate a random point if the buffer is empty
        if (locomotion.buffer.Count == 0)
        {
            var randomPoint = CheckpointGenerator.Instance.Generate(target.position, transform.position, radius1, radius2);
            locomotion.buffer.Add(randomPoint);
        }
        // Handle jump input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            locomotion.Jump();
        }
    }


    private void Awake()
    {
        locomotion = GetComponent<Locomotion>();
    }
    /*
    private void OnDrawGizmos()
    {
        if (target == null)
            return;
        Handles.color = Color.red;
        Handles.DrawWireDisc(target.position, Vector3.up, radius1);
        Handles.DrawWireDisc(target.position, Vector3.up, radius2);
        Handles.DrawSolidDisc(randomPoint.Item1, Vector3.up, 0.3f);
        Handles.DrawSolidDisc(randomPoint.Item2, Vector3.up, 0.3f);
        Handles.DrawLine(randomPoint.Item1, randomPoint.Item2);
    }
    */
}
