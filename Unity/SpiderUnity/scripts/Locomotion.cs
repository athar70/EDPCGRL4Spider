using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Locomotion : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> buffer;              // Buffer to hold target positions for movement
    public float moveSpeed;                     // Speed of movement
    public float rotationSpeed;                  // Speed of rotation
    public float epsilon;                        // Tolerance for reaching target points
    public float jumpIntensity;                  // Force of the jump
    public float jumpYComponent;                 // Vertical component of the jump force
    public float jumpDelayBefore;                // Delay before jumping
    public float jumpDelayAfter;                 // Delay after jumping
    public float waitAfterReachingPoint;         // Wait time after reaching a point
    public bool isStanding = false;              // Flag to check if the character is standing
    private Rigidbody rigidbody;                 // Reference to the Rigidbody component

    public UnityEvent onJump;                   // Event triggered on jump
    public UnityEvent onIdle;                   // Event triggered on idle
    public UnityEvent onWalking;                 // Event triggered on walking
    public UnityEvent onLand;                    // Event triggered on landing

    private Animator animator;                   // Reference to the Animator component
    public float animatorWalkSpeed = 1f;        // Speed parameter for walking animation
    public float animatorJumpSpeed = 1f;        // Speed parameter for jumping animation
    public float animatorIdleSpeed = 1f;        // Speed parameter for idle animation

    private AudioSource soundWalk;               // Audio source for walking sound
    private AudioSource soundRear;               // Audio source for rear sound

    public bool isJumping;                       // Flag to check if the character is jumping
    public bool isWalking
    {
        get { return !isStanding && !isJumping; }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize audio sources
        AudioSource[] audios = GetComponents<AudioSource>();
        soundWalk = audios[1];
        soundRear = audios[0];

        // Set target frame rate
        Application.targetFrameRate = 59;
        animator = GetComponent<Animator>();      // Get the Animator component
    }

    void Update()
    {
        Move();                                   // Handle movement
        SetAnimatorSpeed();                       // Update animator speeds
    }

    // Coroutine to handle the jump action
    private IEnumerator JumpCoroutine(float delayBefore, float delayAfter, float intensity, float yComponent)
    {
        isJumping = true;
        onJump.Invoke();                          // Trigger jump event
        yield return new WaitForSeconds(delayBefore);
        
        // Apply jump force
        rigidbody.AddForce((transform.forward + Vector3.up * yComponent).normalized * intensity);
        onIdle.Invoke();                          // Trigger idle event
        yield return new WaitForSeconds(delayAfter);
        
        isJumping = false;
        onLand.Invoke();                          // Trigger land event
    }

    // Initiate the jump action
    private void Jump(float delayBefore, float delayAfter, float intensity, float yComponent)
    {
        StopAllCoroutines();                      // Stop any ongoing coroutines
        StartCoroutine(JumpCoroutine(delayBefore, delayAfter, intensity, yComponent));
    }

    // Public method to call for jumping
    public void Jump()
    {
        Jump(jumpDelayBefore, jumpDelayAfter, jumpIntensity, jumpYComponent);
    }

    // Handle movement logic
    public void Move()
    {
        if (isJumping) return;                   // Prevent movement while jumping
        if (isStanding)
        {
            onIdle.Invoke();                     // Trigger idle event
            return;
        }

        if (buffer.Count > 0)
        {
            onWalking.Invoke();                  // Trigger walking event
            var targetPosition = buffer[0];     // Get the next target position
            targetPosition.y = transform.position.y; // Keep the current y position

            // Check if the target position is reached
            if ((transform.position - targetPosition).magnitude <= epsilon)
            {
                buffer.RemoveAt(0);              // Remove reached position from buffer
                return;
            }

            // Move towards the target position
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0;
            Vector3 velocity = transform.forward * moveSpeed * (isStanding ? 0 : 1);
            velocity.y = rigidbody.velocity.y;    // Maintain the current vertical velocity
            rigidbody.velocity = velocity;         // Update Rigidbody velocity
            rigidbody.MoveRotation(Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction.normalized, Vector3.up), Time.deltaTime * rotationSpeed));
        }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();    // Get the Rigidbody component
    }

    // Callback method for jump
    public void JumpCallback()
    {
        Debug.Log("Jump!!!");
        soundWalk.Pause();
        if (!soundRear.isPlaying)
        {
            soundRear.Play();                    // Play rear sound
        }
        animator.SetBool("Standing", false);
        animator.SetTrigger("jumping");          // Trigger jumping animation
        animator.SetBool("Walking", false);
    }

    // Callback method after finishing jump
    public void FinishJumpCallback()
    {
        Debug.Log("Just has jumped!");
        soundRear.Pause();
    }

    // Callback method for idle state
    public void IdleCallback()
    {
        Debug.Log("I'm idle right now!!!");
        soundWalk.Pause();
        animator.SetBool("standing", true);
        animator.SetBool("walking", false);
    }

    // Callback method for walking state
    public void WalkCallback()
    {
        Debug.Log("walking!!!");
        if (!soundWalk.isPlaying)
        {
            soundWalk.Play();                   // Play walking sound
            soundWalk.pitch = moveSpeed / 3;   // Adjust pitch based on movement speed
        }
        soundRear.Pause();
        animator.SetBool("standing", false);
        animator.SetBool("walking", true);       // Trigger walking animation
    }

    // Set animator speeds
    private void SetAnimatorSpeed()
    {
        animator.SetFloat("walkSpeed", animatorWalkSpeed);
        animator.SetFloat("jumpSpeed", animatorJumpSpeed);
        animator.SetFloat("idleSpeed", animatorIdleSpeed);
    }
}

// Class to hold orientation data
public class Orientation
{
    public Vector3 position;                   // Position vector
    public Quaternion rotation;                // Rotation quaternion
}
