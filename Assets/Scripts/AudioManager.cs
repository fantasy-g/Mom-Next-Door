using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    public Device device;

    //
    public enum MomClips { footstep,opendoor };
    public enum SonClips { warning };
    public enum Games { Mario , diwurenge };
    public enum GameClips { BGM , QTEsuccess , QTEfail }

    public AudioSource SonSource;
    public AudioSource MomSource;
    public AudioSource DeviceSource;
    public AudioSource DeviceBGM;

    //声音种类
    public List<AudioClip> MomClip = new List<AudioClip>();
    public List<AudioClip> SonClip = new List<AudioClip>();
    public List<List<AudioClip>> DeviceClip = new List<List<AudioClip>>();//暂时不用
    public List<AudioClip> GameClip_Mario = new List<AudioClip>();
    public List<AudioClip> GameClip_Diwu = new List<AudioClip>();
    public int DeviceNum = 1;

    //音量
    public int SonVolume = 1;
    public int MomVolume = 1;
    public int DeviceVolume = 1;
    public int BGMVolume = 1;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        MomSource = this.GetComponent<AudioSource>();
        SonSource = this.GetComponent<AudioSource>();
        DeviceSource = this.GetComponent<AudioSource>();
        DeviceBGM = this.GetComponent<AudioSource>();
    }

    private void PutAudio()
    {
        //获取音频文件到DeviceClip
        
    }

    //播放妈妈音效
    public void MomAudioPlay(MomClips choice)
    {
        MomSource.clip = MomClip[(int)choice];
        MomSource.volume *= MomVolume;
        MomSource.Play();
    }

    //播放儿子音效
    public void SonAudioPlay(SonClips choice)
    {
        SonSource.clip = SonClip[(int)choice];
        SonSource.volume *= SonVolume;
        SonSource.Play();
    }


    //public void DeviceAudioPlay(Games game,GameClips gameClip)
    //{
    //    if (gameClip == GameClips.BGM)
    //    {
    //        DeviceBGM.clip = DeviceClip[(int)game][(int)gameClip];
    //        DeviceBGM.volume = BGMVolume;
    //        DeviceBGM.Play();
    //        if (device.gameState == GameState.Homework)
    //            DeviceBGM.Stop();
    //    }
    //    else
    //    {
    //        DeviceSource.clip = DeviceClip[(int)game][(int)gameClip];
    //        DeviceSource.volume = DeviceVolume;
    //        DeviceSource.Play();
    //    }
    //}

    public void GameAudioPlay(Games game, GameClips gameClip)
    {
        switch(game)
        {
            case Games.Mario:
                DeviceSource.clip = GameClip_Mario[(int)gameClip];
                DeviceSource.volume = DeviceVolume;
                DeviceSource.Play();
                break;
            case Games.diwurenge:
                DeviceSource.clip = GameClip_Diwu[(int)gameClip];
                DeviceSource.volume = DeviceVolume;
                DeviceSource.Play();
                break;
            default:
                break;
        }
    }

    public void GameBGMPlay(Games game)
    {
        switch (game)
        {
            case Games.Mario:
                DeviceBGM.clip = GameClip_Mario[(int)GameClips.BGM];
                DeviceBGM.volume = DeviceVolume;
                DeviceBGM.Play();
                break;
            case Games.diwurenge:
                DeviceBGM.clip = GameClip_Diwu[(int)GameClips.BGM];
                DeviceBGM.volume = DeviceVolume;
                DeviceBGM.Play();
                break;
            default:
                break;
        }
    }
}