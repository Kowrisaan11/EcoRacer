using UnityEngine;

public class TreeGrowth : MonoBehaviour
{
    public float growthSpeed = 0.3f;  // Growth speed factor
    public Vector3 maxScale = new Vector3(1, 1, 1);  // Maximum scale of the tree
    private float elapsedTime = 0f;  // Track the elapsed time

    private void Start()
    {
        if (APIClient.Instance == null)
        {
            Debug.LogError("APIClient instance not found!");
        }
    }

    void Update()
    {
        if (APIClient.Instance != null)
        {
            float point = APIClient.Instance.GetCurrentConsumption()/4;

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate the new scale factor based on points and elapsed time
            float timeFactor = Mathf.Clamp01(elapsedTime / 120f);  // Normalized time factor (0 to 1 over 2 minutes)
            float scaleFactor = Mathf.Clamp(point * growthSpeed * timeFactor, 0.1f, maxScale.x);  // Scale factor based on score and time

            // Update the tree's scale based on the calculated scale factor
            Vector3 targetScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * growthSpeed);  // Smooth transition

            // Debug the current points and scale
            Debug.Log($"Tree Scale: {transform.localScale}, Points: {point}, Elapsed Time: {elapsedTime}");
        }
    }
}
