using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;


public class RegisterView : MonoBehaviour {
	[SerializeField] private InputField userEmailField;
	[SerializeField] private InputField passwordField1;
	[SerializeField] private InputField passwordField2;
	[SerializeField] private Text warningText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Login(){
		SceneManager.LoadScene("Login");
	}

	public void Register()
	{
		if (passwordField1.text != passwordField2.text) {
			Debug.Log ("different passowrd!");
			warningText.text = "different passowrd!";
			return;
		} 

			
		PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest(){
			TitleId = "4A9",
			Username = "apple",
			Email = userEmailField.text,
			Password = passwordField1.text,
		}, loginResult =>
			{
				Debug.Log("success regist");
				SceneManager.LoadScene("Login");
				//go to game scene
			}, error =>
			{
				warningText.text = error.ErrorMessage;
				Debug.Log(error.ErrorMessage);
			});
	}
}
