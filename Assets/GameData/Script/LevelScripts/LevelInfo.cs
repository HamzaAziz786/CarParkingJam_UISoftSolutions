using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelInfo : MonoBehaviour
{
    public DelegateEventScriptableObject Couner;

    public int Total_Cars;
    public int Car_Crossed;
    public GameObject Confettii;

    private void OnEnable()
    {
        Couner.Add_Count += add;
    }
    private void OnDestroy()
    {
        Couner.Add_Count -= add;
    }

    void add(int num)
    {
        Car_Crossed += num;
        Level_Complete_Detection();
    }

    void Level_Complete_Detection()
    {
        if(Car_Crossed == Total_Cars)
        {
            print("Level Complete");
            StartCoroutine(LevelCompleteDelay());
            Instantiate(Confettii, new Vector3(1.7f, 64.9f, -26.3f), Quaternion.identity);

        }
    }

    IEnumerator LevelCompleteDelay()
    {
        yield return new WaitForSecondsRealtime(1);
        LevelCompleteManager.Instence.LevelComplete(0);
    }
}
