using UnityEngine;

public class GuardDetection : MonoBehaviour
{
    public float detectionRange = 10f;
    public float detectionAngle = 30f;
    public float detectionInterval = 0.5f;
    public float rayHeightOffset = 0.5f;
    public GameObject objectToInstantiate;
    public float instantiateHeight = 3f;
    public float throwInterval = 2f; // Minimum time between throws

    private float detectionTimer;
    private float throwTimer = 0f; // Time since the last throw
    public LayerMask ignoreLayer;

    void Update()
    {
        detectionTimer += Time.deltaTime;
        throwTimer += Time.deltaTime; // Increment the throw timer

        if (detectionTimer >= detectionInterval)
        {
            PerformDetection();
            detectionTimer = 0;
        }
    }

    void PerformDetection()
    {
        for (float angle = -detectionAngle; angle <= detectionAngle; angle += detectionAngle / 6)
        {
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            Vector3 direction = rotation * transform.forward;
            Vector3 rayStartPosition = transform.position + Vector3.up * rayHeightOffset;

            RaycastHit hit;
            if (Physics.Raycast(rayStartPosition, direction, out hit, detectionRange, ~ignoreLayer))
            {
                if (hit.collider.CompareTag("Player") && throwTimer >= throwInterval)
                {
                    Debug.Log("Player detected at angle: " + angle);

                    Vector3 instantiatePosition = hit.point + Vector3.up * instantiateHeight;
                    Instantiate(objectToInstantiate, instantiatePosition, Quaternion.identity);

                    throwTimer = 0f; // Reset the throw timer after a throw
                }
            }
            Debug.DrawRay(rayStartPosition, direction * detectionRange, Color.red, detectionInterval);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the guard collides with a player
        if (other.CompareTag("Player"))
        {
            Player throwPlayer = other.GetComponent<Player>();

            // Check if the colliding player is currently being thrown
            if (throwPlayer != null && throwPlayer.isThrown)
            {
                // Destroy the guard GameObject
                Destroy(gameObject);
            }
        }
    }
}
