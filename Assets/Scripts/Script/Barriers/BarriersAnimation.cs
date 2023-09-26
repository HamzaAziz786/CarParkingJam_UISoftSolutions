using UnityEngine;

public class BarriersAnimation : MonoBehaviour
{
    [SerializeField] private Animator anim1, anim2;
    private static readonly int IsOpen = Animator.StringToHash("isOpen");

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player")) return;
        anim1.SetBool(IsOpen, true);
        anim2.SetBool(IsOpen, true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.transform.CompareTag("Player")) return;
        anim1.SetBool(IsOpen, false);
        anim2.SetBool(IsOpen, false);
    }
}
