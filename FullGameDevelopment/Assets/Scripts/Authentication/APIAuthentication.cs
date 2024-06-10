using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using SimpleJSON;

public class APIAuthentication : MonoBehaviour
{
    public static string token; 
    private string apiKey = "NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmNmOjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJjNQ";
    private void Start()
    {
        StartCoroutine(Authenticate(apiKey));
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
            token = json["token"];
            Debug.Log("Authenticated, token: " + token);
        }
    }

    public string GetToken()
    {
        Debug.Log("GetToken called, returning: " + token);
        return token;
    }


    public class BypassCertificate : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
}

