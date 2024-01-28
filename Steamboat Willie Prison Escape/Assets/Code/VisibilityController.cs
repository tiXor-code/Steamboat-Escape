using UnityEngine;

public class VisibilityController : MonoBehaviour
{
    public Camera mainCamera;
    public Transform player1;
    public Transform player2;
    public Material visibilityMaterial; // The material with the visibility shader

    private int wallLayer; // Layer mask for walls
    private Renderer visibilityRenderer;
    private bool isVisibilityEnabled = false;

    void Start()
    {
        // Define the wall layer (make sure your walls are on this layer)
        wallLayer = LayerMask.NameToLayer("Walls"); // Corrected layer name

        // Get the renderer component of the object with the visibility shader
        visibilityRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Cast rays from the camera towards the players and visualize them in the Scene view
        Debug.DrawRay(mainCamera.transform.position, player1.position - mainCamera.transform.position, Color.red);
        Debug.DrawRay(mainCamera.transform.position, player2.position - mainCamera.transform.position, Color.blue);

        // Calculate the direction from the camera to the players
        Vector3 directionToPlayer1 = player1.position - mainCamera.transform.position;
        Vector3 directionToPlayer2 = player2.position - mainCamera.transform.position;

        // Check if the rays hit objects on the wall layer
        bool hitWall1 = Physics.Raycast(mainCamera.transform.position, directionToPlayer1, out RaycastHit hit1, Mathf.Infinity, 1 << wallLayer);
        bool hitWall2 = Physics.Raycast(mainCamera.transform.position, directionToPlayer2, out RaycastHit hit2, Mathf.Infinity, 1 << wallLayer);

        // Activate the visibility shader if either ray hits a wall
        isVisibilityEnabled = hitWall1 || hitWall2;

        // Update the visibility shader material
        UpdateVisibilityMaterial();
    }

    void UpdateVisibilityMaterial()
    {
        if (visibilityMaterial != null && visibilityRenderer != null)
        {
            // Set the shader's visibility property based on whether visibility is enabled
            visibilityMaterial.SetFloat("_VisibilityEnabled", isVisibilityEnabled ? 1.0f : 0.0f);

            // Apply the material to the renderer
            visibilityRenderer.material = visibilityMaterial;

            // Debug message indicating when the shader is applied
            if (isVisibilityEnabled)
            {
                Debug.Log("Visibility Shader Applied!");
            }
        }
    }
}
