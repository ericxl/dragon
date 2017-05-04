using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
public class PlayFabLevelLoginEnsure : MonoBehaviour {
	public string defaultEmail;
	public string defaultPassword;
	// Use this for initialization
	void Start () {
		if (!PlayFabClientAPI.IsClientLoggedIn()){
			PlayFabClientAPI.LoginWithEmailAddress(new LoginWithEmailAddressRequest(){
				Email = defaultEmail,
				Password = defaultPassword,
				InfoRequestParameters = new GetPlayerCombinedInfoRequestParams{
					GetUserAccountInfo = true,
					GetUserInventory = true,
					GetUserVirtualCurrency = true
				}
        }, loginResult =>
        {
        	PlayFabManager.UserName = loginResult.InfoResultPayload.AccountInfo.Username;
			PlayFabManager.playerId = loginResult.InfoResultPayload.AccountInfo.PlayFabId;

        }, error =>
        {
            Debug.Log(error.ErrorMessage);
        });
		}
	}
}
