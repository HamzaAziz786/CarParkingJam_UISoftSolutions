using UnityEngine;

public class GameCurrencyHandler : MonoBehaviour
{
    public static GameCurrencyHandler instance;
    [HideInInspector]
    public int Coins;


    public delegate void _Delegate();
    public _Delegate Update_;


    public UIManager UI;
    private void Awake()
    {
        instance = this;

        Coins = PlayerPrefs.GetInt("cash");
        
        Update_ += UpdateUI;

    }
    private void Start()
    {
        Update_();
    }
    public static void AddToCoins(int amount)
    {
        //Coins = Coins + amount;
        //if (Coins < 0)
        //    Coins = 0;
        // GameData.instance.SetCoins(Coins);
        PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + amount);
    }

    private void UpdateUI()
    {
        UI.CoinText.text = "" + PlayerPrefs.GetInt("cash");

    }

}
