using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using SimpleJSON;

public class CurrentConsumption : MonoBehaviour
{
    public APIAuthentication APIAuthentication;
    public TextMeshProUGUI currentconsumptionvalue;

    private float currentConsumption;

    private void Start()
    {
        StartCoroutine(FetchData());
    }

    IEnumerator FetchData()
    {
        while (true)
        {
            string token = APIAuthentication.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                UnityWebRequest request = UnityWebRequest.Get("http://20.15.114.131:8080/api/power-consumption/current/view");
                request.SetRequestHeader("Authorization", "Bearer " + token);

                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Error fetching data: " + request.error);
                }
                else
                {
                    string jsonResponse = request.downloadHandler.text;
                    var json = JSON.Parse(jsonResponse);
                    currentConsumption = json["currentConsumption"].AsFloat;
                    currentconsumptionvalue.text = "Current Consumption: " + currentConsumption;
                }
            }
            else
            {
                Debug.LogWarning("Token not available. Waiting for authentication...");
            }

            yield return new WaitForSeconds(10f); // Fetch data every 10 seconds
        }
    }

    public float GetCurrentConsumption()
    {
        return currentConsumption;
    }
}
