using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public Button playButton;
    public TextMeshProUGUI countdownText;
    public float countdownTime = 3f;
    public AudioSource beepSound;

    private bool countdownStarted = false;

    private void Start()
    {
        playButton.onClick.AddListener(StartCountdown);
        beepSound.Stop(); // Stop the beep sound initially


    }

    private void StartCountdown()
    {
        playButton.gameObject.SetActive(false);
        countdownStarted = true; // Set countdownStarted to true when the button is clicked
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        float timer = countdownTime;

        while (timer > 0 && countdownStarted)
        {
            countdownText.text = Mathf.CeilToInt(timer).ToString();
            beepSound.Play(); // Play beep sound
            yield return new WaitForSeconds(1f);
            timer--;
        }

            // Transition to the  main scene after the countdown ends
            SceneManager.LoadScene("GameInstructions");

    }
}
