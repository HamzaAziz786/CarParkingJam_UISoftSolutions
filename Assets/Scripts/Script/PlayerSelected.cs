using UnityEngine;

public class PlayerSelected : MonoBehaviour
{
    public DelegateEventScriptableObject player_transfer;
    public PlayerMove player;
    
    public void SelectedPlayer()
    {
        player.Car.mass = 1;
        player.Car.isKinematic = false;
        player_transfer.player_transfer(player);
    }
    public void DeselectedPlayer()
    {
        player_transfer.player_transfer(null);
    }
}
