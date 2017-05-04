using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
public class InventoryPanel : MonoBehaviour {
	public TextMeshProUGUI HeartsValueText;
	public TextMeshProUGUI SwordsValueText;
	public TextMeshProUGUI ShieldsValueText;
	public Button HeartsValueButton;
	public Button SwordsValueButton;
	public Button ShieldsValueButton;
	public void OnEnable(){
		UpdateInventory();
	}
	public void OnHeartsClick(){
		PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest(){ItemId="Heart", VirtualCurrency ="DM", Price = 2},
		result =>{
			UpdateInventory();
		}, error=>{

		});
	}

	public void OnSwordsClick(){
		PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest(){ItemId="Sword", VirtualCurrency ="GC", Price = 3},
		result =>{
			UpdateInventory();
		}, error=>{

		});
	}

	public void OnShieldClick(){
		PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest(){ItemId="Shield", VirtualCurrency ="GC", Price = 4},
		result =>{
			UpdateInventory();
		}, error=>{

		});
	}

	public void OnHeartsOpenTradeHandler(){
		if(HeartsValueText.text == "0") return;
		PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest(){Data = new Dictionary<string, string>{{ "Heart", "1" }}, Permission = UserDataPermission.Public}, 
		result=>{
			SetActiveButton(HeartsValueButton, false);

		}, error=>{

		});
	}

	public void OnSwordsOpenTradeHandler(){
		if(SwordsValueText.text == "0") return;
		PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest(){Data = new Dictionary<string, string>{{ "Sword", "1" }}, Permission = UserDataPermission.Public}, 
		result=>{
			SetActiveButton(SwordsValueButton, false);

		}, error=>{

		});
	}

	public void OnShieldOpenTradeHandler(){
		if(ShieldsValueText.text == "0") return;
		PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest(){Data = new Dictionary<string, string>{{ "Shield", "1" }}, Permission = UserDataPermission.Public}, 
		result=>{
			SetActiveButton(ShieldsValueButton, false);
		}, error=>{

		});
	}

	void SetActiveButton(Button b, bool isActive){
		if(isActive){
			b.image.color = Color.white;
			b.interactable = true;
		}
		else {
			b.image.color = Color.green;
			b.interactable = false;
		}
	}
	public void UpdateInventory(){
		PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), 
		result=>{
			int heartCount = result.Inventory.Where(t=>t.ItemId == "Heart").ToList().Count;
				int swordsCount = result.Inventory.Where(t=>t.ItemId == "Sword").ToList().Count;
				int shieldsCount = result.Inventory.Where(t=>t.ItemId == "Shield").ToList().Count;
			HeartsValueText.text = heartCount.ToString();
			SwordsValueText.text = swordsCount.ToString();
			ShieldsValueText.text = shieldsCount.ToString();
		},error=>{

		});

		PlayFabClientAPI.GetUserData(new GetUserDataRequest(){Keys = new string[]{"Heart","Sword","Shield"}.ToList()},
		result=>{
			if(result.Data.ContainsKey("Heart")){
				if (result.Data["Heart"].Value != "0"){
					SetActiveButton(HeartsValueButton, false);
				}else {
						SetActiveButton(HeartsValueButton, true);

				}
			}else {
					SetActiveButton(HeartsValueButton, true);

			}
			if(result.Data.ContainsKey("Sword")){
				if (result.Data["Sword"].Value != "0"){
					SetActiveButton(SwordsValueButton, false);
				}else {
						SetActiveButton(SwordsValueButton, true);

				}
			}
			else {
					SetActiveButton(SwordsValueButton, true);
			}
			if(result.Data.ContainsKey("Shield")){
				if (result.Data["Shield"].Value != "0"){
					SetActiveButton(ShieldsValueButton, false);
				}else {
						SetActiveButton(ShieldsValueButton, true);

				}
			}else
				{
					SetActiveButton(ShieldsValueButton, true);
			}
		}, error=>{

		});
	}
}
