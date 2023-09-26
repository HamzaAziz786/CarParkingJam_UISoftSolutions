using UnityEngine;

public class CarCounter : MonoBehaviour
{
    public DelegateEventScriptableObject Counter;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Destroy(other.gameObject);
        Counter.Add_Count(1);
        SwapDetection.Instance.PlayerCanSwap = true;
    }
}
