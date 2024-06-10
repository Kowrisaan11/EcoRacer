using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreValue;
    private int score = 0;

    private void Start()
    {
        UpdateScoreText();
    }

    public void IncrementScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    public void DecrementScore(int amount)
    {
        score -= amount;
        UpdateScoreText();
    }

    public int  GetScore()
    {
        return score;
    }

    private void UpdateScoreText()
    {
        scoreValue.text = "Score: " + score.ToString();
    }
}
