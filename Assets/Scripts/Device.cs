using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Device : MonoBehaviour {
    public List<GameObject>QTE = new List<GameObject>();
    public QTE qte;
    public int Temp = 0;  //暂存心情
    private int GameNum = 0;
    private string QteName;
    public GameObject GameList;
    public GameObject ProgressBar;
    private Progressbar progressbar;
    public static Device instance;
    public bool IsFinish = false;
    private Transform Pos;
    void Start()
    {
        instance = this;
        progressbar = ProgressBar.GetComponent<Progressbar>();
    }

    void Update()
    {
        if (IsFinish)
        {
            CreateQte();
            IsFinish = false;
        }
    }
    public void ChooseGame()
    {
       
    }
    public void PlayQte(int n)
    {
       progressbar.C();
       Pos=GameObject.Find("QtePos").transform;
      
        //根据玩家选择的游戏选择QTE
          GameNum = n;
        //生成进度条
        Instantiate(ProgressBar.gameObject,Pos);
       
       




    }

    public void EndQte()
    {
        
        //销毁QTE
       Destroy(GameObject.Find(QteName));
    }

    public void CreateQte()
    {
       
        GameObject NowQTE=Instantiate(QTE[GameNum],Pos);
        NowQTE.transform.parent = Pos;
        NowQTE.GetComponent<QTE>().Play();


    }
}
