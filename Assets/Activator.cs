
using UnityEngine;

public class Activator : MonoBehaviour
{
    public GameObject[] Objs;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            foreach (GameObject obj in Objs)
                obj.SetActive(true);
        }
    }
}
