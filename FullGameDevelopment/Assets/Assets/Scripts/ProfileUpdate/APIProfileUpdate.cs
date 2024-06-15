using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using SimpleJSON;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System;

public class APIProfileUpdate : MonoBehaviour
{
    public APIAuthentication APIAuthentication;

    public TMP_InputField firstnameInput;
    public TMP_InputField lastnameInput;
    public TMP_InputField nicInput;
    public TMP_InputField phoneNumberInput;
    public TMP_InputField emailInput;

    public TextMeshProUGUI firstnameWarning;
    public TextMeshProUGUI lastnameWarning;
    public TextMeshProUGUI nicWarning;
    public TextMeshProUGUI phoneNumberWarning;
    public TextMeshProUGUI emailWarning;

    public Button UpdateProfileButton;

    private void Start()
    {
        // Initialize the button state
        UpdateProfileButton.interactable = false;

        // Add listeners for input fields to validate on change
        firstnameInput.onValueChanged.AddListener(delegate { OnInputValueChanged(); });
        lastnameInput.onValueChanged.AddListener(delegate { OnInputValueChanged(); });
        nicInput.onValueChanged.AddListener(delegate { OnInputValueChanged(); });
        phoneNumberInput.onValueChanged.AddListener(delegate { OnInputValueChanged(); });
        emailInput.onValueChanged.AddListener(delegate { OnInputValueChanged(); });
    }

    private void OnInputValueChanged()
    {
        ValidateAllInputs();
    }

    public void OnUpdateProfileButtonClick()
    {
        if (ValidateAllInputs())
        {
            StartCoroutine(UpdateProfile());
        }
    }

    public IEnumerator UpdateProfile()
    {
        string updateUrl = "http://20.15.114.131:8080/api/user/profile/update";
        string jsonData = $"{{\"firstname\": \"{firstnameInput.text}\", \"lastname\": \"{lastnameInput.text}\", \"nic\": \"{nicInput.text}\", \"phoneNumber\": \"{phoneNumberInput.text}\", \"email\": \"{emailInput.text}\"}}";

        UnityWebRequest request = new UnityWebRequest(updateUrl, "PUT");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + APIAuthentication.GetToken());

        Debug.Log("Token: " + APIAuthentication.GetToken());
        Debug.Log("Sending request to: " + updateUrl);
        Debug.Log("Request body: " + jsonData);
        Debug.Log("Authorization header: " + request.GetRequestHeader("Authorization"));

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error updating profile: " + request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            var json = JSON.Parse(jsonResponse);
            Debug.Log("Updated Profile Info: " + json.ToString());
        }
    }

    public bool ValidateAllInputs()
    {
        bool isValid = true;
        isValid &= ValidateFirstname();
        isValid &= ValidateLastname();
        isValid &= ValidateNic();
        isValid &= ValidatePhoneNumber();
        isValid &= ValidateEmail();

        UpdateProfileButton.interactable = isValid;
        return isValid;
    }

    private bool ValidateFirstname()
    {
        if (string.IsNullOrWhiteSpace(firstnameInput.text))
        {
            firstnameWarning.text = "Firstname cannot be empty.";
            firstnameWarning.color = Color.red;
            return false;
        }
        else
        {
            firstnameWarning.text = "";
            return true;
        }
    }

    private bool ValidateLastname()
    {
        if (string.IsNullOrWhiteSpace(lastnameInput.text))
        {
            lastnameWarning.text = "Lastname cannot be empty.";
            lastnameWarning.color = Color.red;
            return false;
        }
        else
        {
            lastnameWarning.text = "";
            return true;
        }
    }

    private bool ValidateNic()
    {
        string nicRegex1 = @"^[0-9]{9}[Vv]$";
        string nicRegex2 = @"^\d{12}$";

        if (!Regex.IsMatch(nicInput.text, nicRegex1) && !Regex.IsMatch(nicInput.text, nicRegex2))
        {
            nicWarning.text = "NIC number should contain either 9 digits followed by 'v' or 'V' at the end, or 12 digits.";
            nicWarning.color = Color.red;
            return false;
        }
        else
        {
            nicWarning.text = "";
            return true;
        }
    }

    private bool ValidatePhoneNumber()
    {
        string phoneNumberRegex = @"^\d{10}$";

        if (!Regex.IsMatch(phoneNumberInput.text, phoneNumberRegex))
        {
            phoneNumberWarning.text = "Phone number should contain 10 digits.";
            phoneNumberWarning.color = Color.red;
            return false;
        }
        else
        {
            phoneNumberWarning.text = "";
            return true;
        }
    }

    private bool ValidateEmail()
    {
        string emailRegex = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

        if (!Regex.IsMatch(emailInput.text, emailRegex))
        {
            emailWarning.text = "Invalid email format.";
            emailWarning.color = Color.red;
            return false;
        }
        else
        {
            emailWarning.text = "";
            return true;
        }
    }
}
