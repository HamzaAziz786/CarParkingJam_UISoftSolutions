using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCurrencyHandler : MonoBehaviour
{
    public static GameCurrencyHandler instance = null;
    [HideInInspector]
    public int Coins = 0;


    public delegate void _Delegate();
    public _Delegate Update_;


    public UIManager UI = null;
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
    public void AddToCoins(int amount)
    {
        //Coins = Coins + amount;
        //if (Coins < 0)
        //    Coins = 0;
        // GameData.instance.SetCoins(Coins);
        PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + amount);
    }

    public void UpdateUI()
    {
        UI.CoinText.text = "" + PlayerPrefs.GetInt("cash");

    }

}
