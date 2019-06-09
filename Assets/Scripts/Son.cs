using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SonState { homework, PC, PS4, nintendoSwitch, phone };
public class Son : MonoBehaviour {
    public Device GameDevice=new Device();
    public int Joy;//学习和玩乐进度均为0-100
    public int Study;
    public int mood;//心情值关乎学习效率
    SonState SonStatus;

    private void Awake()
    {
        Joy = 0;
        Study = 0;
        mood = 50;
        SonStatus = SonState.homework;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //触发收拾游戏机返回书桌的qte
            //GameDevice.PlayQte((int)SonStatus);
            
            //if (QTE成功)SonStatus = SonState.homework;
        }
        //鼠标右键为聆听状态
        if (Input.GetMouseButtonDown(1))
        {
            AudioManager.Instance.MomAudioPlay(AudioManager.MomClips.footstep);
            //画面变成黑白
        }
        if (Input.GetMouseButtonUp(1))
        {
            AudioManager.Instance.MomAudioPlay(AudioManager.MomClips.footstep);
            //画面恢复彩色
        }
        switch (SonStatus)
        {
            case SonState.homework:
                break;
            case SonState.nintendoSwitch
        }

    }
    public void ChangeState(SonState sonState)
    {
        SonStatus = sonState;

    }
}
