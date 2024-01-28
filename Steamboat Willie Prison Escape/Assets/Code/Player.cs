using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private Vector3 lastCheckpointPosition;
    public bool isThrown = false;
    private void Start()
    {
        // Initialize the last checkpoint position to the player's starting position
        lastCheckpointPosition = transform.position;
    }

    public void SetLastCheckpoint(Vector3 newCheckpointPosition)
    {
        lastCheckpointPosition = newCheckpointPosition;
    }

    public void RespawnAtLastCheckpoint()
    {
        transform.position = lastCheckpointPosition;
        // Add any additional respawn logic here (like resetting velocity)
    }

    // Call this method when the player is hit by an object
    public void HitByObject()
    {
        RespawnAtLastCheckpoint();
    }
    public IEnumerator ResetThrownStatusAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isThrown = false;
    }
}
