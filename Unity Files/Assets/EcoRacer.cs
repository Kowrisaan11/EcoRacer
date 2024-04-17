using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;


 public class AuthenticationManager : MonoBehaviour
{
    string url = "http://20.15.114.131:8080/api/login";
    string profileUrl = "http://20.15.114.131:8080/api/user/profile/view";
    private string apiUrl = "http://20.15.114.131:8080/api/user/profile/update";
    
    string jwtToken;

    // public InputField firstnameInput;
    // public InputField lastnameInput;
    // public InputField nicInput;
    // public InputField phoneNumberInput;
    // public InputField emailInput;



     IEnumerator AuthenticateAndRetrieveProfile(string apiKey)
    {
        // Authentication process to obtain JWT (replace this with your authentication logic)
        yield return StartCoroutine(Authenticate(apiKey));

        // Wait for the JWT token to be obtained
        while (string.IsNullOrEmpty(jwtToken))
        {
            yield return null;
        }

        // Request user profile with JWT
        yield return StartCoroutine(ViewUserProfile());
    }

    IEnumerator Authenticate(string apiKey)
    {
                // Create JSON data with the apiKey
        string jsonData = "{\"apiKey\": \"" + apiKey + "\"}";

        // Create a UnityWebRequest object
        UnityWebRequest request = new UnityWebRequest(url, "POST");

        // Set headers
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("accept", "*/*");

        // Convert JSON data to byte array and set it as request body
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        // Send the request
        yield return request.SendWebRequest();

        // Check for errors
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            // Handle the response data
            jwtToken=request.downloadHandler.text;
            Debug.Log("Response: " + jwtToken);
        }
    }


    IEnumerator ViewUserProfile()
    {
        // Create a UnityWebRequest object for fetching user profile
        //UnityWebRequest profileRequest = UnityWebRequest.Get(profileUrl);
        UnityWebRequest profileRequest = new UnityWebRequest(profileUrl, "GET");

        // // Set authorization header with the JWT token
        profileRequest.SetRequestHeader("Authorization", "Bearer " + jwtToken);
        profileRequest.SetRequestHeader("accept", "*application/json*");

        // Send the profile request
        yield return profileRequest.SendWebRequest();
        // Check for errors
        if (profileRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Profile view Error: " + profileRequest.result);
            Debug.LogError("Profile view Error: " + profileRequest.error);
        }
        else
        {
            // Get and parse the user profile data from the response
            string userProfileJson = profileRequest.downloadHandler.text;
            Debug.Log("User Profile: " + userProfileJson);
        }
    }

    public void UpdateProfile(string firstname, string lastname, string nic, string phoneNumber, string email)
    {
        // string firstname = firstnameInput.text;
        // string lastname = lastnameInput.text;
        // string nic = nicInput.text;
        // string phoneNumber = phoneNumberInput.text;
        // string email = emailInput.text;

        // Check if any field is empty
        if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(nic) ||
            string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(email))
        {
            Debug.Log("Please fill in all fields!");
            return ;
        }
        
        StartCoroutine(SendProfileUpdateRequest(firstname, lastname, nic, phoneNumber, email));
    }

    IEnumerator SendProfileUpdateRequest(string firstname, string lastname, string nic, string phoneNumber, string email)
    {
        // Create JSON object for request data
        Dictionary<string, string> requestData = new Dictionary<string, string>();
        requestData.Add("firstname", firstname);
        requestData.Add("lastname", lastname);
        requestData.Add("nic", nic);
        requestData.Add("phoneNumber", phoneNumber);
        requestData.Add("email", email);

        // Convert request data to JSON string
        string jsonData = JsonUtility.ToJson(requestData);

        // Create UnityWebRequest
        UnityWebRequest request = new UnityWebRequest(apiUrl, "PUT");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        // Set request headers
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + jwtToken);

        // Send the request
        yield return request.SendWebRequest();

        // Check for errors
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error sending update request: " + request.error);
        }
        else
        {
            // Request successful
            Debug.Log("Profile updated successfully!");
        }
    }

    void Start()
    {
        string api = "NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmNmOjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJjNQ";
        StartCoroutine(AuthenticateAndRetrieveProfile(api));
        UpdateProfile("SJ","Jathu","200027700569","0762756363","s.jathurshan03@gmail.com");
    }
}






// using UnityEngine;
// using UnityEngine.Networking;
// using System.Collections;

// public class APIClient : MonoBehaviour
// {
//     IEnumerator Authenticate(string apiKey)
//     {
//         // Define your authentication endpoint URL
//         string url = "http://20.15.114.131:8080/api/login";

//         // Create JSON data with the apiKey
//         string jsonData = "{\"apiKey\": \"" + apiKey + "\"}";

//         // Create a UnityWebRequest object
//         UnityWebRequest request = new UnityWebRequest(url, "POST");

//         // Set headers
//         request.SetRequestHeader("Content-Type", "application/json");
//         request.SetRequestHeader("accept", "*/*");

//         // Convert JSON data to byte array and set it as request body
//         byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
//         request.uploadHandler = new UploadHandlerRaw(bodyRaw);
//         request.downloadHandler = new DownloadHandlerBuffer();

//         // Send the request
//         yield return request.SendWebRequest();

//         // Check for errors
//         if (request.result != UnityWebRequest.Result.Success)
//         {
//             Debug.LogError("Error: " + request.error);
//         }
//         else
//         {
//             // Handle the response data
//             Debug.Log("Response: " + request.downloadHandler.text);
//         }
//     }

//     void Start()
//     {
//         Debug.Log("APIClient.Start()");

//         // Replace with your API key
//         string api = "NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmNmOjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJjNQ";
//         StartCoroutine(Authenticate(api));
//     }
// }
