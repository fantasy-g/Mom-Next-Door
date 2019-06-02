using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance { get { return _instance; } }

    public Mom mom;
    public Son son;

    private bool son_is_happy = false;//心情值是否到100
    private bool son_is_workinghard = false;//学习值是否到100
    private bool mom_discover_son = false;//是否被妈妈发现玩游戏

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

    public void CheckGameOver()
    {
        //被麻麻发现
        if(mom_discover_son)
        {
            //被发现玩游戏 被揍 普通结局 
            return;
        }
        //正常结尾
        if(son_is_happy||son_is_workinghard)
        {
            if(!son_is_happy)
            {
                //没爽到 但努力学习 好学生结局
                return;
            }
            else if(!son_is_workinghard)
            {
                //爽到 但没努力学习 差生结局
                return;
            }
            else
            {
                //既爽到 又努力学习 学神结局
                return;
            }
        }
        else
        {
            //既没有爽到也没有学习
            return;
        }
    }

    public void CheckParameter()
    {
        //游戏结束时比gameover先调用，用于检查参数
    }


}
