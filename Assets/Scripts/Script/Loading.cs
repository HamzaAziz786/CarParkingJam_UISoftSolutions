using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{

    public string SceneToLoad = "Gameplay";
    public Image FillBar;

    public bool FirstScene;

    private AsyncOperation _asyncOperation;

    private void Awake()
    {

        StartCoroutine(LoadScene());

    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1f);

        //Begin to load the Scene you specify
        _asyncOperation = SceneManager.LoadSceneAsync(SceneToLoad);
        //Don't let the Scene activate until you allow it to
        _asyncOperation.allowSceneActivation = false;
        //Debug.Log("Pro :" + asyncOperation.progress);

        LoadGamePlayScene();

    }

    private void LoadGamePlayScene()
    {
        StartCoroutine(SceneLoad(_asyncOperation));
    }

    private IEnumerator SceneLoad(AsyncOperation asyncOperation)
    {
        while (!asyncOperation.isDone)
        {

            FillBar.fillAmount = asyncOperation.progress;

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {

                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }






}