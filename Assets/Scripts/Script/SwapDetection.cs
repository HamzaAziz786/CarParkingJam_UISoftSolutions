using System.Collections;
using UnityEngine;

public class SwapDetection : MonoBehaviour
{
    public static SwapDetection Instance;

    private Vector3 _startPos;
    private bool _fingerDown;

    public DelegateEventScriptableObject player_transfer;
    public int pixelDistToDetect = 20;
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

    private void Selected_Car(PlayerMove player)
    {
        SelectedPlayer = player;
    }

    private IEnumerator active_touch()
    {
        yield return new WaitForSecondsRealtime(0);
        PlayerCanSwap = true;
    }

    private void Update()
    {

#if UNITY_EDITOR
        // For PC

        if (!_fingerDown && Input.GetMouseButtonDown(0))
        {
            _startPos = Input.mousePosition;
            _fingerDown = true;
        }

        if (!_fingerDown) return;
        if (Input.mousePosition.y >= _startPos.y + pixelDistToDetect)
        {
            _fingerDown = false;
            if (PlayerCanSwap)
                if (SelectedPlayer != null)
                {
                    if (SelectedPlayer.up_down)
                    {
                        SelectedPlayer.MoveRight();
                    }
                    else
                    {
                        SelectedPlayer.MoveUp();
                    }
                    PlayerCanSwap = false;
                    StartCoroutine(active_touch());
                }

            //print("Swap up");
        }
        if (Input.mousePosition.y <= _startPos.y - pixelDistToDetect)
        {
            _fingerDown = false;
            if (PlayerCanSwap)
                if (SelectedPlayer != null)
                {
                    if (SelectedPlayer.up_down)
                    {
                        SelectedPlayer.MoveLeft();
                    }
                    else
                    {
                        SelectedPlayer.MoveDown();
                    }
                    PlayerCanSwap = false;
                    StartCoroutine(active_touch());
                }
            //print("Swap down");
        }
        if (Input.mousePosition.x >= _startPos.x + pixelDistToDetect)
        {
            _fingerDown = false;
            if (PlayerCanSwap)
                if (SelectedPlayer != null)
                {
                    if (SelectedPlayer.left_right)
                    {
                        SelectedPlayer.MoveRight();
                    }
                    else
                    {
                        SelectedPlayer.MoveUp();
                    }
                    PlayerCanSwap = false;
                    StartCoroutine(active_touch());
                }
        }

        if (!(Input.mousePosition.x <= _startPos.x - pixelDistToDetect)) return;
        _fingerDown = false;
        if (!PlayerCanSwap) return;
        if (SelectedPlayer == null) return;
        if (SelectedPlayer.left_right)
        {
            SelectedPlayer.MoveLeft();
        }
        else
        {
            SelectedPlayer.MoveDown();
        }
        PlayerCanSwap = false;
        StartCoroutine(active_touch());

        //print("Swap left");
#else

        // For Mobile

        if (!_fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            _startPos = Input.touches[0].position;
            _fingerDown = true;
        }

        if (_fingerDown)
        {
            if (Input.touches[0].position.y >= _startPos.y + pixelDistToDetect)
            {
                _fingerDown = false;
                if(PlayerCanSwap)
                if (SelectedPlayer != null)
                {
                    if (SelectedPlayer.up_down)
                    {
                        SelectedPlayer.MoveRight();
                    }
                    else
                    {
                        SelectedPlayer.MoveUp();
                    }
                    PlayerCanSwap = false;
                        StartCoroutine(active_touch());
                }
                //print("Swap up");
            }
            else if (Input.touches[0].position.y <= _startPos.y - pixelDistToDetect)
            {
                _fingerDown = false;
                if(PlayerCanSwap)
                if (SelectedPlayer != null)
                {
                    if (SelectedPlayer.up_down)
                    {
                        SelectedPlayer.MoveLeft();
                    }
                    else
                    {
                        SelectedPlayer.MoveDown();
                    }
                    PlayerCanSwap = false;
                        StartCoroutine(active_touch());
                }
                //print("Swap down");
            }
            else if (Input.touches[0].position.x >= _startPos.x + pixelDistToDetect)
            {
                _fingerDown = false;
                if(PlayerCanSwap)
                if (SelectedPlayer != null)
                {
                    if (SelectedPlayer.left_right)
                    {
                        SelectedPlayer.MoveRight();
                    }
                    else
                    {
                        SelectedPlayer.MoveUp();
                    }
                    PlayerCanSwap = false;
                        StartCoroutine(active_touch());
                }
                //print("Swap right");
            }
            else if (Input.touches[0].position.x <= _startPos.x - pixelDistToDetect)
            {
                _fingerDown = false;
                if(PlayerCanSwap)
                if (SelectedPlayer != null)
                {
                    if (SelectedPlayer.left_right)
                    {
                        SelectedPlayer.MoveLeft();
                    }
                    else
                    {
                        SelectedPlayer.MoveDown();
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
