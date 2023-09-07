using System.Collections;
using UnityEngine;

public class Collision_Handler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Hurdle"))
        {
            GamePlay_Manager.Instance.BtnClickSource.PlayOneShot(GamePlay_Manager.Instance.AlarmClip);
            GamePlay_Manager.Instance.HidePanels();
            StartCoroutine(Failed());
        }
    }

    IEnumerator Failed()
    {
        yield return new WaitForSeconds(2f);
        GamePlay_Manager.Instance.OnFail();
    }
}
