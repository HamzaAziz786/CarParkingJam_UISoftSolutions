using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{

    public Text Coins;
    public GameData _GameData;

    // Start is called before the first frame update
    private void Start()
    {
        Coins.text = "" + GameData.GetCoins();
    }
}
