using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour
{
    public Image[] images; // Assign your images in the inspector
    public float displayTime = 2f; // Time each image is displayed before the next one appears

    void Start()
    {
        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        foreach (Image img in images)
        {
            img.gameObject.SetActive(true); // Show the current image
            float startTime = Time.time;

            // Wait for displayTime to pass or key press to skip the cutscene
            while (Time.time - startTime < displayTime)
            {
                if (Input.anyKeyDown)
                {
                    // If any key is pressed, end the cutscene early
                    EndCutscene();
                    yield break; // Exit the coroutine
                }
                yield return null; // Wait until the next frame
            }
        }

        // Optional: add a small delay to prevent instant skipping if holding down a key
        yield return new WaitForSeconds(0.2f);

        // Wait for any key press to end the cutscene
        yield return new WaitUntil(() => Input.anyKeyDown);
        EndCutscene();
    }

    void EndCutscene()
    {
        foreach (Image img in images)
        {
            img.gameObject.SetActive(false); // Hide the image
        }
        // Optional: Perform any actions after the cutscene ends
    }
}
