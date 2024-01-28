using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float rotationSpeed = 720f; // Degrees per second
    public CharacterMovement characterMovementScript; // Reference to the CharacterMovement script
    public PlayerMovement playerMovement; // Reference to the PlayerMovement script to get the current control scheme

    void Update()
    {
        // Only rotate the player if they are not jumping
        if (!characterMovementScript.isJumping)
        {
            // Determine rotation direction based on input
            float horizontalInput = 0;
            float verticalInput = 0;

            // Get the current control scheme from PlayerMovement
            ControlScheme controlScheme = playerMovement.GetCurrentControlScheme();

            // Use the control scheme for input
            horizontalInput = Input.GetKey(controlScheme.right) ? 1 : Input.GetKey(controlScheme.left) ? -1 : 0;
            verticalInput = Input.GetKey(controlScheme.forward) ? 1 : Input.GetKey(controlScheme.backward) ? -1 : 0;

            Vector3 inputDirection = new Vector3(horizontalInput, 0, verticalInput);

            if (inputDirection != Vector3.zero)
            {
                // Create a rotation based on the input direction
                Quaternion toRotation = Quaternion.LookRotation(inputDirection, Vector3.up);

                // Smoothly rotate towards the target rotation
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}