using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CurrencyAnimationHandler : MonoBehaviour
{
    public GameObject CoinPrefab;
    public int CoinTotalCreation = 4;
    public Transform CoinParent;
    public Transform CoinTarget;
    private List<GameObject> Coins = new List<GameObject>();
    public static CurrencyAnimationHandler instance;
    public List<AudioSource> sound = new List<AudioSource>();
    public AudioClip clip;
    private void Awake()
    {
        instance = this;
        CreateGems();
    }

    private void CreateGems()
    {
        Coins.Clear();
        for (var i = 0; i < CoinTotalCreation; i++)
        {
            var obj = Instantiate(CoinPrefab, Vector3.zero, Quaternion.identity);
            Coins.Add(obj);
            obj.transform.SetParent(CoinParent);
            obj.gameObject.SetActive(false);
            //if (obj.GetComponent<AudioSource>() != null)
            //    sound.Add(obj.GetComponent<AudioSource>());
        }
    }


    public void RunGemAnimation(Vector3 initialPosition, Vector3 targetPos, Vector3 initialScale, Vector3 finalScale)
    {
        const float speedTime = 1f;
        float total = 0;
        const float ad = 0.04f;
        foreach (var t in Coins)
        {
            t.gameObject.SetActive(true);
            t.transform.SetParent(CoinParent);

            t.transform.localPosition = initialPosition + new Vector3(Random.Range(-50f, 50f), Random.Range(-50f, 50f), 0f);

            t.transform.DOPause();

            t.transform.SetParent(CoinTarget);

            t.transform.localScale = initialScale;
            t.transform.DOLocalMove(targetPos, speedTime).SetDelay(total).SetEase(Ease.Linear);
            t.transform.localEulerAngles = Vector3.zero;
            t.transform.DOScale(finalScale, speedTime).SetDelay(total).SetEase(Ease.Linear);
            StartCoroutine(OnOffObj(t.gameObject, false, speedTime + total));
            total += ad;
            //StartCoroutine(PlaySounds());
        }
        StartCoroutine(DelayUpdateUI(total + speedTime));


    }

    private IEnumerator PlaySounds()
    {
       //for(var i = 0; i < 10; i++)
       //{
           yield return new WaitForSeconds(0.1f);
           sound[0].PlayOneShot(clip);
       //}
    }

    private static IEnumerator OnOffObj(GameObject obj, bool value, float time)
    {
        yield return new WaitForSeconds(time);
        obj.gameObject.SetActive(value);
    }


    private static IEnumerator DelayUpdateUI(float time)
    {
        yield return new WaitForSeconds(time);
        if (GameCurrencyHandler.instance != null)
            GameCurrencyHandler.instance.Update_();
        
    }

}
