using UnityEngine;

public class CarDetector : MonoBehaviour
{
    public int waypointNum;
    public PlayerMove player;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        player = other.GetComponent<PlayerMove>();
        player.moving = true;
        player.OnRoadDetection.SetActive(true);
        //SoundsManager.instance.RoadFlip(SoundsManager.instance.AS);

        player.emojiCool.Play();
        player._Waypoint.startPoint = waypointNum;
        player._Waypoint.StartMove();
        other.GetComponent<Rigidbody>().isKinematic = true;
    }

}
