using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public enum GameState { Close,Open,Playing,Homework};
public class Device : MonoBehaviour {
    public List<GameObject> GameProgress;
    public List<GameObject> BackGround;
    public int GameProgressIndex = 0;
    public List<GameObject> Games;
    public List<GameObject> Books;
    public int BooksIndex = 0;
    public List<GameObject> Bubbles;
    public List<GameObject>QTE = new List<GameObject>();
    public GameState gameState;
    public float TempScore = 0;  //暂存心情
    public float HomeworkScore = 0;
    public bool CanChange = true;
    private int GameNum = 0;
    public GameObject GameList;
    public GameObject ProgressBar;
    public float ProgressBarTimer = 0;
    private float BubbleTimer = 0;
    private float MomOpenDoorTimer = 0;
    private float OpenedDoorTimer = 0;
    private float CloseDoorTimer = 0;
    private bool ShowBubble = false;
    private bool isWarning = false;
    public AudioSource voiceSource;
    public AudioClip warning;
    public AudioClip qteSuccess;
    public AudioClip homeworkqte;
    public AudioClip diwuQte;
    private float HomworkTimer = 0;
    public bool DoHomework = true;
    public GameObject Homework;
    private bool CanFill = false;
    private GameObject NowQTE;
    private GameObject go;
    private Transform Pos;
    private float duration = 0;
    void Awake()
    {
        gameState =GameState.Homework;
        Pos = GameObject.Find("QtePos").transform;
        AudioManager.Instance.SonAudioPlay(AudioManager.SonClips.BGM);
        go = new GameObject();
        voiceSource = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        MomOpenDoorTimer += Time.deltaTime;
        if (MomOpenDoorTimer > 20)
        {
            //孩子冒出警告信息
            if (!isWarning)
            {
                voiceSource.clip = warning;
                voiceSource.Play();
                isWarning = true;
            }

            if (gameState != GameState.Homework) {
                //播放提示音
                Bubbles[2].SetActive(true);
            }
            OpenedDoorTimer += Time.deltaTime;
            if (OpenedDoorTimer > 1.5)
            {
                BackGround[0].SetActive(false);
                BackGround[1].SetActive(true);
                BackGround[2].SetActive(true);
                Bubbles[2].SetActive(false);
                if (gameState == GameState.Playing) {
                    LevelManager.Instance.GameOver(Endings.DefeatEnd);
                }
                CloseDoorTimer += Time.deltaTime;
                if (CloseDoorTimer > 3) {
                    BackGround[0].SetActive(true);
                    BackGround[1].SetActive(false);
                    BackGround[2].SetActive(false);
                    MomOpenDoorTimer = 0;
                    OpenedDoorTimer = 0;
                    CloseDoorTimer = 0;
                    isWarning = false;
                }
            }
        }
        if (ShowBubble == true&&gameState == GameState.Playing)//玩游戏时候显示两秒气泡
        {
           BubbleTimer += Time.deltaTime;
            Bubbles[0].SetActive(true);
            CanChange = false;
            if (BubbleTimer > 2)
            {
                  CanChange = true;
                Bubbles[0].SetActive(false);
                BubbleTimer = 0;
                ShowBubble =false;
            }

           
        }

        if (ShowBubble == true && gameState == GameState.Homework)//做作业时候显示两秒气泡
        {
            BubbleTimer += Time.deltaTime;
            Bubbles[1].SetActive(true);
            CanChange = false;
            if (BubbleTimer > 2)
            {
                CanChange = true;
                Bubbles[1].SetActive(false);
                BubbleTimer = 0;
                ShowBubble = false;

            }
            
        }

        if (TempScore>0&&gameState!= GameState.Playing)
        { TempScore -= Time.deltaTime;}

        if (TempScore <0)
        {
            TempScore = 0;
        }

        if (CanFill&&(gameState == GameState.Playing))
        {
            ProgressBarTimer += Time.deltaTime;
            if (ProgressBarTimer > 4)
            {
                CreateQte();
                CanFill = false;
                ProgressBarTimer = 0;
            }
        }
        switch (gameState)
        {   
            case GameState.Homework:
            {
               if (DoHomework == true)
                {
                    HomworkTimer += Time.deltaTime;
                    if (HomworkTimer > 5)
                    {
                        //暂时用游戏qte
                        GameNum = 0;
                        CreateQte();
                        HomworkTimer = 0;
                        DoHomework = false;
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha1) && CanChange==true)
                {
                        Debug.Log("input");
                    DoHomework = false;
                    HomworkTimer = 0;
                    Homework.SetActive(false);
                    Games[0].SetActive(false);
                    Games[1].SetActive(true);
                    GameList.SetActive(true);
                    gameState = GameState.Open;

                    if (NowQTE)
                    {
                        Destroy(NowQTE.gameObject);
                    }
                }


            }
                    break;
            case GameState.Open:
             if (Input.GetKeyDown(KeyCode.Alpha2) && CanChange == true)
             {
                 Bubbles[0].SetActive(false);
                 DoHomework = true;
                 ProgressBarTimer = 0;
                    GameList.SetActive(false);
                    Homework.SetActive(true);
                    gameState =GameState.Homework;
                    Games[0].SetActive(true);
                    Games[1].SetActive(false);
                    Games[2].SetActive(false);
                }
                break;
                
            case GameState.Playing:
                if (Input.GetKeyDown(KeyCode.Alpha2) && CanChange == true)

                {
                    AudioManager.Instance.SonAudioPlay(AudioManager.SonClips.BGM);
                    Bubbles[0].SetActive(false);
                    DoHomework = true;
                    ProgressBarTimer = 0;
                    GameList.SetActive(false);
                    Homework.SetActive(true);
                    gameState = GameState.Homework;
                    Games[0].SetActive(true);
                    Games[1].SetActive(false);
                    Games[2].SetActive(false);
                    Games[3].SetActive(false);
                    if (NowQTE)
                    {
                        Destroy(NowQTE.gameObject);
                    }
                }
                break;
          
        }
       

    }
    
