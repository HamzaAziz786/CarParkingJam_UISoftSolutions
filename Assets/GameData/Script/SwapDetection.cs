using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapDetection : MonoBehaviour
{

    public static SwapDetection Instance;

    public DelegateEventScriptableObject player_transfer;
    Vector3 StartPos;
    public int pixelDistToDetect = 20;
    bool FingerDown;
    public PlayerMove SelectedPlayer;
    public bool PlayerCanSwap = true;



    private void OnEnable()
    {
        player_transfer.player_transfer += Selected_Car;
    }
    private void OnDestroy()
    {
        player_transfer.player_transfer -= Selected_Car;
    }

    private void Start()
    {
        Instance = this;
    }
    public void Selected_Car(PlayerMove player)
    {
        SelectedPlayer = player;
    }

    IEnumerator active_touch()
    {
        yield return new WaitForSecondsRealtime(0);
        PlayerCanSwap = true;
    }

    void Update()
    {

#if UNITY_EDITOR
        // For PC

        if (!FingerDown && Input.GetMouseButtonDown(0))
        {
            StartPos = Input.mousePosition;
            FingerDown = true;
        }

        if (FingerDown)
        {
            if (Input.mousePosition.y >= StartPos.y + pixelDistToDetect)
            {
                FingerDown = false;
                if (PlayerCanSwap)
                    if (SelectedPlayer != null)
                    {
                        if (SelectedPlayer.up_down)
                        {
                            SelectedPlayer.moveRight();
                        }
                        else
                        {
                            SelectedPlayer.moveUp();
                        }
                        PlayerCanSwap = false;
                        StartCoroutine(active_touch());
                    }

                //print("Swap up");
            }
            if (Input.mousePosition.y <= StartPos.y - pixelDistToDetect)
            {
                FingerDown = false;
                if (PlayerCanSwap)
                    if (SelectedPlayer != null)
                    {
                        if (SelectedPlayer.up_down)
                        {
                            SelectedPlayer.moveLeft();
                        }
                        else
                        {
                            SelectedPlayer.moveDown();
                        }
                        PlayerCanSwap = false;
                        StartCoroutine(active_touch());
                    }
                //print("Swap down");
            }
            if (Input.mousePosition.x >= StartPos.x + pixelDistToDetect)
            {
                FingerDown = false;
                if (PlayerCanSwap)
                    if (SelectedPlayer != null)
                    {
                        if (SelectedPlayer.left_right)
                        {
                            SelectedPlayer.moveRight();
                        }
                        else
                        {
                            SelectedPlayer.moveUp();
                        }
                        PlayerCanSwap = false;
                        StartCoroutine(active_touch());
                    }
                //print("Swap Right");
            }
            if (Input.mousePosition.x <= StartPos.x - pixelDistToDetect)
            {
                FingerDown = false;
                if (PlayerCanSwap)
                    if (SelectedPlayer != null)
                    {
                        if (SelectedPlayer.left_right)
                        {
                            SelectedPlayer.moveLeft();
                        }
                        else
                        {
                            SelectedPlayer.moveDown();
                        }
                        PlayerCanSwap = false;
                        StartCoroutine(active_touch());
                    }

                //print("Swap left");
            }
        }
#else

        // For Mobile

        if (!FingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            StartPos = Input.touches[0].position;
            FingerDown = true;
        }

        if (FingerDown)
        {
            if (Input.touches[0].position.y >= StartPos.y + pixelDistToDetect)
            {
                FingerDown = false;
                if(PlayerCanSwap)
                if (SelectedPlayer != null)
                {
                    if (SelectedPlayer.up_down)
                    {
                        SelectedPlayer.moveRight();
                    }
                    else
                    {
                        SelectedPlayer.moveUp();
                    }
                    PlayerCanSwap = false;
                        StartCoroutine(active_touch());
                }
                //print("Swap up");
            }
            else if (Input.touches[0].position.y <= StartPos.y - pixelDistToDetect)
            {
                FingerDown = false;
                if(PlayerCanSwap)
                if (SelectedPlayer != null)
                {
                    if (SelectedPlayer.up_down)
                    {
                        SelectedPlayer.moveLeft();
                    }
                    else
                    {
                        SelectedPlayer.moveDown();
                    }
                    PlayerCanSwap = false;
                        StartCoroutine(active_touch());
                }
                //print("Swap down");
            }
            else if (Input.touches[0].position.x >= StartPos.x + pixelDistToDetect)
            {
                FingerDown = false;
                if(PlayerCanSwap)
                if (SelectedPlayer != null)
                {
                    if (SelectedPlayer.left_right)
                    {
                        SelectedPlayer.moveRight();
                    }
                    else
                    {
                        SelectedPlayer.moveUp();
                    }
                    PlayerCanSwap = false;
                        StartCoroutine(active_touch());
                }
                //print("Swap right");
            }
            else if (Input.touches[0].position.x <= StartPos.x - pixelDistToDetect)
            {
                FingerDown = false;
                if(PlayerCanSwap)
                if (SelectedPlayer != null)
                {
                    if (SelectedPlayer.left_right)
                    {
                        SelectedPlayer.moveLeft();
                    }
                    else
                    {
                        SelectedPlayer.moveDown();
                    }
                    PlayerCanSwap = false;
                        StartCoroutine(active_touch());
                }
                //print("Swap left");
            }
        }

#endif


    }

}
