using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class RegisterView : MonoBehaviour {

    [SerializeField] private InputField usernameField;
    [SerializeField] private InputField EmailField;
    [SerializeField] private InputField passwordField;
    [SerializeField] private InputField confirmPassword;

    public void back()
    {
        SceneManager.LoadScene("Login");
    }

    public void addNewUser()
    {
        if (passwordField.text == confirmPassword.text)
        {
            PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest()
            {
                TitleId = "4A9",
                Username = usernameField.text,
                Email = EmailField.text,
                Password = passwordField.text,

            }, LoginResult => 
            {
                Debug.Log("Register Success");
                setUserData();
                SceneManager.LoadScene("Login");

            }, error =>
            {
                Debug.Log(error.ErrorMessage);
            });
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void setUserData()
    {
        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
             Data = new Dictionary<string, string>(){
                 { "GOLD", "1" },
                 { "LVL", "1" },
                 { "HP", "100" },
                 { "XP", "0" },
                 { "MP", "100" },
                 { "ATTACK", "20" },
                 { "DEFENCE", "0" }
             }
        };

        PlayFabClientAPI.UpdateUserData(request, (result) =>
        {
            Debug.Log("Successfully set user data");
        }, (error) =>
        {
            Debug.Log("Got error setting user data Ancestor to Arthur");
            Debug.Log(error.ErrorDetails);
        });
    }
}
