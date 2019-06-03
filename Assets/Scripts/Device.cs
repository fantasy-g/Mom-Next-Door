using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Device : MonoBehaviour {
    public List<GameObject>QTE = new List<GameObject>();
   
    public int TempScore = 0;  //暂存心情
    private int GameNum = 0;
    //private string QteName;
    public GameObject GameList;
    public GameObject ProgressBar;
   public float ProgressBarTimer = 0;
   
    private bool CanFill = false;
    private GameObject NowQTE;
    
    private Transform Pos;
    
    private float duration = 0;
    void Start()
    {
       
       
    }

    void Update()
    {
       
        if (CanFill)
        {
            ProgressBarTimer += Time.deltaTime;
            if (ProgressBarTimer > 6.5)
            {
                CreateQte();
                
                CanFill = false;
                ProgressBarTimer = 0;
            }
        }

       

    }
    public void ChooseGame()
    {
       
    }
    public void PlayQte(int n)
    {
       
        Pos =GameObject.Find("QtePos").transform;
      //GameList.gameObject.SetActive(false);
        
        //根据玩家选择的游戏选择QTE
          GameNum = n;
        //生成进度条
        Instantiate(ProgressBar.gameObject,Pos);
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
        
    }
    public void CreateQte()
    {
       
        NowQTE=Instantiate(QTE[GameNum],Pos);
       
        NowQTE.transform.parent = Pos;
        duration=NowQTE.GetComponent<QTE>().Play();
         NowQTE.GetComponent<QTE>().GetButton().onClick.AddListener(GetScore);
         
    }

    
}
