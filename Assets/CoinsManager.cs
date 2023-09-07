using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{

    public Text Coins;
    public GameData _GameData;

    // Start is called before the first frame update
    void Start()
    {
        Coins.text = "" + _GameData.GetCoins();
    }
}
