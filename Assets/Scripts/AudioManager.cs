using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    //
    public enum MomClips { footstep,opendoor };
    public enum SonClips { warning };

    public AudioSource SonSource;
    public AudioSource MomSource;

    //声音种类
    private List<AudioClip> MomClip;
    private List<AudioClip> SonClip;

    //音量
    public int SonVolume = 1;
    public int MomVolume = 1;


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
    }

    private void PutAudio()
    {
        //获取音频文件到MomClips和SonClips
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

}
