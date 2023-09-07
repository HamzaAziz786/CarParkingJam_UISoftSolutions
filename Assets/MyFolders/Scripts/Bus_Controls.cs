using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bus_Controls : MonoBehaviour
{
    //[Space(5)]
    //[Header("Engine Objs")]
    //public Text EngineTxt;
    //public Image EngineImg;
    //public GameObject ClickImg;

    //[Space(5)]
    //[Header("Menu Objs")]
    //public GameObject MenuButtons;
    //public GameObject UpArrowBtn;
    //public Text speedTxt;
    //public Image FuelFillImg;

    //[Space(5)]
    //[Header("Passengers Objs")]
    //public GameObject BgImg;
    //public Image MoodImg;
    //public Image HungerImg;
    //public Text MoodTxt;
    //public Text HungerTxt;
    //public Sprite HappyImg, ScaredImg, SadImg, AngerImg;
    //[HideInInspector]public float mood, hunger;

    //public GameObject DoorClickAnim;
    ////public GameObject SpeedoMeter;
    //public GameObject UiMirrorObj;
    //GameObject SideMirror;
    //Animator WipersAnim;
    //Animator DoorAnim;
    //AudioSource HornSrc;
    [HideInInspector]public bool IsLeft, IsRight;
    public static Bus_Controls instance;
    [HideInInspector] public bool isDoorOpen;

    private void Awake()
    {
        instance = this;

        //mood = hunger = 100;

        StartCoroutine(UpdateMoodImgColour());

    }

    IEnumerator UpdateMoodImgColour()
    {
        yield return new WaitForSeconds(1);

        //if (GamePlay.instance.PassengerObj.activeSelf)
        //{
        //    if (mood >= 75)
        //        MoodImg.sprite = HappyImg;
        //    else if (mood < 75 && mood > 50)
        //        MoodImg.sprite = ScaredImg;
        //    else if (mood < 50 && mood > 25)
        //        MoodImg.sprite = SadImg;
        //    else
        //        MoodImg.sprite = AngerImg;

        //    if (hunger >= 75)
        //        HungerImg.color = Color.green;
        //    else if (hunger < 75 && hunger > 50)
        //        HungerImg.color = Color.yellow;
        //    else
        //        HungerImg.color = Color.red;

        //}

        //if(SideMirror.activeSelf)
        //{
        //    if (GamePlay.instance.RccCam.cameraMode == RCC_Camera.CameraMode.WHEEL)
        //    {
        //        UiMirrorObj.SetActive(false);
        //    }else
        //    {
        //        UiMirrorObj.SetActive(true);
        //    }
        //}

        if (RCC_SceneManager.Instance.activePlayerVehicle.speed > 10)
        {
            RCC_SceneManager.Instance.activePlayerVehicle.fuelConsumptionRate = RCC_SceneManager.Instance.activePlayerVehicle.speed / 250;
        }

        //if (RCC_SceneManager.Instance.activePlayerVehicle.speed > 15)
        //{
        //    EngineImg.transform.parent.gameObject.SetActive(false);
        //}else
        //{
        //    EngineImg.transform.parent.gameObject.SetActive(true);
        //}


        StartCoroutine(UpdateMoodImgColour());
    }

    private void Update()
    {
        if (RCC_SceneManager.Instance.activePlayerVehicle != null)
        {
            //FuelFillImg.fillAmount = (RCC_SceneManager.Instance.activePlayerVehicle.fuelTank / RCC_SceneManager.Instance.activePlayerVehicle.fuelTankCapacity);

            //speedTxt.text = "" + RCC_SceneManager.Instance.activePlayerVehicle.speed.ToString("0");

            //if (RCC_SceneManager.Instance.activePlayerVehicle.speed > 100)
            //    mood = Mathf.Lerp(mood, 0, Time.deltaTime / 1000);
            //else if(RCC_SceneManager.Instance.activePlayerVehicle.speed > 120)
            //    mood = Mathf.Lerp(mood, 0, Time.deltaTime / 900);
            //else if (RCC_SceneManager.Instance.activePlayerVehicle.speed > 150)
            //    mood = Mathf.Lerp(mood, 0, Time.deltaTime / 700);

        }

        //if (GamePlay.instance.PassengerObj.activeSelf)
        //    hunger = Mathf.Lerp(hunger, 0, Time.deltaTime / 2000);

        //MoodTxt.text = mood.ToString("0") + "%";
        //HungerTxt.text = hunger.ToString("0") + "%";
    }

    //public void UpArrowBtnClick()
    //{
    //    //GamePlay.instance.BtnClickSound();
    //    UpArrowBtn.SetActive(false);
    //    MenuButtons.SetActive(true);
    //    SpeedoMeter.SetActive(false);
    //}
    //public void DownArrowBtnClick()
    //{
    //    //GamePlay.instance.BtnClickSound();
    //    UpArrowBtn.SetActive(true);
    //    MenuButtons.SetActive(false);
    //    SpeedoMeter.SetActive(true);
    //}

    //public void OnPassengerBtnClick()
    //{
    //    GamePlay.instance.BtnClickSound();
    //    if (!BgImg.activeSelf)
    //        BgImg.SetActive(true);
    //    else
    //        BgImg.SetActive(false);
    //}

    public void LeftIndecator()
    {
        //GamePlay.instance.BtnClickSound();
        if (IsLeft && IsRight)
            IsRight = false;
        else
        {
            IsRight = false;
            if (IsLeft)
                IsLeft = false;
            else
                IsLeft = true;
        }
    }
    public void RightIndecator()
    {
        //GamePlay.instance.BtnClickSound();
        if (IsLeft && IsRight)
            IsLeft = false;
        else
        {
            IsLeft = false;
            if (IsRight)
                IsRight = false;
            else
                IsRight = true;
        }
    }
    public void HazardLights()
    {
        //GamePlay.instance.BtnClickSound();
        if (IsRight && IsLeft)
        {
            IsRight = false;
            IsLeft = false;
        }
        else
        {
            IsRight = true;
            IsLeft = true;
        }
    }
    //public void SideMirrorFunc()
    //{
    //    GamePlay.instance.BtnClickSound();
    //    if (SideMirror.activeSelf)
    //    {
    //        SideMirror.SetActive(false);
    //        UiMirrorObj.SetActive(false);
    //    }
    //    else
    //    {
    //        SideMirror.SetActive(true);

    //        if (GamePlay.instance.RccCam.cameraMode == RCC_Camera.CameraMode.WHEEL)
    //        {
    //            UiMirrorObj.SetActive(false);
    //        }
    //        else
    //        {
    //            UiMirrorObj.SetActive(true);
    //        }
    //    }

    //}
    // Start is called before the first frame update
    void Start()
    {

        //WipersAnim = GameObject.FindGameObjectWithTag("WiperAnim").GetComponent<Animator>();
        //DoorAnim = GameObject.FindGameObjectWithTag("DoorAnim").GetComponent<Animator>();
        //HornSrc = GameObject.FindGameObjectWithTag("Horn").GetComponent<AudioSource>();
        //SideMirror = GameObject.FindGameObjectWithTag("SideMirror").transform.GetChild(0).gameObject;


    }

    //public void HornFunc()
    //{
    //    if (!HornSrc.isPlaying)
    //        HornSrc.Play();
    //}

    //public void WipersFun()
    //{
    //    //GamePlay.instance.BtnClickSound();
    //    //if (!WipersAnim.GetBool("WiperOn"))
    //    //{
    //    //    WipersAnim.SetBool("WiperOn", true);// = true;
    //    //}else
    //    //{
    //    //    WipersAnim.SetBool("WiperOn", false);
    //    //}
    //}

    //public void DoorFun()
    //{
    //    //GamePlay.instance.BtnClickSound();

    //    if (!DoorAnim.GetBool("DoorOpen"))
    //    {
    //        isDoorOpen = true;
    //        DoorAnim.SetBool("DoorOpen", true);// = true;
    //    }

    //    else
    //    {
    //        isDoorOpen = false;
    //        DoorAnim.SetBool("DoorOpen", false);
    //    }
    //}

    //public void EngineOnOff()
    //{
    //    GamePlay.instance.BtnClickSound();

    //    if (GamePlay.instance.MyPlayer.engineRunning)
    //    {
    //        EngineTxt.text = "STOP";
    //        EngineImg.color = Color.red;
    //        GamePlay.instance.MyPlayer.engineRunning = false;
    //        //EngineImg.transform.parent.GetComponent<Animator>().enabled = true;
    //        ClickImg.SetActive(true);
    //    }
    //    else
    //    {
    //        if (!Fuel_Handler.instance.startFilling)
    //        {
    //            EngineTxt.text = "START";
    //            EngineImg.color = Color.green;
    //            GamePlay.instance.MyPlayer.engineRunning = true;
    //            //EngineImg.transform.parent.GetComponent<Animator>().enabled = false;
    //            ClickImg.SetActive(false);

    //            isDoorOpen = false;
    //            DoorAnim.SetBool("DoorOpen", false);
    //        }
    //    }
    //}

}
