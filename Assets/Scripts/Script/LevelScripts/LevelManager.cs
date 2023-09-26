using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    [HideInInspector]
    public LevelInfo CurrentLevel;

    public List<LevelInfo> Levels = new List<LevelInfo>();
    public bool TestLevel;
    public int levelnumber = 1;

    public bool allowLevelsRandomizationOrderAfterAllLevelsPlayed = true;

    private GameObject _levelParent;

    public delegate void LevelCreateFunc(int level);
    public LevelCreateFunc levelCreateFuncEvent;

    private void Awake()
    {
        Instance = this;
        CreateLevelParentObject();

    }


    private void CreateLevelParentObject()
    {
        _levelParent = new GameObject
        {
            transform =
            {
                position = Vector3.zero,
                eulerAngles = Vector3.zero
            },
            name = "Level Parent"
        };
    }

    private void CheckRandomizationOrderStatus()
    {
        try
        {

            LevelRandomization lr;
            if (!GameData.LevelRandomizationOrderHasKey())
            {

                lr = new LevelRandomization
                {
                    order = new int[Levels.Count]
                };
                for (var i = 0; i < lr.order.Length; i++)
                {
                    lr.order[i] = i;
                }
                GameData.SetLevelRandomizationOrder(lr);

            }

            lr = GameData.GetLevelRandomizationOrder();


            if (lr.order.Length < Levels.Count)
            {
                lr = new LevelRandomization
                {
                    order = new int[Levels.Count]
                };
                for (var i = 0; i < lr.order.Length; i++)
                {
                    lr.order[i] = i;
                }
                GameData.SetLevelRandomizationOrder(lr);
            }


            var temp = Levels.ToList();
            for (var i = 0; i < Levels.Count; i++)
            {
                Levels[i] = temp[lr.order[i]];
                Levels[i].name = "" + lr.order[i];

            }
            
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.ToString());

        }
    }

    public void LevelsArrayReshuffle()
    {
        if (allowLevelsRandomizationOrderAfterAllLevelsPlayed)
        {
            for (var t = 0; t < Levels.Count; t++)
            {
                var tmp = Levels[t];
                var r = Random.Range(t, Levels.Count);
                Levels[t] = Levels[r];
                Levels[r] = tmp;
            }
        }

        var lr = GameData.GetLevelRandomizationOrder();
        for (var i = 0; i < Levels.Count; i++)
        {
            var b = Levels[i].name.Where(char.IsDigit).Aggregate(string.Empty, (current, t) => current + t);

            lr.order[i] = int.Parse(b);
        }
        
        GameData.SetLevelRandomizationOrder(lr);
    }
    private void Start()
    {
        LevelStart();
    }

    private void LevelStart()
    {

        CheckRandomizationOrderStatus();

        var level = GameData.GetLevelNumber();

        if (level < 0)
            level = 0;

        level -= 1;

        var indexLevel = GameData.GetLevelNumberIndex();



        if (indexLevel > Levels.Count)
        {
            indexLevel = Levels.Count;
            GameData.SetLevelNumberIndex(Levels.Count);
        }

        indexLevel -= 1;


        if (TestLevel)
        {
            level = levelnumber;
            indexLevel = levelnumber;
        }
        

        if (level < 0)
            level = 0;
        if (indexLevel < 0)
            indexLevel = 0;
        
        var prefab = Levels[indexLevel];
        prefab.gameObject.SetActive(false);
        var li = Instantiate(prefab, new Vector3(-3.579486f, 0.295979f, -.7829592f), Quaternion.identity);
        li.gameObject.SetActive(true);
        
        var transform1 = li.transform;
        transform1.position = new Vector3(-3.579486f, 0.295979f, -.7829592f);
        transform1.eulerAngles = Vector3.zero;
        transform1.localScale = Vector3.one;

        CurrentLevel = li;


        li.transform.SetParent(_levelParent.transform);
        li.gameObject.SetActive(true);
        

        levelCreateFuncEvent.Invoke(level + 1);
        
    }
    
}