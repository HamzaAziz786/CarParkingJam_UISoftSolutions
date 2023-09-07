using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class
    LevelsPanel : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject[] lockedImg;
    public Transform Content;
    //public Button nextBtn;
    //public GameObject loadingPanel;
    public static LevelsPanel instance;
    GameObject[] Shines;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;



        Shines = new GameObject[levels.Length];

        for (int i = 0; i < levels.Length; i++)
        {
            Shines[i] = levels[i].transform.GetChild(2).gameObject;
        }

        SelectLevel();

    }

    public void ContentPosition()
    {
        if (PlayerPrefs.GetInt("levels") < 3)
        {
            Content.localPosition = new Vector3(0f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels") >= 3 && PlayerPrefs.GetInt("levels") < 5)
        {
            Content.localPosition = new Vector3(-750f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels") >= 5 && PlayerPrefs.GetInt("levels") < 7)
        {
            Content.localPosition = new Vector3(-1150f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels") >= 7 && PlayerPrefs.GetInt("levels") < 9)
        {
            Content.localPosition = new Vector3(-2000f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels") >= 9 && PlayerPrefs.GetInt("levels") < 11)
        {
            Content.localPosition = new Vector3(-2700f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels") >= 11 && PlayerPrefs.GetInt("levels") < 13)
        {
            Content.localPosition = new Vector3(-3500f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels") >= 13 && PlayerPrefs.GetInt("levels") <= 14)
        {
            Content.localPosition = new Vector3(-4100f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels") >= 15 && PlayerPrefs.GetInt("levels") <= 16)
        {
            Content.localPosition = new Vector3(-4700f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels") >= 17 && PlayerPrefs.GetInt("levels") <= 18)
        {
            Content.localPosition = new Vector3(-5300f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels") >= 19 && PlayerPrefs.GetInt("levels") <= 20)
        {
            Content.localPosition = new Vector3(-6000f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels") >= 21 && PlayerPrefs.GetInt("levels") <= 22)
        {
            Content.localPosition = new Vector3(-6500f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels") >= 23 && PlayerPrefs.GetInt("levels") <= 24)
        {
            Content.localPosition = new Vector3(-7000f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels") >= 25 && PlayerPrefs.GetInt("levels") <= 26)
        {
            Content.localPosition = new Vector3(-7500f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels") >= 27 && PlayerPrefs.GetInt("levels") <= 28)
        {
            Content.localPosition = new Vector3(-8100f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels") >= 29)
        {
            Content.localPosition = new Vector3(-9000f, 0f, 0f);
        }
    }

    private void OnEnable()
    {
        //nextBtn.interactable = false;
        foreach (GameObject obj in levels)
        {
            obj.GetComponent<Button>().interactable = false;
        }

        ContentPosition();
    }
    private void OnDisable()
    {
        //nextBtn.interactable = false;

        foreach (GameObject obj in Shines)
            obj.SetActive(false);
    }

    public void SelectLevel()
    {
        foreach (GameObject obj in Shines)
            obj.SetActive(false);

        Shines[PlayerPrefs.GetInt("levels")].SetActive(true);
        //nextBtn.interactable = true;
    }

    void Update()
    {
        for (int i = 0; i <= PlayerPrefs.GetInt("levels"); i++)
        {
            levels[i].GetComponent<Button>().interactable = true;
        }
        for (int i = 0; i <= PlayerPrefs.GetInt("levels"); i++)
        {
            lockedImg[i].SetActive(false);
        }
    }
}
