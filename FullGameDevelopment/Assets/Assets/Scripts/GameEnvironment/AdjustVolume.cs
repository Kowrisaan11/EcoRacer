using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    public AudioSource backgroundMusic; // Reference to the AudioSource component
    public float score; // The current score
    public float volumeChangeRate = 0.1f; // Rate at which volume changes
    private float targetVolume; // Target volume to reach

    void Start()
    {
        if (backgroundMusic == null)
        {
            backgroundMusic = GetComponent<AudioSource>();
        }


        if (APIClient.Instance == null)
        {
            Debug.LogError("APIClient instance not found!");
        }
        targetVolume = backgroundMusic.volume; // Initialize target volume
    }


    void Update()
    {

        if (APIClient.Instance != null)
        {
            score = APIClient.Instance.GetPowerDifference();
            // Adjust the target volume based on the score
            AdjustVolumeBasedOnScore();

            // Smoothly transition the volume to the target volume
            backgroundMusic.volume = Mathf.Lerp(backgroundMusic.volume, targetVolume, Time.deltaTime * volumeChangeRate);

        }

    }

    void AdjustVolumeBasedOnScore()
    {
        // Decrease volume to 40% if score is less than 20, otherwise set it to 100%
        if (score < 0.5)
        {
            targetVolume = 0.0001f; // Set volume to 40%
        }
        else
        {
            targetVolume = 1.0f; // Set volume to 100%
            // backgroundMusic.clip = sfx;
            // backgroundMusic.Play();
        }
    }
}





