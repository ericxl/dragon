using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

using TMPro;
public class TradePanel : MonoBehaviour {
	public TextMeshProUGUI tradeNameText;
	public GameObject heartTradeInfo;
	public GameObject swordTradeInfo;
	public GameObject shieldTradeInfo;



	public string tradePlayFabId;
	public void UpdatePlayerTradeInfo(){
		PlayFabClientAPI.GetUserData(new GetUserDataRequest(){PlayFabId = tradePlayFabId, Keys = new string[]{"Heart","Sword","Shield"}.ToList()},
		result=>{
			if(result.Data.ContainsKey("Heart")){
				if (result.Data["Heart"].Value != "0"){
						heartTradeInfo.SetActive(true);
				}
				else {
						heartTradeInfo.SetActive(false);

				}
			}
			else {
						heartTradeInfo.SetActive(false);

				}
			if(result.Data.ContainsKey("Sword")){
				if (result.Data["Sword"].Value != "0"){
						swordTradeInfo.SetActive(true);
				}
				else{
						swordTradeInfo.SetActive(false);

				}
			}else {
					swordTradeInfo.SetActive(false);

				}
			if(result.Data.ContainsKey("Shield")){
				if (result.Data["Shield"].Value != "0"){
						shieldTradeInfo.SetActive(true);
				}
				else {
						shieldTradeInfo.SetActive(false);

				}
			}else {
					shieldTradeInfo.SetActive(false);

				}
		}, error=>{

		});
	}

	public void HeartBuy(){
		PlayFabServerAPI.UpdateUserData(new PlayFab.ServerModels.UpdateUserDataRequest(){PlayFabId = tradePlayFabId,KeysToRemove = new string[]{"Heart"}.ToList()},
		result=>{
				//UpdatePlayerTradeInfo();
		},error=>{

		});
		PlayFabServerAPI.AddUserVirtualCurrency(new PlayFab.ServerModels.AddUserVirtualCurrencyRequest(){PlayFabId = tradePlayFabId, VirtualCurrency="DM",Amount = 4},
		result=>{}, error=>{});
		PlayFabServerAPI.GetUserInventory(new PlayFab.ServerModels.GetUserInventoryRequest(){PlayFabId = tradePlayFabId},result=>{
			PlayFab.ServerModels.ItemInstance i = result.Inventory.Where(t=>t.ItemId == "Heart").ToList()[0];
			PlayFabServerAPI.RevokeInventoryItem(new PlayFab.ServerModels.RevokeInventoryItemRequest(){PlayFabId = tradePlayFabId, ItemInstanceId = i.ItemInstanceId},result1=>{}, error1=>{});
		}, error=>{
		});
		PlayFabServerAPI.SubtractUserVirtualCurrency(new PlayFab.ServerModels.SubtractUserVirtualCurrencyRequest(){PlayFabId = PlayFabManager.playerId, VirtualCurrency="DM",Amount = 4},
		result=>{}, error=>{});
		PlayFabServerAPI.GrantItemsToUser(new PlayFab.ServerModels.GrantItemsToUserRequest(){PlayFabId = PlayFabManager.playerId, ItemIds=new string[]{"Heart"}.ToList()},result=>{
		},error=>{});
	}

	public void SwordBuy(){
		PlayFabServerAPI.UpdateUserData(new PlayFab.ServerModels.UpdateUserDataRequest(){PlayFabId = tradePlayFabId,KeysToRemove = new string[]{"Sword"}.ToList()},
		result=>{
			//UpdatePlayerTradeInfo();
		},error=>{

		});

		PlayFabServerAPI.AddUserVirtualCurrency(new PlayFab.ServerModels.AddUserVirtualCurrencyRequest(){PlayFabId = tradePlayFabId, VirtualCurrency="GC",Amount = 6},
		result=>{}, error=>{});
		PlayFabServerAPI.GetUserInventory(new PlayFab.ServerModels.GetUserInventoryRequest(){PlayFabId = tradePlayFabId},result=>{
			PlayFab.ServerModels.ItemInstance i = result.Inventory.Where(t=>t.ItemId == "Sword").ToList()[0];
			PlayFabServerAPI.RevokeInventoryItem(new PlayFab.ServerModels.RevokeInventoryItemRequest(){PlayFabId = tradePlayFabId, ItemInstanceId = i.ItemInstanceId},result1=>{}, error1=>{});
		}, error=>{
		});
		PlayFabServerAPI.SubtractUserVirtualCurrency(new PlayFab.ServerModels.SubtractUserVirtualCurrencyRequest(){PlayFabId = PlayFabManager.playerId, VirtualCurrency="GC",Amount = 6},
		result=>{}, error=>{});
		PlayFabServerAPI.GrantItemsToUser(new PlayFab.ServerModels.GrantItemsToUserRequest(){PlayFabId = PlayFabManager.playerId, ItemIds=new string[]{"Sword"}.ToList()},result=>{
		},error=>{});
	}

	public void ShieldBuy(){
		PlayFabServerAPI.UpdateUserData(new PlayFab.ServerModels.UpdateUserDataRequest(){PlayFabId = tradePlayFabId,KeysToRemove = new string[]{"Shield"}.ToList()},
		result=>{
			//UpdatePlayerTradeInfo();
		},error=>{

		});


		PlayFabServerAPI.AddUserVirtualCurrency(new PlayFab.ServerModels.AddUserVirtualCurrencyRequest(){PlayFabId = tradePlayFabId, VirtualCurrency="GC",Amount = 8},
		result=>{}, error=>{});
		PlayFabServerAPI.GetUserInventory(new PlayFab.ServerModels.GetUserInventoryRequest(){PlayFabId = tradePlayFabId},result=>{
			PlayFab.ServerModels.ItemInstance i = result.Inventory.Where(t=>t.ItemId == "Shield").ToList()[0];
			PlayFabServerAPI.RevokeInventoryItem(new PlayFab.ServerModels.RevokeInventoryItemRequest(){PlayFabId = tradePlayFabId, ItemInstanceId = i.ItemInstanceId},result1=>{}, error1=>{});
		}, error=>{
		});
		PlayFabServerAPI.SubtractUserVirtualCurrency(new PlayFab.ServerModels.SubtractUserVirtualCurrencyRequest(){PlayFabId = PlayFabManager.playerId, VirtualCurrency="GC",Amount = 8},
		result=>{}, error=>{});
		PlayFabServerAPI.GrantItemsToUser(new PlayFab.ServerModels.GrantItemsToUserRequest(){PlayFabId = PlayFabManager.playerId, ItemIds=new string[]{"Shield"}.ToList()},result=>{
		},error=>{});
	}

	void OnEnable(){
		UpdatePlayerTradeInfo();
	}
}
