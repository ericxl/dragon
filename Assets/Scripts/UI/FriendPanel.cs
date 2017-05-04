using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using CoroutineHelper;
public class FriendPanel : MonoBehaviour {
	public TMP_InputField FriendNameInputFieid;
	public TextMeshProUGUI FriendErrorText;
	public GameObject FriendListItemPrefab;
	public GameObject FriendContent;
	public TradePanel tradePanel;
	private List<GameObject> friendLists = new List<GameObject>();

	public void AddFriendButtonHandler(){
		PlayFabClientAPI.AddFriend(new AddFriendRequest{FriendUsername = FriendNameInputFieid.text},
		result=>{

			FriendErrorText.color = Color.green;
			FriendErrorText.text = "Add Friend Success!";
			UpdateFriendLists();
			Run.After(3, ()=>{
				FriendErrorText.color = Color.black;
				FriendErrorText.text = "";
			});
		}, error=>{
			FriendErrorText.color = Color.red;
			FriendErrorText.text = error.ErrorMessage;
			Run.After(3, ()=>{
				FriendErrorText.color = Color.black;
				FriendErrorText.text = "";
			});
		});
		FriendNameInputFieid.text = "";
	}

	public void OnEnable(){
		UpdateFriendLists();
	}

	public void UpdateFriendLists(){
		foreach(var g in friendLists){
			Destroy(g);
		}
		friendLists.Clear();
		PlayFabClientAPI.GetFriendsList(new GetFriendsListRequest(),
			result=>{
			foreach(var f in result.Friends){
				var go = Instantiate(FriendListItemPrefab, FriendContent.transform);
				var item = go.GetComponent<FriendPanelItem>();
				item.friendName.text = f.Username;
				item.tradeButton.onClick.AddListener(()=>{
					tradePanel.tradePlayFabId = f.FriendPlayFabId;

					tradePanel.gameObject.SetActive(true);
					//this.gameObject.SetActive(false);
					tradePanel.tradeNameText.text = "Trade: " + f.Username;
				});
				friendLists.Add(go);
			}
		}, error=>{
			FriendErrorText.color = Color.red;
			FriendErrorText.text = error.ErrorMessage;
			Run.After(3, ()=>{
				FriendErrorText.color = Color.black;
				FriendErrorText.text = "";
			});
		});

	}
}
