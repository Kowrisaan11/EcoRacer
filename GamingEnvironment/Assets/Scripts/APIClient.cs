using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using SimpleJSON;

public class APIClient : MonoBehaviour
{
    public TextMeshProUGUI dataText;
    public ScoreManager scoreManager;

    private float currentConsumption;

    private void Start()
    {
        StartCoroutine(Authenticate("NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmNmOjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJjNQ"));
    }

    IEnumerator Authenticate(string apiKey)
    {
        string url = "http://20.15.114.131:8080/api/login";
        string jsonData = "{\"apiKey\": \"" + apiKey + "\"}";

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Disable SSL certificate validation for testing (not recommended for production)
        request.certificateHandler = new BypassCertificate();

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            var json = JSON.Parse(jsonResponse);
            string token = json["token"];

            // Start fetching data
            StartCoroutine(FetchData(token));
        }
    }

    IEnumerator FetchData(string token)
    {
        string dataUrl = "http://20.15.114.131:8080/api/power-consumption/current/view";

        while (true)
        {
            UnityWebRequest request = UnityWebRequest.Get(dataUrl);
            request.SetRequestHeader("Authorization", "Bearer " + token);

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error fetching data: " + request.error);
                if (request.responseCode == 401) // Unauthorized
                {
                    StartCoroutine(Authenticate("NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmNmOjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJjNQ"));
                }
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                var json = JSON.Parse(jsonResponse);
                currentConsumption = json["currentConsumption"].AsFloat;
                dataText.text = "Current Consumption: " + currentConsumption;
            }

            yield return new WaitForSeconds(10f); // Fetch data every 10 seconds
        }
    }

    public class BypassCertificate : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }

    public float GetCurrentConsumption()
    {

        Debug.Log("GetCurrentConsumption called, returning: " + currentConsumption);
        return currentConsumption;
    }
}
