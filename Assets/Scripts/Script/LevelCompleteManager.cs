using System.Collections;
using UnityEngine;

public class LevelCompleteManager : MonoBehaviour
{
    public static LevelCompleteManager Instance;

    public delegate void LevelCompleteFunc();
    public LevelCompleteFunc levelCompleteFuncEvent;

    private int _currentLevel;

    private Coroutine _levelCompleteCor;

    private void OnEnable()
    {
        Instance = this;
    }
    private void OnDestroy()
    {
        LevelManager.Instance.levelCreateFuncEvent -= SetGamePlayLevelNumberText;
    }
    private void Start()
    {
        LevelManager.Instance.levelCreateFuncEvent += SetGamePlayLevelNumberText;
    }

    private void SetGamePlayLevelNumberText(int level)
    {
        _currentLevel = level;
    }
    public void LevelComplete(float time)
    {

        SoundsManager.instance.StopMusic();
        if (_levelCompleteCor != null)
            StopCoroutine(_levelCompleteCor);
        _levelCompleteCor = StartCoroutine(DelayLevelComplete(time));

    }


    private IEnumerator DelayLevelComplete(float time)
    {
        yield return new WaitForSeconds(time);
        
        if (!GameManager.Instance.IsGamePlayState()) yield break;
        try
        {
            GameManager.Instance.SetLevelCompleteState();
                
            //print("Coins Adding");
            GameCurrencyHandler.AddToCoins(50);

            if (_currentLevel > 1)
            {
                //Add Interstitial Ad here on Level Complete
            }
            
            //UIManager will handle the UI On Off Setting using event system if consfusion? visit it


            GameData.SetLevelAttemptRate(1);

            levelCompleteFuncEvent();

            SoundsManager.instance.PlayLevelWinSound(SoundsManager.instance.AS);


            CurrencyAnimationHandler.instance.RunGemAnimation(Vector3.zero, Vector3.zero, Vector3.one, Vector3.one);


        }
        catch (System.Exception e)
        {
            Debug.LogError(e.ToString());

        }
    }
}
