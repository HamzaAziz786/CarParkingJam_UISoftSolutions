using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsPanel_Driving : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject[] lockedImg;
    public Transform Content;
    //public Button nextBtn;
    //public GameObject loadingPanel;
    public static LevelsPanel_Driving instance;
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
        if (PlayerPrefs.GetInt("levels2") < 3)
        {
            Content.localPosition = new Vector3(0f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels2") >= 3 && PlayerPrefs.GetInt("levels2") < 5)
        {
            Content.localPosition = new Vector3(-750f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels2") >= 5 && PlayerPrefs.GetInt("levels2") < 7)
        {
            Content.localPosition = new Vector3(-1150f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels2") >= 7 && PlayerPrefs.GetInt("levels2") < 9)
        {
            Content.localPosition = new Vector3(-2000f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels2") >= 9 && PlayerPrefs.GetInt("levels2") < 11)
        {
            Content.localPosition = new Vector3(-2700f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels2") >= 11 && PlayerPrefs.GetInt("levels2") < 13)
        {
            Content.localPosition = new Vector3(-3500f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels2") >= 13 && PlayerPrefs.GetInt("levels2") <= 14)
        {
            Content.localPosition = new Vector3(-4100f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels2") >= 15 && PlayerPrefs.GetInt("levels2") <= 16)
        {
            Content.localPosition = new Vector3(-4700f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels2") >= 17 && PlayerPrefs.GetInt("levels2") <= 18)
        {
            Content.localPosition = new Vector3(-5300f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels2") >= 19 && PlayerPrefs.GetInt("levels2") <= 20)
        {
            Content.localPosition = new Vector3(-6000f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels2") >= 21 && PlayerPrefs.GetInt("levels2") <= 22)
        {
            Content.localPosition = new Vector3(-6500f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels2") >= 23 && PlayerPrefs.GetInt("levels2") <= 24)
        {
            Content.localPosition = new Vector3(-7000f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels2") >= 25 && PlayerPrefs.GetInt("levels2") <= 26)
        {
            Content.localPosition = new Vector3(-7500f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels2") >= 27 && PlayerPrefs.GetInt("levels2") <= 28)
        {
            Content.localPosition = new Vector3(-8100f, 0f, 0f);
        }
        else if (PlayerPrefs.GetInt("levels2") >= 29)
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

        //ContentPosition();
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

        Shines[PlayerPrefs.GetInt("levels2")].SetActive(true);
        //nextBtn.interactable = true;
    }

    void Update()
    {
        for (int i = 0; i <= PlayerPrefs.GetInt("levels2"); i++)
        {
            levels[i].GetComponent<Button>().interactable = true;
        }
        for (int i = 0; i <= PlayerPrefs.GetInt("levels2"); i++)
        {
            lockedImg[i].SetActive(false);
        }
    }
}
