using UnityEngine;
using TMPro;

public class EndSceneController : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        // Get the score value from PlayerPrefs
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);

        // Display the score value
        finalScoreText.text = "Final Score: " + finalScore;
    }
}
