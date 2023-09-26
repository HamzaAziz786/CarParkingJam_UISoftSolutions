using UnityEngine;
using SWS;

public class Hurdel : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        collision.rigidbody.isKinematic = true;
        gameObject.GetComponent<splineMove>().Stop();

        GameOverManager.Instance.GameOver(1f);
    }
}
