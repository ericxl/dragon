﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
public class LoginView : MonoBehaviour
{
    [SerializeField] private InputField userEmailField;
    [SerializeField] private InputField passwordField;
	// Use this for initialization
    private void Start () {

	}
	
	// Update is called once per frame
    private void Update () {
		
	}

    public void Login()
    {
        PlayFabClientAPI.LoginWithEmailAddress(new LoginWithEmailAddressRequest(){
            Email = userEmailField.text,
            Password = passwordField.text
        }, loginResult =>
        {
            Debug.Log("success");
            //go to game scene
        }, error =>
        {
            Debug.Log(error.ErrorMessage);
        });
    }
}
