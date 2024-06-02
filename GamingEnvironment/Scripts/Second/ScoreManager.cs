using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI textField;
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

    public int GetScore()
    {
        return score;
    }

    private void UpdateScoreText()
    {
        textField.text = "Score: " + score.ToString();
    }
}
