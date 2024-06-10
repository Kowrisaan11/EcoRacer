using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using SimpleJSON;
using System.Collections;


public class APIProfileView : MonoBehaviour
{
    public APIAuthentication APIAuthentication;

    public TextMeshProUGUI firstnameText;
    public TextMeshProUGUI lastnameText;
    public TextMeshProUGUI usernameText;
    public TextMeshProUGUI nicText;
    public TextMeshProUGUI phoneNumberText;
    public TextMeshProUGUI emailText;

    public void OnViewProfileButtonClick()
    {
        StartCoroutine(ViewProfile());
    }

    private IEnumerator ViewProfile()
    {
        string profileUrl = "http://20.15.114.131:8080/api/user/profile/view";
        UnityWebRequest request = UnityWebRequest.Get(profileUrl);
        request.SetRequestHeader("Authorization", "Bearer " + APIAuthentication.GetToken());

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error viewing profile: " + request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            var json = JSON.Parse(jsonResponse);

            // Display profile information in the specified format
            firstnameText.text = "FirstName: " + json["user"]["firstname"];
            lastnameText.text = "LastName: " + json["user"]["lastname"];
            usernameText.text = "Username: " + json["user"]["username"];
            nicText.text = "NIC: " + json["user"]["nic"];
            phoneNumberText.text = "PhoneNumber: " + json["user"]["phoneNumber"];
            emailText.text = "Email: " + json["user"]["email"];

            Debug.Log("Profile Info: " + json.ToString());
        }
    }
}
