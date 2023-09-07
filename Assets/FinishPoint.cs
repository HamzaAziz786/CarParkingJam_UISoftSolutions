using UnityEngine;
using System.Collections;

public class FinishPoint : MonoBehaviour
{
    [HideInInspector]public bool IsFinish;
    public static FinishPoint Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GamePlay_Manager.Instance.BtnClickSource.PlayOneShot(GamePlay_Manager.Instance.victoryClip);

            IsFinish = true;
            RCC_SceneManager.Instance.activePlayerVehicle.brakeInput = 1;
            GamePlay_Manager.Instance.HidePanels();
            StartCoroutine(Complete());
        }
    }

    IEnumerator Complete()
    {
        yield return new WaitForSeconds(3f);
        GamePlay_Manager.Instance.OnComplete();
    }
}
