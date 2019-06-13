using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        if(_instance==null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
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
            CheckEndings();
            if(gameover == false)
                gameover = true;
        }
        else
            SchoolTime -= Time.deltaTime * timespeed;

        hour = (int)SchoolTime / 3600;
        minute = (int)(SchoolTime - hour * 3600) / 60;
        second = (int)(SchoolTime - hour * 3600 - minute * 60);

        ShowTimeText.text = string.Format("{0:D2}:{1:D2}:{2:D2}",
                                            hour, minute, second);
    }

    public void GameOver()
    {
        //被麻麻发现
        if (gameover == true)
            return;
        if(mom_discover_son)
        {
            //被发现玩游戏 被揍 普通结局 
            Debug.Log("NormalEnding");
            return;
        }
        else if(son_is_despair==true)
        {
            //心情值归0 绝望
            Debug.Log("绝望结局");
            return;
        }
        //正常结尾
        if(son_is_happy||son_is_workinghard)
        {
            if(!son_is_happy)
            {
                //没爽到 但努力学习 好学生结局
                Debug.Log("GoodEnding");
                return;
            }
            else if(!son_is_workinghard)
            {
                //爽到 但没努力学习 差生结局
                Debug.Log("BadEnding");
                return;
            }
            else
            {
                //既爽到 又努力学习 学神结局
                Debug.Log("TrueEnding");
                return;
            }
        }
        else
        {
            //既没有爽到也没有学习
            Debug.Log("CaiEnding");
            return;
        }
    }
    
    //public void CheckEndings(Endings ending)
    //{
    //    switch(ending)
    //    {
    //        case Endings.normalend:
    //            mom_discover_son = true;
    //            break;
    //        case Endings.goodend:
    //            son_is_workinghard = true;
    //            break;
    //        case Endings.badend:
    //            break;
    //        case Endings.trueend:
    //            son_is_workinghard = true;
    //            son_is_happy = true;
    //            break;
    //        default:
    //            break;
    //    }
    //    CheckGameOver();

    //}
    public void CheckEndings()
    {
        if (gameover == true)
            return;
        if(SchoolTime!=0)
        {
            if (son.Joy == 0)
                son_is_despair = true;
            else
                mom_discover_son = true;
        }
        else
        {
            mom_discover_son = false;
            son_is_despair = false;
            if (son.Joy == 100)
            {
                son_is_happy = true;
            }
            else
            {
                son_is_happy = false;
            }
            if (son.Study == 100)
            {
                son_is_workinghard = true;
            }
            else
            {
                son_is_workinghard = false;
            }
        }

        GameOver();
    }
}