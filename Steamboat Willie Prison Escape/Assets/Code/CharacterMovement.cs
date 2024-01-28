using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public KeyCode jumpKey;
    public float jumpForce = 5f;
    public Rigidbody rb;
    public Transform otherCharacter;
    public float maxDistance = 2f;

    public bool useWASD; // Set this to true for the first character, false for the second

    private Vector3 jumpDirection;
    private Vector3 initialJumpDirection; // Direction captured at the start of the jump
    private float jumpTimeCounter;
    public float maxJumpTime = 1f;
    public bool isJumping;
    private bool isGrounded; // Track if the character is grounded

    // Cylinder 
    public CylinderPositionController cylinderController;
    private Vector3 medianPosition;
    //rotation of movement
    public PlayerMovement playerMovement;



    private void Start()
    {
        isGrounded = true; // Start as grounded
    }

    //void Update()
    //{
        void Update()
        {
            ControlScheme controlScheme = playerMovement.GetCurrentControlScheme();

            // Update median position
            medianPosition = (cylinderController.player1.position + cylinderController.player2.position) / 2;

            // Determine jump direction based on input
            if (!isJumping) // Only update direction if not already jumping
            {
                jumpDirection = Vector3.zero;
                jumpDirection.x = Input.GetKey(controlScheme.right) ? 1 : Input.GetKey(controlScheme.left) ? -1 : 0;
                jumpDirection.z = Input.GetKey(controlScheme.forward) ? 1 : Input.GetKey(controlScheme.backward) ? -1 : 0;
            }

            // Jump logic
            if (Input.GetKeyDown(controlScheme.jump) && isGrounded)
            {
                Vector3 intendedJumpPosition = transform.position + jumpDirection * jumpForce;
                if (Vector3.Distance(intendedJumpPosition, medianPosition) <= cylinderController.cylinderRadius)
                {
                    // Capture initial jump direction and perform jump
                    initialJumpDirection = jumpDirection;
                    isJumping = true;
                    isGrounded = false; // Character is no longer grounded
                    jumpTimeCounter = maxJumpTime;
                    rb.velocity = new Vector3(initialJumpDirection.x * jumpForce, rb.velocity.y, initialJumpDirection.z * jumpForce);
                }
                else
                {
                    Debug.Log("Jump too far!");
                }
            }

            if (Input.GetKey(controlScheme.jump) && isJumping)
            {
                if (jumpTimeCounter > 0)
                {
                    rb.velocity = new Vector3(initialJumpDirection.x * jumpForce, jumpForce, initialJumpDirection.z * jumpForce);
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }

            if (Input.GetKeyUp(controlScheme.jump))
            {
                isJumping = false;
            }
        }


        // Detect if the character is grounded when colliding with the ground
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
        }

        // Detect when the character leaves the ground
        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = false;
            }
        }
    //}
}
