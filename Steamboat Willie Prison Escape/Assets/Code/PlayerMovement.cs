using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CameraFollow cameraFollowScript; // Reference to the CameraFollow script
    public bool isPlayer1 = true; // Set this in the inspector to differentiate between players

    private ControlScheme currentControlScheme;

    void Update()
    {
        // Update the current control scheme based on whether it's player 1 or 2
        currentControlScheme = isPlayer1 ? cameraFollowScript.currentControlSchemePlayer1 : cameraFollowScript.currentControlSchemePlayer2;

        // Rest of your code (e.g., handling input based on currentControlScheme)
    }

    // Expose the current control scheme for other scripts
    public ControlScheme GetCurrentControlScheme()
    {
        return currentControlScheme;
    }
}
