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
        LevelManager.Instance.levelCreateFuncEvent += SetGamePlayLevelNumber;

        Screen.orientation = ScreenOrientation.Portrait;

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

    public void NextBtn()
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
        SceneManager.LoadScene(1);
    }

}
