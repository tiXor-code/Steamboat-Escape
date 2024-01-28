using UnityEngine;

public class HazardObject : MonoBehaviour
{
    public string groundTag = "Ground"; // Tag of the ground object
    public Transform player1;
    public Transform player2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit by object");
            Player player1Script = player1.GetComponent<Player>();
            Player player2Script = player2.GetComponent<Player>();
            if (player1Script != null || player2Script != null)
            {
                player1Script.HitByObject();
                player2Script.HitByObject();
            }
            Destroy(gameObject); // Destroy the hazard object after hitting the player
        }
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject); // Destroy the hazard object after hitting the player
        }
    }////ground colliding->destroy
}
