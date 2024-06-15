using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using SimpleJSON;

public class ScoreManagement : MonoBehaviour
{
    public TextMeshProUGUI scoreValue;
    private int score;

    private void Start()
    {
        StartCoroutine(FetchData());
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

    private IEnumerator FetchData()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://backends-j6al.onrender.com/user/getLast");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error fetching data: " + request.error);
        }
        else
        {
            Debug.Log("Data fetched successfully: " + request.downloadHandler.text);
            
            string jsonResponse = request.downloadHandler.text;
            var json = JSON.Parse(jsonResponse);

            if (json != null && json["score"] != null)
            {
                score = json["score"].AsInt;
                Debug.Log("Score updated to: " + score);
                UpdateScoreText();
            }
            else
            {
                Debug.LogWarning("JSON response does not contain 'score'");
            }

        }
    
    }
}
