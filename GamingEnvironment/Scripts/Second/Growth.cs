// using UnityEngine;

// public class TreeGrowth : MonoBehaviour
// {
//     public float growthSpeed = 0.00000001f;  // Base growth speed
//     public Vector3 maxScale = new Vector3(1 / 10, 1 / 10, 1 / 10);  // Maximum scale of the tree
//     public int points = 2;  // Current game score

//     void Update()
//     {
//         // Calculate the new scale factor based on points
//         float scaleFactor = Mathf.Clamp(points * growthSpeed, 0.00000001f, maxScale.x);  // Ensure the scale factor stays within a reasonable range

//         // Update the tree's scale based on the calculated scale factor
//         Vector3 targetScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
//         transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime);

//         Debug.Log($"Tree Scale: {transform.localScale}, Points: {points}");
//     }
// }

using UnityEngine;

public class TreeGrowth : MonoBehaviour
{
    public float growthSpeed = 0.5f;  // Growth speed factor
    public Vector3 maxScale = new Vector3(1, 1, 1);  // Maximum scale of the tree
    public int points = 2;  // Current game score
    private float elapsedTime = 0f;  // Track the elapsed time

    void Update()
    {
        // Increment the elapsed time
        elapsedTime += Time.deltaTime;

        // Calculate the new scale factor based on points and elapsed time
        float timeFactor = Mathf.Clamp01(elapsedTime / 120f);  // Normalized time factor (0 to 1 over 2 minutes)
        float scaleFactor = Mathf.Clamp(points * growthSpeed * timeFactor, 0.1f, maxScale.x);  // Scale factor based on score and time

        // Update the tree's scale based on the calculated scale factor
        Vector3 targetScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * growthSpeed);  // Smooth transition

        //Debug.Log($"Tree Scale: {transform.localScale}, Points: {points}, Elapsed Time: {elapsedTime}");
    }
}

