using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using SimpleJSON;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System;

public class APIProfileRegister : MonoBehaviour
{
    public APIAuthentication APIAuthentication;

    public TMP_InputField firstnameInput;
    public TMP_InputField lastnameInput;
    public TMP_InputField nicInput;
    public TMP_InputField phoneNumberInput;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField usernameInput;

    public TextMeshProUGUI firstnameWarning;
    public TextMeshProUGUI lastnameWarning;
    public TextMeshProUGUI nicWarning;
    public TextMeshProUGUI phoneNumberWarning;
    public TextMeshProUGUI emailWarning;
    public TextMeshProUGUI passwordWarning;
    public TextMeshProUGUI usernameWarning;

    public TextMeshProUGUI registrationErrorMessage;  // New element for displaying registration errors

    public Button UpdateProfileButton;

    private void Start()
    {
        passwordInput.contentType = TMP_InputField.ContentType.Password;
        passwordInput.asteriskChar = '*';
        passwordInput.ForceLabelUpdate();

        // Initialize the button state
        UpdateProfileButton.interactable = false;

        // Add listeners for input fields to validate on change
        firstnameInput.onValueChanged.AddListener(delegate { OnInputValueChanged(); });
        lastnameInput.onValueChanged.AddListener(delegate { OnInputValueChanged(); });
        nicInput.onValueChanged.AddListener(delegate { OnInputValueChanged(); });
        phoneNumberInput.onValueChanged.AddListener(delegate { OnInputValueChanged(); });
        emailInput.onValueChanged.AddListener(delegate { OnInputValueChanged(); });
        passwordInput.onValueChanged.AddListener(delegate { OnInputValueChanged(); });
        usernameInput.onValueChanged.AddListener(delegate { OnInputValueChanged(); });

        // Make the error message initially invisible
        registrationErrorMessage.gameObject.SetActive(false);
    }

    private void OnInputValueChanged()
    {
        ValidateAllInputs();
    }

    public void OnUpdateProfileButtonClick()
    {
        // Clear the error message before attempting to update the profile
        registrationErrorMessage.gameObject.SetActive(false);

        if (ValidateAllInputs())
        {
            StartCoroutine(UpdateProfile());
        }
    }

    public IEnumerator UpdateProfile()
    {
        string encryptedPassword = EncryptPassword(passwordInput.text);

        string updateUrl = "http://20.15.114.131:8080/api/user/profile/update";
        string jsonData = $"{{\"firstname\": \"{firstnameInput.text}\", \"lastname\": \"{lastnameInput.text}\", \"nic\": \"{nicInput.text}\", \"phoneNumber\": \"{phoneNumberInput.text}\", \"email\": \"{emailInput.text}\", \"password\": \"{encryptedPassword}\", \"username\": \"{usernameInput.text}\"}}";

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

            if (request.responseCode == 409)  // Conflict, which could mean username already exists
            {
                registrationErrorMessage.text = "Username already exists. Please choose another one.";
                registrationErrorMessage.color = Color.red;
            }
            else
            {
                registrationErrorMessage.text = "An error occurred. Please try again.";
                registrationErrorMessage.color = Color.red;
            }

            // Make the error message visible
            registrationErrorMessage.gameObject.SetActive(true);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            var json = JSON.Parse(jsonResponse);
            Debug.Log("Updated Profile Info: " + json.ToString());

            // Handle success (e.g., navigate to a different scene or provide feedback to the user)
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
        isValid &= ValidatePassword();
        isValid &= ValidateUsername();

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

    private bool ValidatePassword()
    {
        string passwordRegex = @"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@#$%^&])[A-Za-z\d@#$%^&]{12,}$";

        if (!Regex.IsMatch(passwordInput.text, passwordRegex))
        {
            passwordWarning.text = "Password should contain at least 12 characters including at least one letter, one number, and one of these symbols: @#$%^&";
            passwordWarning.color = Color.red;
            return false;
        }
        else
        {
            passwordWarning.text = "";
            return true;
        }
    }

    private bool ValidateUsername()
    {
        if (string.IsNullOrWhiteSpace(usernameInput.text))
        {
            usernameWarning.text = "Username cannot be empty.";
            usernameWarning.color = Color.red;
            return false;
        }
        else if (usernameInput.text.Length < 5 || usernameInput.text.Length > 10)
        {
            usernameWarning.text = "Username should be between 5 and 10 characters.";
            usernameWarning.color = Color.red;
            return false;
        }
        else
        {
            usernameWarning.text = "";
            return true;
        }
    }

    private string EncryptPassword(string password)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(password);
        return Convert.ToBase64String(plainTextBytes);
    }
}
