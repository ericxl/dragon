using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PlayFab.ClientModels;
using PlayFab.Events;

public static class Database
{
    static Database()
    {
        foreach (var method in from m in typeof(User).GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
                               where m.Name.StartsWith("SetUp")
                               select m)
        {
            method.Invoke(null, null);
        }

        foreach (var method in from m in typeof(Game).GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
                               where m.Name.StartsWith("SetUp")
                               select m)
        {
            method.Invoke(null, null);
        }
    }

    public static class Game
    {

        public static DateTime ServerTime;
        public static List<CatalogItem> Items;
        public static List<int> LevelEXPs;

        #region Events Setup

        private static void SetUpServerTimeHandlers()
        {
            PlayFabEvents.Instance.OnGetTimeResultEvent += r =>
            {
                ServerTime = r.Time;
                OnServerTimeChanged(r.Time);
            };
        }

        #endregion
    }

    public static class User
    {
        public static string PlayFabId;
        public static string DisplayName;
        public static int PlayerLevel;
        public static int PlayerEXP;
		public static int CashCoins;
		public static int GameCoins;
        public static List<ItemInstance> PlayerInventory;
		public static List<VirtusArts.FriendInfo> Friends;

        #region Events Setup

        private static void SetUpPlayFabIdHandlers()
        {
            PlayFabEvents.Instance.OnLoginResultEvent += r =>
            {
                if (PlayFabId != r.PlayFabId)
                {
                    PlayFabId = r.PlayFabId;
                    OnPlayFabIdChanged(r.PlayFabId);
                }
            };
        }

        private static void SetUpDisplayNameHandlers()
        {
            PlayFabEvents.Instance.OnUpdateUserTitleDisplayNameResultEvent += r =>
            {
                if (DisplayName != r.DisplayName)
                {
                    DisplayName = r.DisplayName;
                    OnDisplayNameChanged(r.DisplayName);
                }
            };

            PlayFabEvents.Instance.OnGetAccountInfoResultEvent += r =>
            {
                if (r.AccountInfo.PlayFabId == PlayFabId.ToString())
                {
                    if (DisplayName != r.AccountInfo.TitleInfo.DisplayName)
                    {
                        DisplayName = r.AccountInfo.TitleInfo.DisplayName;
                        OnDisplayNameChanged(r.AccountInfo.TitleInfo.DisplayName);
                    }
                }
            };
        }

        private static void SetUpPlayerLevelHandlers()
        {
            PlayFabEvents.Instance.OnGetPlayerStatisticsResultEvent += r =>
            {
                var levelStat = r.Statistics.FirstOrDefault(t => t.StatisticName.Equals(Constant.PlayerLevel, StringComparison.InvariantCultureIgnoreCase));
                var level = levelStat == null ? 0 : levelStat.Value;
                if (PlayerLevel != level)
                {
                    PlayerLevel = level;
                    OnPlayerLevelChanged(level);
                }
            };
            PlayFabEvents.Instance.OnLoginResultEvent += r =>
            {
                if (r.InfoResultPayload.PlayerStatistics != null)
                {
                    var levelStat = r.InfoResultPayload.PlayerStatistics.FirstOrDefault(t => t.StatisticName.Equals(Constant.PlayerLevel, StringComparison.InvariantCultureIgnoreCase));
                    var level = levelStat == null ? 0 : levelStat.Value;
                    if (PlayerLevel != level)
                    {
                        PlayerLevel = level;
                        OnPlayerLevelChanged(level);
                    }
                }
            };
        }

        private static void SetUpPlayerEXPHandlers()
        {
            PlayFabEvents.Instance.OnGetPlayerStatisticsResultEvent += r =>
            {
                var expStat = r.Statistics.FirstOrDefault(t => t.StatisticName.Equals(Constant.PlayerEXP, StringComparison.InvariantCultureIgnoreCase));
                var exp = expStat == null ? 0 : expStat.Value;
                if (PlayerEXP != exp)
                {
                    PlayerEXP = exp;
                    OnPlayerEXPChanged(exp);
                }
            };
            PlayFabEvents.Instance.OnLoginResultEvent += r =>
            {
                if (r.InfoResultPayload.PlayerStatistics != null)
                {
                    var expStat = r.InfoResultPayload.PlayerStatistics.FirstOrDefault(t => t.StatisticName.Equals(Constant.PlayerEXP, StringComparison.InvariantCultureIgnoreCase));
                    var exp = expStat == null ? 0 : expStat.Value;
                    if (PlayerEXP != exp)
                    {
                        PlayerEXP = exp;
                        OnPlayerEXPChanged(exp);
                    }
                }
            };
        }

        private static void SetUpCashCoinsHandlers()
        {
            PlayFabEvents.Instance.OnGetUserInventoryResultEvent += r =>
            {
                var coin = r.VirtualCurrency.TryGet(Constant.Diamonds);
                if (CashCoins != coin)
                {
                    CashCoins = coin;
                    OnCashCoinsChanged(coin);
                }
            };

            PlayFabEvents.Instance.OnLoginResultEvent += r =>
            {
                if (r.InfoResultPayload.UserVirtualCurrency != null)
                {
					var coin = r.InfoResultPayload.UserVirtualCurrency.TryGet(Constant.Diamonds);
                    if (CashCoins != coin)
                    {
                        CashCoins = coin;
                        OnCashCoinsChanged(coin);
                    }
                }
            };
        }

        private static void SetUpGameCoinsHandlers()
        {
            PlayFabEvents.Instance.OnGetUserInventoryResultEvent += r =>
            {
                var coin = r.VirtualCurrency.TryGet(Constant.Gold);
                if (GameCoins != coin)
                {
                    GameCoins = coin;
                    OnGameCoinsChanged(coin);
                }
            };
            PlayFabEvents.Instance.OnLoginResultEvent += r =>
            {
                if (r.InfoResultPayload.UserVirtualCurrency != null)
                {
                    var coin = r.InfoResultPayload.UserVirtualCurrency.TryGet(Constant.Gold);
                    if (GameCoins != coin)
                    {
                        GameCoins = coin;
                        OnGameCoinsChanged(coin);
                    }
                }
            };
        }


        #endregion

    }

    #region Events

    public static event Action<DateTime> OnServerTimeChanged = s => { };


    public static event Action<string> OnPlayFabIdChanged = s => { };
    public static event Action<string> OnDisplayNameChanged = s => { };
    public static event Action<int> OnPlayerLevelChanged = s => { };
    public static event Action<int> OnPlayerEXPChanged = s => { };
    public static event Action<int> OnCashCoinsChanged = s => { };
    public static event Action<int> OnGameCoinsChanged = s => { };
	public static event Action<List<VirtusArts.FriendInfo>> OnFriendsChanged = s => { };

    #endregion
}

