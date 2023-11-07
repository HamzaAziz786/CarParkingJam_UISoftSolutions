using System.Collections;
using UnityEngine;
using DG.Tweening;
public class LevelInfo : MonoBehaviour
{
    public DelegateEventScriptableObject Couner;

    public int Total_Cars;
    public int Car_Crossed;
    public GameObject Confettii;
    public GameObject[] Hurdles;
    public static LevelInfo instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void OnEnable()
    {
        Couner.Add_Count += Add;
    }
    private void OnDestroy()
    {
        Couner.Add_Count -= Add;
    }
    public void DisbaleHurdles()
    {
        for (int i = 0; i < Hurdles.Length; i++)
        {
            Hurdles[i].transform.DOScale(0, Random.Range(0.1f, .5f)).SetEase(Ease.OutBounce);
        }
    }
    public void EnableHurdles()
    {
        for (int i = 0; i < Hurdles.Length; i++)
        {
            //Hurdles[i].gameObject.SetActive(true);
            Hurdles[i].transform.DOScale(1, Random.Range(0.1f, .5f)).SetEase(Ease.OutBounce);
        }
    }
    private void Add(int num)
    {
        Car_Crossed += num;
        Level_Complete_Detection();
    }

    private void Level_Complete_Detection()
    {
        if (Car_Crossed != Total_Cars) return;
        print("Level Complete");
        StartCoroutine(LevelCompleteDelay());
        Instantiate(Confettii, new Vector3(1.7f, 64.9f, -26.3f), Quaternion.identity);
    }

    private static IEnumerator LevelCompleteDelay()
    {
        yield return new WaitForSecondsRealtime(1);
        LevelCompleteManager.Instance.LevelComplete(0);
    }
}
