using System.Collections;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{

    public static GameOverManager Instance;

    public delegate void GameOverFunc();
    public GameOverFunc GameOverFuncEvent;
    private Coroutine _gameOverCor;

    private void Start()
    {
        Instance = this;
    }
    public void GameOver(float time)
    {

        if (_gameOverCor != null)
            StopCoroutine(_gameOverCor);
        _gameOverCor = StartCoroutine(DelayGameOver(time));


    }

    private IEnumerator DelayGameOver(float time)
    {
        yield return new WaitForSeconds(time);

        if (!GameManager.Instance.IsGamePlayState() && !GameManager.Instance.IsMainMenuState()) yield break;
        yield return new WaitForSeconds(1f);

        try
        {

            GameManager.Instance.SetGameOverState();


            //UIManager will handle the UI On Off Setting using event system if confusion? visit it


            var attempt = GameData.GetLevelAttemptRate();

            attempt++;
            GameData.SetLevelAttemptRate(attempt);

            GameOverFuncEvent.Invoke();

            SoundsManager.instance.PlayLevelFailSound(SoundsManager.instance.AS);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.ToString());

        }

    }
}
