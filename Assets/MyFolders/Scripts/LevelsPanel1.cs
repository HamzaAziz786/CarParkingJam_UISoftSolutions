using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelsPanel1 : MonoBehaviour
{
    public static LevelsPanel1 instance;
    public GameObject[] levels2;
    public GameObject[] lockedImg2;
    public Button nextBtn;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    private void OnEnable()
    {
        nextBtn.interactable = false;
        //loadingPanel.SetActive(true);
        
        foreach (GameObject obj in levels2)
            {
                obj.GetComponent<Button>().interactable = false;
            }
    }
   
    // Update is called once per frame
    void Update()
    {
            for (int i = 0; i <= PlayerPrefs.GetInt("levels2"); i++)
            {
                levels2[i].GetComponent<Button>().interactable = true;
            }
            for (int i = 0; i <= PlayerPrefs.GetInt("levels2"); i++)
            {
                lockedImg2[i].SetActive(false);
            }

        
    }

        
     
}
