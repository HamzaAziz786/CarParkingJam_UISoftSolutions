using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Header("CurrencyUI")]
    public RectTransform CoinTextParent;
    public Text CoinText;

    [Header("MainMenuUI")]
    public GameObject MainMenuDialogue;

    [Header("GamePlayUI")]
    public GameObject GamePlayDialogue;
    public RectTransform GamePlayLevelNumberParent;
    public Text GamePlayLevelNumberText;

    [Header("LevelCompleteUI")]
    public GameObject LevelCompleteDialogue;
    [Header("GameOverUI")]
    public GameObject GameOverDialogue;

    private void Awake()
    {
        Instance = this;
        AnimateGamePlayLevelNumber(1000f, 0, Ease.Linear);
        AnimateCoinTextParent(1000f, 0, Ease.Linear);
    }
    private void Start()
    {
        AnimateGamePlayLevelNumber(-300f, 1f, Ease.Linear);
        AnimateCoinTextParent(-300f, 1f, Ease.Linear);
        LevelManager.Instance.levelCreateFuncEvent += SetGamePlayLevelNumberText;
        GameManager.Instance.GameStarFuncEvent += SetGameStartUI;
        GameManager.Instance.GameResetFuncEvent += SetGameResetUI;
        GameManager.Instance.gameOverManager.GameOverFuncEvent += SetGameOverUI;
        GameManager.Instance.levelCompleteManager.levelCompleteFuncEvent += SetLevelCompleteUI;
    }

    private void SetGamePlayLevelNumberText(int level)
    {
        GamePlayLevelNumberText.text = "Level " + level;
    }

    private void SetGameStartUI()
    {
        MainMenuDialogue.gameObject.SetActive(false);
        GamePlayDialogue.gameObject.SetActive(true);
    }

    private void SetGameResetUI()
    {
        MainMenuDialogue.gameObject.SetActive(true);
        GamePlayDialogue.gameObject.SetActive(false);
        GameOverDialogue.gameObject.SetActive(false);
        LevelCompleteDialogue.gameObject.SetActive(false);
        AnimateGamePlayLevelNumber(-250f, 1f, Ease.Linear);

    }

    private void SetLevelCompleteUI()
    {
        LevelCompleteDialogue.gameObject.SetActive(true);
        MainMenuDialogue.gameObject.SetActive(false);
        GamePlayDialogue.gameObject.SetActive(false);
        AnimateGamePlayLevelNumber(1000f, 1f, Ease.Linear);


    }

    private void SetGameOverUI()
    {
        GameOverDialogue.gameObject.SetActive(true);
        MainMenuDialogue.gameObject.SetActive(false);
        GamePlayDialogue.gameObject.SetActive(false);
        AnimateGamePlayLevelNumber(1000f, 1f, Ease.Linear);
        AnimateCoinTextParent(1000f, 1f, Ease.Linear);
    }

    private void AnimateGamePlayLevelNumber(float yPos, float speed, Ease ease)
    {
        GamePlayLevelNumberParent.DOPause();
        GamePlayLevelNumberParent.DOAnchorPosY(yPos, speed).SetEase(ease);
    }

    private void AnimateCoinTextParent(float yPos, float speed, Ease ease)
    {
        CoinTextParent.DOPause();
        CoinTextParent.DOAnchorPosY(yPos, speed).SetEase(ease);
    }
    private void OnDisable()
    {
        CoinTextParent.DOPause();
        GamePlayLevelNumberParent.DOPause();
    }
}
