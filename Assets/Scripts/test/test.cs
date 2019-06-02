using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    public QTE QTE;
    public Bubble Bubble;

    void Start() {
        //QTE.Play();
        Bubble.Bub("嘿嘿嘿", 3f);
    }

    public void TestBubble() {
        List<string> texts = new List<string>{
            "哈哈哈哈哈",
            "Nice boy",
            "You Die",
            "Audio",
            "Bubble",
            "Tesla 特斯拉",
            "Attention!"
        };

        int index = Random.Range(0, texts.Count - 1);
        Bubble.Bub(texts[index], Random.Range(2f, 4f));
    }

}