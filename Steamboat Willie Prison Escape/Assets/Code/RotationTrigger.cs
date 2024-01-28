using UnityEngine;

public class RotationTrigger : MonoBehaviour
{
    private int playerCounter = 0; // Counter for the players inside the trigger
    public int rotationVariable = 0; // Variable that represents the state (0, 1, or 2)

    public bool triggerInRoom = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the "Player" tag
        if (other.CompareTag("Player"))
        {
            playerCounter++; // Increment the counter
            UpdateRotationVariable(); // Update the rotation variable based on the counter
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger has the "Player" tag
        if (other.CompareTag("Player"))
        {
            playerCounter--; // Decrement the counter
            UpdateRotationVariable(); // Update the rotation variable based on the counter
        }
    }

    private void UpdateRotationVariable()
    {
        if (playerCounter == 7)
        {
            // If exactly two players are inside the trigger, set zrotated
            CameraFollow.zrotated = rotationVariable;
            if (triggerInRoom)
            {
                CameraFollow.inRoom = true;
            }
        }
        else
        {
            // If the number of players inside is not 2, you might want to reset zrotated
            // CameraFollow.zrotated = defaultValue; // (set this if needed)
            if (triggerInRoom)
            {
                CameraFollow.inRoom = false;
            }
        }
    }
}
