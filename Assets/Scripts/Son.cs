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
    public GameObject Homework;
    public List<GameObject> Games;
    public GameObject StartInterface;
    private GameObject go;
    //public GameObject GameList;
    private void Awake()
    {
        Joy = 0;
        Study = 0;
        mood = 50;
        SonStatus = SonState.homework;
        Time.timeScale = 0;
        go = new GameObject();
    }
    private void Update()
    {
        if (Input.anyKeyDown && StartInterface.activeInHierarchy) {
            StartInterface.SetActive(false);//进入游戏界面
            Time.timeScale = 1;
        }


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
            Time.timeScale = 0.5f;
            //画面变成黑白
        }
        if (Input.GetMouseButtonUp(1))
        {
            // AudioManager.Instance.MomAudioPlay(AudioManager.MomClips.footstep);
            Time.timeScale = 1f;
            //画面恢复彩色
        }
        //switch (SonStatus)
        //{
        //    //case SonState.homework:
        //    //    if (Input.GetKeyDown(KeyCode.Alpha1))
        //    //    {
        //    //        Homework.SetActive(false);
        //    //        //Games[0].SetActive(false);
        //    //        //Games[1].SetActive(true);
        //    //        //GameList.SetActive(true);                  
        //    //        SonStatus = SonState.PC;
        //    //        OpenComputer();
        //    //    }
        //    //    break;
        //    case SonState.PC:
        //        if (Input.GetKeyDown(KeyCode.Escape))
        //        {
        //            Homework.SetActive(true);
        //            SonStatus = SonState.homework;
        //            Games[0].SetActive(true);
        //            Games[1].SetActive(false);
        //            Games[2].SetActive(false);
        //        }
        //        break;
        //}

    }
    public void ChangeState(SonState sonState)
    {
        SonStatus = sonState;

    }
    //private void OpenComputer()
    //{
    //    //播放开机音乐
    //   go.transform.DOMoveZ(0.1f, 3f).OnComplete(new TweenCallback(OnGames));
        
    //}
    //private void OnGames()
    //{
    //    Games[1].SetActive(false);
    //    Games[2].SetActive(true);
    //}
    //private void AddQte()
    //{
    //    GameObject go = new GameObject();
    //    go.transform.DOMoveZ(0.1f, 3f)
    //        .OnComplete(new TweenCallback(OnGames));
    //}

}
