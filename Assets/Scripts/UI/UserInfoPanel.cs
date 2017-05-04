using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using CoroutineHelper;
public class UserInfoPanel : MonoBehaviour {
	public TextMeshProUGUI GoldValueText;
	public TextMeshProUGUI DiamondValueText;

	public void Start(){
		Run.Every(10, 3, ()=>{
			PlayFabClientAPI.GetUserInventory(new PlayFab.ClientModels.GetUserInventoryRequest(),
			result=>{

			},error=>{

			});
		});
		PlayFab.Events.PlayFabEvents.Instance.OnGetUserInventoryResultEvent += r =>
            {
                var gold = r.VirtualCurrency.TryGet(Constant.Gold);
				GoldValueText.text = gold.ToString();
            };
		PlayFab.Events.PlayFabEvents.Instance.OnLoginResultEvent += r =>
            {
                if (r.InfoResultPayload.UserVirtualCurrency != null)
                {
					var gold = r.InfoResultPayload.UserVirtualCurrency.TryGet(Constant.Gold);
					GoldValueText.text = gold.ToString();
				}
            };

		PlayFab.Events.PlayFabEvents.Instance.OnGetUserInventoryResultEvent += r =>
            {
                var diamond = r.VirtualCurrency.TryGet(Constant.Diamonds);
				DiamondValueText.text = diamond.ToString();
            };
		PlayFab.Events.PlayFabEvents.Instance.OnLoginResultEvent += r =>
            {
                if (r.InfoResultPayload.UserVirtualCurrency != null)
                {
					var diamond = r.InfoResultPayload.UserVirtualCurrency.TryGet(Constant.Diamonds);
					DiamondValueText.text = diamond.ToString();
				}
            };
	}
}
