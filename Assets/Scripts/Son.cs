using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//[SerializeField]
public enum SonState { homework, PC, PS4, nintendoSwitch, phone };
public class Son : MonoBehaviour {
    public Device GameDevice=new Device();
    public int Joy;//学习和玩乐进度均为0-100
    public int Study;
    public int mood;//心情值关乎学习效率
    SonState SonStatus;
    public GameObject StartInterface;
    //public GameObject GameList;
    private void Awake()
    {
        Joy = 0;
        Study = 0;
        mood = 50;
        SonStatus = SonState.homework;
        Time.timeScale = 0;
    }
    private void Update()
    {
        /*if (Input.anyKeyDown && StartInterface.activeInHierarchy) {
            StartInterface.SetActive(false);//进入游戏界面
            Time.timeScale = 1;
        }*/


        if (Input.GetKeyDown(KeyCode.Q))
        {
            //触发收拾游戏机返回书桌的qte
            //GameDevice.PlayQte((int)SonStatus);
            
            //if (QTE成功)SonStatus = SonState.homework;
        }
        //鼠标右键为聆听状态
        if (Input.GetMouseButtonDown(1))
        {
            // AudioManager.Instance.MomAudioPlay(AudioManager.MomClips.footstep);
            Time.timeScale = 0.3f;
            //画面变成黑白
        }
        if (Input.GetMouseButtonUp(1))
        {
            // AudioManager.Instance.MomAudioPlay(AudioManager.MomClips.footstep);
            Time.timeScale = 1f;
            //画面恢复彩色
        }

    }
    public void ChangeState(SonState sonState)
    {
        SonStatus = sonState;

    }

}
