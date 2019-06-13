using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mom : MonoBehaviour {
    public GameObject openDoor;
    public float ProgressBarTimer = 0;
    private GameObject go;
    private void Awake()
    {
        ProgressBarTimer = 0;
        go = new GameObject();
    }
    private void Update()
    {
        ProgressBarTimer += Time.deltaTime;
        if (ProgressBarTimer > 5f) {
            ProgressBarTimer -= 5f;
            go.transform.DOMoveZ(0.1f, 3f)
            .  OnComplete(new TweenCallback(CheckSon));
        }
    }
    private void CheckSon()
    { }
}