    public void PlayQte(int n)
    {
        if (n == 0) {
            Games[1].SetActive(false);
            Games[3].SetActive(true);
            AudioManager.Instance.GameAudioPlay(AudioManager.Games.Mario, AudioManager.GameClips.BGM);
            }
        if (n == 1)
        {
            Games[1].SetActive(false);
            //暂时
            Games[2].SetActive(true);
            AudioManager.Instance.GameAudioPlay(AudioManager.Games.diwurenge, AudioManager.GameClips.BGM);
        }
        gameState = GameState.Playing;
        GameList.SetActive(false);
        GameNum = n;
        //播放音效
        //AddQte();
       
      //GameList.gameObject.SetActive(false);
        
        //根据玩家选择的游戏选择QTE
         
        //生成进度条
        //Instantiate(ProgressBar.gameObject,Pos);
        CanFill = true;
      
    }

    //public void EndQte()
    //{

    //    //销毁QTE
    //    Destroy(NowQTE.gameObject);
    //}
    public void GetScore()
    {
        if(gameState==GameState.Playing)
        {
            if (NowQTE.GetComponent<QTE>().GetScore() > 0)
            {
                CanFill = true;
                ShowBubble = true;
                if (GameNum == 0)
                {
                    voiceSource.clip = qteSuccess;
                    voiceSource.Play();
                }
                if (GameNum == 1)
                {
                    voiceSource.clip = diwuQte;
                    voiceSource.Play();
                }
                //成功触发qte以后会判断结局条件
                GameProgress[GameProgressIndex].SetActive(false);
                GameProgressIndex++;
                GameProgress[GameProgressIndex].SetActive(true);
                if (GameProgressIndex == 6)
                {
                    LevelManager.Instance.GameOver(Endings.GameFinishEnd);
                }
            }
            else
            {
                CanFill = true;
            }
            if (TempScore>100)
            {
                TempScore = 100;
            }
        }

        if (gameState == GameState.Homework)
        {
            HomeworkScore += NowQTE.GetComponent<QTE>().GetScore();
            if (NowQTE.GetComponent<QTE>().GetScore() > 0)
            {
                voiceSource.clip = homeworkqte;
                voiceSource.Play();
                ShowBubble = true;
                DoHomework = true;
                Books[BooksIndex].SetActive(false);
                BooksIndex++;
                Books[BooksIndex].SetActive(true);
                if (BooksIndex == 4) {
                    LevelManager.Instance.GameOver(Endings.HomeworkEnd);
                }
            }
            else
            {
                DoHomework = true;
            }
            if(HomeworkScore>100)
            {
                HomeworkScore = 100;
            }
           
        }

    }
    public void CreateQte()
    {
       
        NowQTE=Instantiate(QTE[GameNum],Pos);
       
        NowQTE.transform.parent = Pos;
        duration=NowQTE.GetComponent<QTE>().Play();
         NowQTE.GetComponent<QTE>().GetButton().onClick.AddListener(GetScore);
        
         
    }
}
