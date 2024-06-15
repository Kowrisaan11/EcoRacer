using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Register : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_InputField passwordField;
    public Button submitButton;

    public void CallRegister()
    {
        StartCoroutine(RegisterUser());
    }

    IEnumerator RegisterUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        // WWW www = new WWW("http://localhost/sqlconnect/register.php", form); 
        WWW www = new WWW("https://php2-okc9.onrender.com/register.php", form);

        yield return www;

        if (www.text == "0")
        {
            Debug.Log("User created successfully.");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0); 
        }
        else
        {
            Debug.Log("User creation failed. Error #" + www.text);
        }
    }
}
