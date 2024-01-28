using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTrigger : MonoBehaviour
{
    public string endGameSceneName = "EndGame"; // Name of your end game scene

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Load the end game scene
            SceneManager.LoadScene(endGameSceneName);
        }
    }
}
