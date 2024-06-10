using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScoreSave : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("GameScoreSave script started.");
        DBUpdateScore();
    }

    public void CallSaveData()
    {
        StartCoroutine(SavePlayerData());
    }

    private IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.username);
        form.AddField("score", DBManager.score);

        Debug.Log("Saving data - Username: " + DBManager.username + ", Score: " + DBManager.score);

        WWW www = new WWW("http://localhost/sqlconnect/savedata.php", form);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("Score Data Saved.");
        }
        else
        {
            Debug.Log("Save failed. Error #" + www.text);
        }

        DBManager.LogOut();
        SceneManager.LoadScene(9);
    }

    public void DBUpdateScore()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        Debug.Log("Final Score from PlayerPrefs: " + finalScore);
        DBManager.score = finalScore;
    }
}
