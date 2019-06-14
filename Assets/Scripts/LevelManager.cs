using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum Endings { TimeOutEnd,HomeworkEnd,GameFinishEnd,DefeatEnd}
public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance { get { return _instance; } }

    
    public Mom mom;
    public Son son;

    public Text ShowTimeText;
    public float SchoolTime = 28800f;
    public int timespeed = 1;
    public int hour;
    public int minute;
    public int second;

    [SerializeField]
    private bool son_is_happy = false;//心情值是否到100
    [SerializeField]
    private bool son_is_workinghard = false;//学习值是否到100
    [SerializeField]
    private bool mom_discover_son = false;//是否被妈妈发现玩游戏
    [SerializeField]
    private bool son_is_despair = false;//心情值为0

    private bool gameover=false;

    public List<GameObject> EndingPanels = new List<GameObject>();

    private void Awake()
    {
        /* if(_instance==null)
         {
             _instance = this;
             DontDestroyOnLoad(gameObject);
         }
         else
         {
             Destroy(gameObject);
             return;
         }*/
         if(_instance==null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        if (SchoolTime <= 0)
        {
            SchoolTime = 0;
            GameOver(Endings.TimeOutEnd);
        }
        else
            SchoolTime -= Time.deltaTime * timespeed;

        hour = (int)SchoolTime / 3600;
        minute = (int)(SchoolTime - hour * 3600) / 60;
        second = (int)(SchoolTime - hour * 3600 - minute * 60);

        ShowTimeText.text = string.Format("{0:D2}:{1:D2}:{2:D2}",
                                            hour, minute, second);
    }

    public void GameOver(Endings GameStatus)
    {
        //被麻麻发现
        if (gameover == true)
            return;
        gameover = true;
        Time.timeScale = 0;
        AudioManager.Instance.EndAudioPlay(GameStatus);
        EndingPanels[(int)GameStatus].SetActive(true);

    }
    public void restartGame()
    {
        SceneManager.LoadScene(0);
        SchoolTime = 10800f;
    }
}