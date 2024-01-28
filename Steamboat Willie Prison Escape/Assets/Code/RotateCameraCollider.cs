using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraCollider : MonoBehaviour
{
    public float newYRotation = 0f; // Set the desired Y rotation when entering this trigger
    public float rotationSpeed = 2.0f; // Speed of the rotation
    public bool activateRotation = false; // Set this to true if you want to activate rotation

    private int playersInsideCollider = 0; // Counter for the number of players inside the collider
    private Camera mainCamera;
    private Quaternion targetRotation;

    private void Start()
    {
        mainCamera = Camera.main; // Change this to your actual camera reference
        targetRotation = mainCamera.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player (you can add tags or layers to differentiate)
        if (other.CompareTag("Player"))
        {
            playersInsideCollider++;
            if (playersInsideCollider >= 2)
            {
                activateRotation = true;
                targetRotation = Quaternion.Euler(mainCamera.transform.eulerAngles.x, newYRotation, mainCamera.transform.eulerAngles.z);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the colliding object is the player (you can add tags or layers to differentiate)
        if (other.CompareTag("Player"))
        {
            playersInsideCollider--;
            if (playersInsideCollider < 2)
            {
                activateRotation = false;
            }
        }
    }

    private void Update()
    {
        if (activateRotation)
        {
            // Smoothly rotate the camera towards the target rotation
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
