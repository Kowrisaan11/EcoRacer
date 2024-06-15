using UnityEngine;

public class StaticBird : MonoBehaviour
{
    public GameObject bird; // Reference to the bird GameObject
    private Animator birdAnimator;

    void Start()
    {
        birdAnimator = bird.GetComponent<Animator>();
        bird.SetActive(true); // Initially hide the bird


    }

}


