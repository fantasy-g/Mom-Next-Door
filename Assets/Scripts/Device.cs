using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public enum GameState { Close,Open,Playing,Homework};
public class Device : MonoBehaviour {
    public List<GameObject> Games;
    public List<GameObject>QTE = new List<GameObject>();
    private GameState gameState;
    public int TempScore = 0;  //暂存心情
    private int GameNum = 0;
    //private string QteName;
    public GameObject GameList;
    public GameObject ProgressBar;
   public float ProgressBarTimer = 0;
    public GameObject Homework;
    private bool CanFill = false;
    private GameObject NowQTE;
    private GameObject go;
    private Transform Pos;
    private float duration = 0;
    void Awake()
    {
        gameState =GameState.Homework;
        go = new GameObject();
    }

    void Update()
    {

        if (CanFill&&(gameState == GameState.Playing))
        {
            ProgressBarTimer += Time.deltaTime;
            if (ProgressBarTimer > 3)
            {
                Games[1].SetActive(false);
                Games[2].SetActive(true);

                CreateQte();

                CanFill = false;
                ProgressBarTimer = 0;
            }
        }
    Debug.Log(gameState);
        switch (gameState)
        {   
            case GameState.Homework:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    Homework.SetActive(false);
                    Games[0].SetActive(false);
                   Games[1].SetActive(true);
                    GameList.SetActive(true);
                    gameState = GameState.Open;
                    go.transform.DOMoveZ(0f,3f ).OnComplete(new TweenCallback(OnGames));
                    Debug.Log("PC");
                }
                    break;
            case GameState.Open:
             if (Input.GetKeyDown(KeyCode.Escape))
                {
                    GameList.SetActive(false);
                    Homework.SetActive(true);
                    gameState =GameState.Homework;
                    Games[0].SetActive(true);
                    Games[1].SetActive(false);
                    Games[2].SetActive(false);
                }
                break;
                
            case GameState.Playing:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    GameList.SetActive(false);
                    Homework.SetActive(true);
                    gameState = GameState.Homework;
                    Games[0].SetActive(true);
                    Games[1].SetActive(false);
                    Games[2].SetActive(false);
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
        gameState = GameState.Playing;
        GameList.SetActive(false);
        GameNum = n;
        //播放音效
        //AddQte();
        
        
        Pos =GameObject.Find("QtePos").transform;
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
       TempScore+= NowQTE.GetComponent<QTE>().GetScore();
        Debug.Log(TempScore);
        GameList.SetActive(true);

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
