using System.Collections;
using UnityEngine;

public class ChangeHouseColor : MonoBehaviour
{
    public Color originalColor = Color.gray; // Original color
    public Color redColor = Color.red;
    public Color blueColor = Color.blue;
    public Color greenColor = Color.green;

    public float scoreThreshold = 0.5f; // Threshold to switch colors
    public float colorChangeDuration = 1.0f; // Duration for each color

    public float score;
    new Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();

        // Check if a renderer is attached
        if (renderer == null)
        {
            Debug.LogError("No Renderer attached to the GameObject.");
            return;
        }
        if (APIClient.Instance == null)
        {
            Debug.LogError("APIClient instance not found!");
        }

        // Create a new material to ensure this script doesn't change other objects' colors
        renderer.material = new Material(renderer.sharedMaterial);

        // Start color changing coroutine
        StartCoroutine(ChangeColorRoutine());
    }

    IEnumerator ChangeColorRoutine()
    {
        while (true)
        {
            // Change to red for colorChangeDuration seconds
            renderer.material.color = redColor;
            yield return new WaitForSeconds(colorChangeDuration);

            // Change to blue for colorChangeDuration seconds
            renderer.material.color = blueColor;
            yield return new WaitForSeconds(colorChangeDuration);
            renderer.material.color = greenColor;
            yield return new WaitForSeconds(colorChangeDuration);
        }
    }

    void Update()
    {
        if (APIClient.Instance != null)
        {
            score = APIClient.Instance.GetCurrentConsumption()/100000;

        }
        // Change the color based on the score
        if (score < scoreThreshold)
        {
            renderer.material.color = originalColor;
        }
    }
}
