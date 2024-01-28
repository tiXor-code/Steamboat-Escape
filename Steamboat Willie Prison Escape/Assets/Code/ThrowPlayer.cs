using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPlayer : MonoBehaviour
{
    public Transform cylinder; // Reference to the cylinder's transform
    public float throwForce = 10f; // Adjust as needed
    private Transform otherPlayer; // Reference to the other player's transform (make it private if it's only set by this script)

    void Update()
    {
        // Check for the throw action (e.g., pressing a key)
        if (Input.GetKeyDown(KeyCode.T) && otherPlayer != null) // 'T' is an example key
        {
            ThrowPlayerFunction();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the "Player" tag and the name "Minnie"
        if (other.gameObject.name == "Minnie")
        {
            otherPlayer = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger has the "Player" tag
        if (other.gameObject.name == "Minnie")
        {
            otherPlayer = null;
        }
    }

    void ThrowPlayerFunction()
    {
        // Calculate direction towards the enemy or a specific point
        Vector3 throwDirection = (otherPlayer.position - transform.position).normalized;

        // Optional: Adjust throwDirection to ensure the player stays within the cylinder

        // Apply force to the other player (consider using Rigidbody for physics-based movement)
        Rigidbody otherPlayerRb = otherPlayer.GetComponent<Rigidbody>();
        if (otherPlayerRb != null)
        {
            otherPlayerRb.AddForce(throwDirection * throwForce, ForceMode.Impulse);

            // Set isThrown to true on the other player
            Player otherPlayerScript = otherPlayer.GetComponent<Player>();
            if (otherPlayerScript != null)
            {
                otherPlayerScript.isThrown = true;
            }
            StartCoroutine(otherPlayerScript.ResetThrownStatusAfterDelay(0.5f));
        }
    }
    private IEnumerator ResetThrownStatusAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Player otherPlayerScript = otherPlayer.GetComponent<Player>();
        otherPlayerScript.isThrown = false;
    }
}
