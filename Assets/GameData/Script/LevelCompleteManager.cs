using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteManager : MonoBehaviour
{
    public static LevelCompleteManager Instence;

    public delegate void LevelCompleteFunc();
    public LevelCompleteFunc levelCompleteFuncEvent;

    int CurrentLevel;

    private Coroutine LevelCompleteCor = null;

    private void OnEnable()
    {
        Instence = this;
    }
    private void OnDestroy()
    {
        LevelManager.instance.levelCreateFuncEvent -= SetGamePlayLevelNumberText;
    }
    private void Start()
    {
        LevelManager.instance.levelCreateFuncEvent += SetGamePlayLevelNumberText;
    }
    void SetGamePlayLevelNumberText(int Level)
    {
        CurrentLevel = Level;
    }
    public void LevelComplete(float time)
    {

        if (LevelCompleteCor != null)
            this.StopCoroutine(LevelCompleteCor);
        LevelCompleteCor = StartCoroutine(DelayLevelComplete(time));


    }


    IEnumerator DelayLevelComplete(float time)
    {


        yield return new WaitForSeconds(time);


        if (GameManager.instance.IsGamePlayState())
        {



            try
            {
                GameManager.instance.SetLevelCompleteState();

               // AdsManager.Instance.ShowInterstitialLoading();
                //print("Coins Adding");
                GameCurrencyHandler.instance.AddToCoins(50);

                if (CurrentLevel > 1)
                {
                    //Add Interstitial Ad here on Level Complete
                }




                //UIManager will handle the UI On Off Setting using event system if consfusion? visit it


                GameData.instance.SetLevelAttemptRate(1);

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
}
