using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public enum GameState { Close,Open,Playing,Homework};
public class Device : MonoBehaviour {
    public List<GameObject> Games;
    public List<GameObject> Books;
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
    private bool ShowBubble = false;

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
    }

    void Update()
    {
        if (ShowBubble == true&&gameState == GameState.Playing)
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

        if (ShowBubble == true && gameState == GameState.Homework)
        {
            BubbleTimer += Time.deltaTime;
            Bubbles[1].SetActive(true);
            CanChange = false;
            if (BubbleTimer > 2)
            {
                 CanChange = true;
                Debug.Log("33");
                Bubbles[1].SetActive(false);
                BubbleTimer = 0;
                ShowBubble = false;

            }
            
        }
        if(TempScore>0&&gameState!= GameState.Playing)
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
                if (HomeworkScore > 0 && HomeworkScore < 25)
                {
                    Books[0].SetActive(true);
                }

                if (HomeworkScore > 24 && HomeworkScore < 50)
                {
                    Books[0].SetActive(false);
                    Books[1].SetActive(true);
                    }
                if (HomeworkScore > 49 && HomeworkScore < 75)
                {
                    Books[1].SetActive(false);
                    Books[2].SetActive(true);
                }
                if (HomeworkScore > 74 && HomeworkScore < 100)
                {
                    Books[2].SetActive(false);
                    Books[3].SetActive(true);
                }

                if (HomeworkScore == 100)
                {
                    Books[3].SetActive(false);
                    Books[4].SetActive(true);
                }
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
             if (Input.GetKeyDown(KeyCode.Escape) && CanChange == true)
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
                if (Input.GetKeyDown(KeyCode.Escape) && CanChange == true)

                {
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
            }
        if (n == 1)
        {
            Games[1].SetActive(false);
            //暂时
            Games[2].SetActive(true);
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
            TempScore += NowQTE.GetComponent<QTE>().GetScore();
            Debug.Log(TempScore);
            if (TempScore <= 100 && NowQTE.GetComponent<QTE>().GetScore() > 0)
            {
                CanFill = true;

                ShowBubble = true;
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
            if (HomeworkScore <= 100 && NowQTE.GetComponent<QTE>().GetScore() > 0)
            {
                ShowBubble = true;
                DoHomework = true;
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
    private void OnGames()
    {
       
    }
    private void AddQte()
    {
        GameObject go = new GameObject();
        go.transform.DOMoveZ(0.1f, 3f).OnComplete(new TweenCallback(OnGames));
    }

}
