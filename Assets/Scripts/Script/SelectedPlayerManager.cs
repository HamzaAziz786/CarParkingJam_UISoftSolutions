using UnityEngine;

public class SelectedPlayerManager : MonoBehaviour
{
    public DelegateEventScriptableObject player_transfer;

    public PlayerMove SelectedCar;

    private void OnEnable()
    {
        player_transfer.player_transfer += Selected_Ball;
    }

    private void OnDestroy()
    {
        player_transfer.player_transfer -= Selected_Ball;
    }


    private void Selected_Ball(PlayerMove player)
    {
        SelectedCar = player;
    }
}
