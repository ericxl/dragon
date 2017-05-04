using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.SharedModels;
using UnityEngine;
public static class PlayFabManager
{
	public static string UserName;

    public static string AuthKey { get; private set; }

	public static void Request<TRequest, TResult>(TRequest request = null, Action<TResult> resultCallback = null, Action<PlayFabError> errorCallback = null, object customData = null, Dictionary<string, string> extraHeaders = null) 
        where TRequest : PlayFabRequestCommon 
        where TResult : PlayFabResultCommon
    {
        Action<PlayFabError> errorHandler = error =>
        {
            try
            {
                // anything else heree
                //errorTimes++;

                if (typeof(TResult) == typeof(LoginResult))
                {
                    Debug.Log("This is a get inv request");
                }
            }
            finally
            {
                if (errorCallback != null)
                {
                    errorCallback(error);
                }
            }
        };

        var name = typeof(TRequest).Name;
        var methodName = name.Substring(0, name.Length - "Request".Length);
        var methods = typeof(PlayFabClientAPI).GetMethods(BindingFlags.Static | BindingFlags.Public ).Where(m => m.Name == methodName);
        if (methods == null)
        {
            throw new MissingMethodException();
        }
        var method = methods.First(m => !m.IsGenericMethod);
        var methodParams = method.GetParameters();
        var requestParam = methodParams.First();

        if (requestParam.ParameterType != typeof(TRequest))
        {
            throw new InvalidOperationException();
        }
        var actionParam = methodParams[1];
        var args = actionParam.ParameterType.GetGenericArguments();
        if (args.First().Name != typeof(TResult).Name)
        {
            throw new InvalidOperationException();
        }

        var actual = request ?? Activator.CreateInstance<TRequest>();

		method.Invoke(null, new object[] { actual, resultCallback, errorHandler, customData, extraHeaders});
    }
    /*
    public static void Execute(string functionName, Action resultCallback = null, Action<PlayFabError> errorCallback = null)
    {
        Execute<object>(functionName, null, resultCallback, errorCallback);
    }

    public static void Execute<TParameter>(string functionName, TParameter parameter = default(TParameter), Action resultCallback = null, Action<PlayFabError> errorCallback = null)
    {
        Execute(functionName, parameter, resultCallback.To<object>(), errorCallback);
    }

    public static void Execute<TParameter, TResult>(string functionName, TParameter parameter = default (TParameter), Action<TResult> resultCallback = null, Action<PlayFabError> errorCallback = null)
    {
        if (parameter == null)
        {
            parameter = Activator.CreateInstance<TParameter>();
        }
        Request<ExecuteCloudScriptRequest, ExecuteCloudScriptResult>(new ExecuteCloudScriptRequest { FunctionName = functionName, FunctionParameter = parameter},
            result =>
            {
                if (resultCallback == null) return;
                var s = JsonConvert.SerializeObject(result.FunctionResult);
                var r = JsonConvert.DeserializeObject<TResult>(s);
                resultCallback(r);
            }, errorCallback);
    }
    */

    public static void Login(string userName, Action<LoginResult> loginCallback, Action<PlayFabError> errorCallback)
    {
        Action<LoginResult> login = loginResult =>
        {
            AuthKey = loginResult.SessionTicket;
            if (loginCallback != null)
            {
                loginCallback(loginResult);
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(new LoginWithEmailAddressRequest{
        	Email = userName,
        	Password = "1234567890",
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetTitleData = true,
                GetUserData = true,
                GetPlayerStatistics = true,
                GetUserInventory = true,
                GetUserVirtualCurrency = true
            },
            TitleId = PlayFabSettings.TitleId
		}, login, null);
		/*
        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest
        {
            CreateAccount = true,
            CustomId =
#if UNITY_EDITOR
                userName,
#else
                "pc_" + userName,
#endif
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetTitleData = true,
                GetUserData = true,
                GetPlayerStatistics = true,
                GetUserInventory = true,
                GetUserVirtualCurrency = true
            },
            TitleId = PlayFabSettings.TitleId
        }, login, null);
        */
    }

    /*
    public static void BuyTimedItem(string itemId, Action<DateTime> resultCallback, Action<PlayFabError> errorCallback)
    {
        var ci = (from i in Database.Game.Items
                  where i.ItemId == itemId
                  select i).FirstOrDefault();
        if (ci != null)
        {
            var pair = ci.VirtualCurrencyPrices.FirstOrDefault();
            var key = pair.Key;
            var value = pair.Value;
            Action<PlayFabError> purchaseFailedAction = error =>
            {
                if (errorCallback != null)
                {
                    errorCallback(error);
                }
            };
            Action<PurchaseItemResult> purchaseCompleteAction = purchaseCompleteResult =>
            {
                Execute<object, long>("addTimeStampToItem", new { ItemInstanceId = purchaseCompleteResult.Items.First().ItemInstanceId, ItemId = purchaseCompleteResult.Items.First().ItemId },
                result =>
                {
                    var dt = result.ToEpochByMilliseconds();
                    if (resultCallback != null)
                    {
                        Request<GetUserInventoryRequest, GetUserInventoryResult>();
                        resultCallback(dt);
                    }
                }, error =>
                {
                    purchaseFailedAction(error);
                });
            };

            Request(new PurchaseItemRequest { ItemId = itemId, VirtualCurrency = key, Price = (int)value }, purchaseCompleteAction);
        }
    }

	public static void RedeemItem(string itemInstanceId, Action<bool> resultCallback, Action<PlayFabError> errorCallback){
		var ci = (from i in Database.User.PlayerInventory
			where i.ItemInstanceId == itemInstanceId
                  select i).FirstOrDefault();
        if (ci != null)
        {
			Execute<object, bool>("redeemLockedItem", new { ItemInstanceId = itemInstanceId },
                result =>
                {
					if (resultCallback != null)
                    {
                        Request<GetUserInventoryRequest, GetUserInventoryResult>();
                        resultCallback(result);
                    }
                }, error =>
                {
					if (errorCallback != null)
                	{
                    	errorCallback(error);
                	}
                });

            }
        }
        */
	public static void AddFriend(string email, Action resultCallback, Action<PlayFabError> errorCallback){
		Action<AddFriendResult> c = (result) =>
            {
            };
		PlayFabClientAPI.AddFriend(new AddFriendRequest(){FriendEmail = email},c, errorCallback);
	}

}