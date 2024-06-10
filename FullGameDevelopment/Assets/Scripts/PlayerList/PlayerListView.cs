using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Linq; // To use LINQ for sorting

public class PlayerListView : MonoBehaviour
{
    public Transform playerListContainer;
    public GameObject playerListItemPrefab;

    void Start()
    {
        StartCoroutine(FetchPlayerDetails());
    }

    IEnumerator FetchPlayerDetails()
    {
        string url = "http://localhost/sqlconnect/getplayerdetails.php";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            string jsonResult = www.downloadHandler.text;
            Debug.Log("JSON Result: " + jsonResult);

            PlayerDetailsList playerDetailsList = JsonUtility.FromJson<PlayerDetailsList>(jsonResult);

            // Sort the list based on score in descending order
            List<PlayerDetails> sortedPlayerList = playerDetailsList.players.OrderByDescending(player => player.score).ToList();

            // Instantiate the sorted list
            foreach (PlayerDetails player in sortedPlayerList)
            {
                GameObject listItem = Instantiate(playerListItemPrefab, playerListContainer);

                TextMeshProUGUI nameText = listItem.transform.Find("PlayerName").GetComponent<TextMeshProUGUI>();
                nameText.text = player.username;
                nameText.fontSize = 20;
                nameText.color = new Color(0f, 0f, 0.545f); // Light dark color

                TextMeshProUGUI scoreText = listItem.transform.Find("PlayerScore").GetComponent<TextMeshProUGUI>();
                scoreText.text = player.score.ToString();
                scoreText.fontSize = 20;
                scoreText.color = new Color(0f, 0f, 0.545f); // Light dark color
            }
        }
    }
}

[System.Serializable]
public class PlayerDetails
{
    public string username;
    public int score;
}

[System.Serializable]
public class PlayerDetailsList
{
    public List<PlayerDetails> players;
}
