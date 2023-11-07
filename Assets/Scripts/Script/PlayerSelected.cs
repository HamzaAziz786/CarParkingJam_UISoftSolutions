using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using DG.Tweening;
public class PlayerSelected : MonoBehaviour
{
    public DelegateEventScriptableObject player_transfer;
    public PlayerMove player;
    
    public void SelectedPlayer()
    {
        // player.Car.mass = 1;
        if (GameManager.Instance.IsDestroyCar)
        {
            player.transform.DOScale(0, .3f).SetEase(Ease.OutBounce);
            //Destroy(player);
            GameManager.Instance.IsDestroyCar = false;
           CarCounter.instance.Counter.Add_Count(1);
            SwapDetection.Instance.PlayerCanSwap = true;
        }
        player.Car.isKinematic = false;
        player_transfer.player_transfer(player);
    }
    public void DeselectedPlayer()
    {
        player_transfer.player_transfer(null);
    }
}
