using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;
    private void Awake()
    {
        Instance = this;
        Initialization();
    }

    private static void Initialization()
    {
        if (GameVersionHasKey(1)) return; //version 1.0 //1
        SetGameVersionKey(1, 1);
        SetCoins(0);

        SetLevelNumber(1);

    }
    #region GameVersions

    private static bool GameVersionHasKey(int versionNumber)
    {
        return PlayerPrefs.HasKey(GamePlayerPrefKeys.GameVersion + versionNumber);
    }

    private static void SetGameVersionKey(int versionNumber, int value)
    {
        PlayerPrefs.SetInt(GamePlayerPrefKeys.GameVersion + versionNumber, value);
    }
    public int GetGameVersionKey(int versionNumber)
    {
        return PlayerPrefs.GetInt(GamePlayerPrefKeys.GameVersion + versionNumber, 0);
    }
    #endregion


    #region GameCurrency

    private static void SetCoins(int value)
    {
        PlayerPrefs.SetInt(GamePlayerPrefKeys.Coins, value);
    }
    public static int GetCoins()
    {
        return PlayerPrefs.GetInt(GamePlayerPrefKeys.Coins, 0);
    }

    #endregion
    
    #region Level
    public static void SetLevelNumber(int value)
    {
        PlayerPrefs.SetInt(GamePlayerPrefKeys.Level, value);
    }
    public static int GetLevelNumber()
    {
        return PlayerPrefs.GetInt(GamePlayerPrefKeys.Level, 1);
    }
    public static void SetLevelNumberIndex(int value)
    {
        PlayerPrefs.SetInt(GamePlayerPrefKeys.LevelsIndex, value);
    }
    public static int GetLevelNumberIndex()
    {
        return PlayerPrefs.GetInt(GamePlayerPrefKeys.LevelsIndex, 1);
    }

    public static bool LevelRandomizationOrderHasKey()
    {

        return PlayerPrefs.HasKey(GamePlayerPrefKeys.LevelsRandomizationOrder);


    }
    public static void SetLevelRandomizationOrder(LevelRandomization lr)
    {
        PlayerPrefs.SetString(GamePlayerPrefKeys.LevelsRandomizationOrder, JsonUtility.ToJson(lr));
    }
    public static LevelRandomization GetLevelRandomizationOrder()
    {
        return JsonUtility.FromJson<LevelRandomization>(PlayerPrefs.GetString(GamePlayerPrefKeys.LevelsRandomizationOrder));
    }

    public static void SetLevelAttemptRate(int count)
    {
        PlayerPrefs.SetInt(GamePlayerPrefKeys.LevelAttemptRate, count);
    }
    public static int GetLevelAttemptRate()
    {
        return PlayerPrefs.GetInt(GamePlayerPrefKeys.LevelAttemptRate, 1);
    }

    #endregion
}
