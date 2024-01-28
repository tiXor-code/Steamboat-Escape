using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public Transform cylinder; // Reference to the cylinder's transform
    public float smoothTime = 0.5f; // Time for the camera to catch up to the target position
    public float minDistance = 5f; // Minimum distance of the camera from the players
    public float maxDistance = 10f; // Maximum distance of the camera from the players
    public Vector3 offset = new Vector3(0, 2, -5); // Offset of the camera position
    public LayerMask wallLayer; // LayerMask to detect walls
    public float lookUpOffset = 1.0f; // Height above the cylinder position the camera should look at

    private Vector3 velocity = Vector3.zero;
    [SerializeField] private float currentYRotation = 0f; // Serialized field to store the current Y rotation of the camera
    private bool isRotating = false; // Bool to control whether to apply Y rotation
    public static int zrotated = 0;
    public Vector3 zoffset;
    public float bigValue;
    public float smallValue;

    public static bool inRoom = false;

    public ControlScheme player1DefaultControls; // WASD
    public ControlScheme player1RightControls;
    public ControlScheme player1LeftControls;
    public ControlScheme player1RotatedControls; // Reversed controls for rotation
    public ControlScheme player2DefaultControls; // Arrow keys
    public ControlScheme player2RightControls;
    public ControlScheme player2LeftControls;
    public ControlScheme player2RotatedControls; // Reversed controls for rotation

    public  ControlScheme currentControlSchemePlayer1;
    public  ControlScheme currentControlSchemePlayer2;
    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is a trigger that changes the Y rotation
        if (other.CompareTag("CameraRotationTrigger"))
        {
            // Get the new Y rotation value from the trigger
            float newYRotation = other.GetComponent<RotateCameraCollider>().newYRotation;

            // Update the camera's Y rotation
            currentYRotation = newYRotation;

            // Set isRotating to true to activate Y rotation
            isRotating = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the colliding object is a trigger that changes the Y rotation
        if (other.CompareTag("CameraRotationTrigger"))
        {
            // Set isRotating to false to deactivate Y rotation
            isRotating = false;
        }
    }
    
    void Update()
    {
        // Calculate the median position of the players
        Vector3 medianPosition = (player1.position + player2.position) / 2;
        float distanceBetweenPlayers = Vector3.Distance(player1.position, player2.position);

        // Adjust camera distance based on the distance between players
        float cameraDistance = Mathf.Clamp(distanceBetweenPlayers, minDistance, maxDistance);

        // Calculate the desired position of the camera
        //Vector3 desiredPosition = medianPosition + offset.normalized * cameraDistance;
        if (inRoom == true)
        {
            Debug.Log("CameraFollow: Player is in the room.");
            minDistance = 3f;
        }
        else
        {
            Debug.Log("CameraFollow: Player is not in the room.");
            minDistance = 5f;
        }
        if (zrotated == 0)//forward
        {
             zoffset = new Vector3(0, smallValue, -bigValue);
            currentControlSchemePlayer1 = player1DefaultControls;
            currentControlSchemePlayer2 = player2DefaultControls;
        }
        else if (zrotated == 1)//right
        {
             zoffset = new Vector3(-bigValue, smallValue, 0);
            currentControlSchemePlayer1 = player1RightControls;
            currentControlSchemePlayer2 = player2RightControls;
        }
        else if (zrotated == -1) //left
        {
            zoffset = new Vector3(bigValue, smallValue, 0);
            currentControlSchemePlayer1 = player1LeftControls;
            currentControlSchemePlayer2 = player2LeftControls;
        }
        else if (zrotated == -2) //left
        {
            zoffset = new Vector3(0, smallValue, bigValue);
            currentControlSchemePlayer1 = player1RotatedControls;
            currentControlSchemePlayer2 = player2RotatedControls;
        }

        Vector3 desiredPosition = medianPosition + zoffset.normalized * cameraDistance;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);




        // Check for walls between the camera and the median position of the players
        if (Physics.Linecast(medianPosition, smoothedPosition, out RaycastHit hit, wallLayer))
        {
            // If there's a wall, position the camera right in front of the wall
            smoothedPosition = hit.point - offset.normalized * 0.5f; // 0.5f is a small buffer to avoid clipping through the wall
        }

        // Update the position of the camera with the offset applied
        //transform.position = smoothedPosition - (isRotating ? transform.forward * offset.z : Vector3.zero);
        transform.position = smoothedPosition;

        // Calculate the desired look-at position with an upward offset
        Vector3 lookAtPosition = medianPosition + Vector3.up * lookUpOffset;

        // Calculate the desired Y rotation
        float newYRotation = isRotating ? currentYRotation : transform.eulerAngles.y;

        // Make the camera look at the adjusted median position with the new Y rotation
        Quaternion lookRotation = Quaternion.LookRotation(lookAtPosition - transform.position);
        //transform.rotation = Quaternion.Euler(lookRotation.eulerAngles.x, newYRotation, lookRotation.eulerAngles.z);

        transform.rotation = lookRotation;
    }
}