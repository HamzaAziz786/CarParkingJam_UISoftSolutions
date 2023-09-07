using UnityEngine;
using UnityEngine.UI;
public class Store_Script : MonoBehaviour
{

    public GameObject[] players;
    public GameObject startBtn, buyBtn;
    public Text price;
    public GameObject WatchVideoBtn;

    [HideInInspector] public int num;
    public static Store_Script instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

    }

    private void OnEnable()
    {

        hidePlayers();
        players[PlayerPrefs.GetInt("currentPlayer")].SetActive(true);

        num = PlayerPrefs.GetInt("currentPlayer");
        buyBtn.SetActive(false);
        startBtn.SetActive(true);
        WatchVideoBtn.SetActive(false);
    }

    private void OnDisable()
    {
        hidePlayers();
    }

    void hidePlayers()
    {
        foreach (GameObject ply in players)
        {
            ply.SetActive(false);
        }

    }

    public void onNext()
    {

        MenuManager.instance.BtnClickSound();

        if (num == 0)
        {
            hidePlayers();
            players[1].SetActive(true);


            if (PlayerPrefs.GetInt("car1") != 1)
            {
                buyBtn.SetActive(true);
                startBtn.SetActive(false);
                price.text = "$" + 2000;
                WatchVideoBtn.SetActive(true);
            }
            else
            {
                WatchVideoBtn.SetActive(false);
                buyBtn.SetActive(false);
                startBtn.SetActive(true);
                PlayerPrefs.SetInt("currentPlayer", 1);
            }

        }
        else if (num == 1)
        {
            hidePlayers();
            players[2].SetActive(true);


            if (PlayerPrefs.GetInt("car2") != 1)
            {
                buyBtn.SetActive(true);
                startBtn.SetActive(false);
                price.text = "$" + 5000;
                WatchVideoBtn.SetActive(true);
            }
            else
            {
                WatchVideoBtn.SetActive(false);
                buyBtn.SetActive(false);
                startBtn.SetActive(true);
                PlayerPrefs.SetInt("currentPlayer", 2);
            }
        }
        else if (num == 2)
        {
            hidePlayers();
            players[3].SetActive(true);


            if (PlayerPrefs.GetInt("car3") != 1)
            {
                buyBtn.SetActive(true);
                startBtn.SetActive(false);
                price.text = "$" + 7000;
                WatchVideoBtn.SetActive(true);
            }
            else
            {
                WatchVideoBtn.SetActive(false);
                buyBtn.SetActive(false);
                startBtn.SetActive(true);
                PlayerPrefs.SetInt("currentPlayer", 3);
            }
        }
        else if (num == 3)
        {
            hidePlayers();
            players[4].SetActive(true);


            if (PlayerPrefs.GetInt("car4") != 1)
            {
                buyBtn.SetActive(true);
                startBtn.SetActive(false);
                price.text = "$" + 10000;
                WatchVideoBtn.SetActive(true);
            }
            else
            {
                WatchVideoBtn.SetActive(false);
                buyBtn.SetActive(false);
                startBtn.SetActive(true);
                PlayerPrefs.SetInt("currentPlayer", 4);
            }
        }
        else if (num == 4)
        {
            hidePlayers();
            players[5].SetActive(true);


            if (PlayerPrefs.GetInt("car4") != 1)
            {
                buyBtn.SetActive(true);
                startBtn.SetActive(false);
                price.text = "$" + 15000;
                WatchVideoBtn.SetActive(true);
            }
            else
            {
                WatchVideoBtn.SetActive(false);
                buyBtn.SetActive(false);
                startBtn.SetActive(true);
                PlayerPrefs.SetInt("currentPlayer", 5);
            }
        }


        else if (num == 5)
        {
            hidePlayers();
            players[0].SetActive(true);


            buyBtn.SetActive(false);
            WatchVideoBtn.SetActive(false);
            startBtn.SetActive(true);
            PlayerPrefs.SetInt("currentPlayer", 0);
        }

        if (num < 5)
        {
            num++;
        }
        else
        {
            num = 0;
        }
    }
    public void onPrev()
    {
        MenuManager.instance.BtnClickSound();
        if (num == 5)
        {
            hidePlayers();
            players[4].SetActive(true);

            if (PlayerPrefs.GetInt("car4") != 1)
            {
                buyBtn.SetActive(true);
                startBtn.SetActive(false);
                price.text = "$" + 10000;
                WatchVideoBtn.SetActive(true);
            }
            else
            {
                WatchVideoBtn.SetActive(false);
                buyBtn.SetActive(false);
                startBtn.SetActive(true);
                PlayerPrefs.SetInt("currentPlayer", 4);
            }

        }
        else if (num == 4)
        {
            hidePlayers();
            players[3].SetActive(true);

            if (PlayerPrefs.GetInt("car3") != 1)
            {
                buyBtn.SetActive(true);
                startBtn.SetActive(false);
                price.text = "$" + 7000;
                WatchVideoBtn.SetActive(true);
            }
            else
            {
                WatchVideoBtn.SetActive(false);
                buyBtn.SetActive(false);
                startBtn.SetActive(true);
                PlayerPrefs.SetInt("currentPlayer", 3);
            }

        }
        else if (num == 3)
        {
            hidePlayers();
            players[2].SetActive(true);

            if (PlayerPrefs.GetInt("car2") != 1)
            {
                buyBtn.SetActive(true);
                startBtn.SetActive(false);
                price.text = "$" + 5000;
                WatchVideoBtn.SetActive(true);
            }
            else
            {
                WatchVideoBtn.SetActive(false);
                buyBtn.SetActive(false);
                startBtn.SetActive(true);
                PlayerPrefs.SetInt("currentPlayer", 2);
            }

        }
        else if (num == 2)
        {
            hidePlayers();
            players[1].SetActive(true);
            if (PlayerPrefs.GetInt("car1") != 1)
            {
                buyBtn.SetActive(true);
                startBtn.SetActive(false);
                price.text = "$" + 2000;
                WatchVideoBtn.SetActive(true);
            }
            else
            {
                WatchVideoBtn.SetActive(false);
                buyBtn.SetActive(false);
                startBtn.SetActive(true);
                PlayerPrefs.SetInt("currentPlayer", 1);
            }

        }
        else if (num == 1)
        {
            hidePlayers();
            players[0].SetActive(true);
            WatchVideoBtn.SetActive(false);
            buyBtn.SetActive(false);
            startBtn.SetActive(true);
            PlayerPrefs.SetInt("currentPlayer", 0);

        }
        else if (num == 0)
        {
            hidePlayers();
            players[5].SetActive(true);
            if (PlayerPrefs.GetInt("car5") != 1)
            {
                buyBtn.SetActive(true);
                startBtn.SetActive(false);
                price.text = "$" + 15000;
                WatchVideoBtn.SetActive(true);
            }
            else
            {
                WatchVideoBtn.SetActive(false);
                buyBtn.SetActive(false);
                startBtn.SetActive(true);
                PlayerPrefs.SetInt("currentPlayer", 5);
            }

        }

        if (num > 0)
        {
            num--;
        }
        else
        {
            num = 5;
        }
    }

    public void onPurchase()
    {
        MenuManager.instance.BtnClickSound();

        if (num == 1)
        {
            if (PlayerPrefs.GetInt("cash") >= 2000)
            {
                buyBtn.SetActive(false);
                PlayerPrefs.SetInt("car1", 1);
                PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") - 2000);
                startBtn.SetActive(true);
                PlayerPrefs.SetInt("currentPlayer", 1);
                WatchVideoBtn.SetActive(false);
            }

        }
        else if (num == 2)
        {
            if (PlayerPrefs.GetInt("cash") >= 5000)
            {
                buyBtn.SetActive(false);
                PlayerPrefs.SetInt("car2", 1);
                PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") - 5000);
                startBtn.SetActive(true);
                PlayerPrefs.SetInt("currentPlayer", 2);
                WatchVideoBtn.SetActive(false);
            }
        }
        else if (num == 3)
        {
            if (PlayerPrefs.GetInt("cash") >= 7000)
            {
                buyBtn.SetActive(false);
                PlayerPrefs.SetInt("car3", 1);
                PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") - 7000);
                startBtn.SetActive(true);
                PlayerPrefs.SetInt("currentPlayer", 3);
                WatchVideoBtn.SetActive(false);

            }
        }
        else if (num == 4)
        {
            if (PlayerPrefs.GetInt("cash") >= 10000)
            {
                buyBtn.SetActive(false);
                PlayerPrefs.SetInt("car4", 1);
                PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") - 10000);
                startBtn.SetActive(true);
                PlayerPrefs.SetInt("currentPlayer", 4);
                WatchVideoBtn.SetActive(false);

            }
        }
        else if (num == 5)
        {
            if (PlayerPrefs.GetInt("cash") >= 15000)
            {
                buyBtn.SetActive(false);
                PlayerPrefs.SetInt("car5", 1);
                PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") - 15000);
                startBtn.SetActive(true);
                PlayerPrefs.SetInt("currentPlayer", 5);
                WatchVideoBtn.SetActive(false);

            }
        }


    }


}
