using UnityEngine;

public class BirdController : MonoBehaviour
{
    public GameObject bird; // Reference to the bird GameObject
    private Animator birdAnimator;

    void Start()
    {
        birdAnimator = bird.GetComponent<Animator>();
        bird.SetActive(false); // Initially hide the bird
        if (APIClient.Instance == null)
        {
            Debug.LogError("APIClient instance not found!");
        }
        // if (score > 20)
        // {
        //     bird.SetActive(true);

        // }

    }

    void Update()
    {
        float score = APIClient.Instance.GetCurrentConsumption()/100000;
        if (score > 0.5f)
        {
            bird.SetActive(true);
            //ShowBird();

        }
    }

    void ShowBird()
    {
        if (!bird.activeInHierarchy)
        {
            bird.SetActive(true); // Show the bird
            birdAnimator.Play("BirdFlyingAnimation"); // Play the bird's flying animation
        }
    }
}


