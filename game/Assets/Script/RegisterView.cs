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
}
