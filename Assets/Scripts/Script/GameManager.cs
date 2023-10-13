using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public enum GameState
    {
        MainMenu,
        GamePlay,
        GameOver,
        LevelComplete

    }
    public GameState gamestate = GameState.MainMenu;
    public static GameManager Instance;

    [FormerlySerializedAs("UI")]
    [Header("kindly go through all thanks")]
    [Header("--------Caution------- Template Keys functions[Level-Start , GameOver , LevelComplete]  design using '--Event System Delegate events--'")]
    public UIManager ui;
    public LevelCompleteManager levelCompleteManager;
    public GameOverManager gameOverManager;


    public delegate void GameStarFunc();
    public GameStarFunc GameStarFuncEvent;

    public delegate void GameResetFunc();
    public GameStarFunc GameResetFuncEvent;

    public int CurrentLevel;
    public GameObject Tutorial;


    private void Awake()
    {
        Instance = this;
        GameResetFuncEvent += GameReset;
    }

    private void Start()
    {
        HamzaFunction();
        LevelManager.Instance.levelCreateFuncEvent += SetGamePlayLevelNumber;
        if(!LevelManager.Instance.TestLevel)
            AdsController.instance.ShowAd(AdType.BANNER, 0);


    }

    private void OnDestroy()
    {
        LevelManager.Instance.levelCreateFuncEvent -= SetGamePlayLevelNumber;
    }

    private void SetGamePlayLevelNumber(int Level)
    {
        CurrentLevel = Level;


    }

    private void SetMainMenuState()
    {
        gamestate = GameState.MainMenu;
    }
    public void HamzaFunction()
    {
        Debug.Log("HamzaAziz");
    }
    private void SetGamePlayState()
    {
        gamestate = GameState.GamePlay;
    }
    public void SetGameOverState()
    {
        gamestate = GameState.GameOver;
    }
    public void SetLevelCompleteState()
    {
        gamestate = GameState.LevelComplete;
    }
    public bool IsMainMenuState()
    {
        return gamestate == GameState.MainMenu;
    }
    public bool IsGamePlayState()
    {
        return gamestate == GameState.GamePlay;
    }
    public bool IsGameOverState()
    {
        return gamestate == GameState.GameOver;
    }
    public bool IsLevelCompleteState()
    {
        return gamestate == GameState.LevelComplete;
    }

    public void GameStart()
    {
        GameStarFuncEvent.Invoke();
        SetGamePlayState();
        //UIManager will handle the UI On Off Setting using event system if consfusion? visit it
    }

    private void GameReset()
    {

        SetMainMenuState();
        //UIManager will handle the UI On Off Setting using event system if consfusion? visit it
    }

    public void RetryBtn()
    {
        GameResetFuncEvent.Invoke();
        LoadSceneFunction("ParkingJam");
    }

    private static void LoadSceneFunction(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Resume()
    {
        Time.timeScale = 1;
    }
    public void SkipLevel()
    {

        if (AdsController.instance.admobController.IsRewardedAdLoaded())
        {
            AdsController.instance.LoadingPanel.SetActive(true);
            Invoke(nameof(Reward), 8f);
        }
        else
        {
            AdsController.instance.LoadingPanel.SetActive(true);
            AdsController.instance.admobController.RequestRewardedAd();
            Invoke(nameof(Reward), 8f);
        }

    }

    public void Reward()
    {
        AdsController.instance.ShowAd(AdNetwork.ADMOB, AdType.REWARDED, SkipLevelReward);
    }


    public void SkipLevelReward()
    {
        var lvl = GameData.GetLevelNumber();
       
        if ((lvl % (LevelManager.Instance.Levels.Count) == 0))
        {


            LevelManager.Instance.LevelsArrayReshuffle();

        }

        lvl++;

        GameData.SetLevelNumber(lvl);

        lvl = GameData.GetLevelNumberIndex();
        lvl++;
        if ((lvl) > (LevelManager.Instance.Levels.Count))
            lvl = 1;
        GameData.SetLevelNumberIndex(lvl);

        //gameResetFuncEvent();

        LoadSceneFunction("ParkingJam");


        //UIManager will handle the UI On Off Setting using event system if consfusion? visit it
    }


    public void NextBtn()
    {
        if (!LevelManager.Instance.TestLevel)
        {
            AdsController.instance.ShowAd(AdNetwork.ADMOB, AdType.INTERSTITIAL);
            Invoke(nameof(NextLevel), .5f);
        }
        else
        {
            NextLevel();
        }

    }
    public void NextLevel()
    {
        var lvl = GameData.GetLevelNumber();
        if ((lvl % (LevelManager.Instance.Levels.Count) == 0))
        {


            LevelManager.Instance.LevelsArrayReshuffle();

        }

        lvl++;

        GameData.SetLevelNumber(lvl);

        lvl = GameData.GetLevelNumberIndex();
        lvl++;
        if ((lvl) > (LevelManager.Instance.Levels.Count))
            lvl = 1;
        GameData.SetLevelNumberIndex(lvl);

        //gameResetFuncEvent();

        LoadSceneFunction("ParkingJam");


        //UIManager will handle the UI On Off Setting using event system if consfusion? visit it

    }
    public void GoMenu()
    {
        if(!LevelManager.Instance.TestLevel)
        {
            AdsController.instance.ShowAd(AdNetwork.ADMOB, AdType.INTERSTITIAL);
            Invoke(nameof(SceneLoad), .3f);
        }
        else
        {
            SceneLoad();
        }
    }
    public void SceneLoad()
    {
        SceneManager.LoadScene(1);
    }
}
