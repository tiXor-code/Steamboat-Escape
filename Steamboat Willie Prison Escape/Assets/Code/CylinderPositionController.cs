using UnityEngine;

public class CylinderPositionController : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float cylinderRadius = 3f; // Set this based on the size of your cylinder

    void Update()
    {
        
            // Calculate the median position
            Vector3 medianPosition = (player1.position + player2.position) / 2;

            // Set only the x and z components of the cylinder's position
            Vector3 newPosition = new Vector3(medianPosition.x, transform.position.y, medianPosition.z);
            transform.position = newPosition;
    }
}
