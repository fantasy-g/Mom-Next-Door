﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MultiKeyQTE : QTE {
    
    public int KeyCount = 7;
    public int PrizeScore = 10;
    public float MaxDuration = 30f;         // 最大时间
    public GameObject ArrowKeyPrefab;
    
    private bool playing = false;
    private float timer = 0;
    private int PunishScore = -5;

    [SerializeField]
    private List<Sprite> keySprites = new List<Sprite>();
    private List<KeyCode> keyCodes = new List<KeyCode>();
    private List<KeyCode> backKeyCodes = new List<KeyCode>();
    private List<KeyCode> ArrowKeyCodes = new List<KeyCode> {
            KeyCode.UpArrow,
            KeyCode.DownArrow,
            KeyCode.LeftArrow,
            KeyCode.RightArrow,
            KeyCode.Space
        };


    private void Awake() {
        score = 0;
    }

    private void Start() {
        // 图片 Sprites 加载
        foreach (KeyCode key in ArrowKeyCodes) {
            string spritePath = string.Format("UI/{0}", GetKeyString(key));
            Sprite sprite = Resources.Load(spritePath, typeof(Sprite)) as Sprite;
            keySprites.Add(sprite);
        }

        // 随机生成按键
        for (int i = 0; i < KeyCount; i++) {
            int index = Random.Range(0, ArrowKeyCodes.Count);
            keyCodes.Add(ArrowKeyCodes[index]);
            backKeyCodes.Add(ArrowKeyCodes[index]);
        }

        // 实例化按键Prefab from backKeyCodes
        InstantiateArrowKey();
    }


    private void Update() {
        if (playing && Input.anyKeyDown) {
            int tmpScore = TryHit();
            // 按错重新开始
            if (tmpScore == PunishScore) {
                Restart();
            }
            else {
                score += tmpScore;
            }
        }
        
        if (timer > 0) {
            timer -= Time.deltaTime;
        }
        else if (timer < 0) {
            timer = 0;
            Finish();   // 运行结束
        }

    }


    public override float Play() {
        playing = true;
        timer = MaxDuration;
        return MaxDuration;
    }

    public override Button GetButton() {
        return GetComponent<Button>();
    }

    private int TryHit() {
        if (!Input.GetKey(keyCodes[0])) {
            return PunishScore;
        }
        else {
            keyCodes.RemoveAt(0);
            Destroy(transform.GetChild(0).gameObject);

            if (keyCodes.Count == 0) {
                playing = false;
                timer = -1;     // 全部完成，结束QTE
            }
            return PrizeScore;
        }

    }


    private void Finish() {
        Debug.Log("Finish MultiKeyQTE with score : " + score);
        ExecuteEvents.Execute<IPointerClickHandler>(
            GetComponent<Button>().gameObject,
            new PointerEventData(EventSystem.current),
            ExecuteEvents.pointerClickHandler
            );
        playing = false;
        Destroy(gameObject, 0.1f);
    }

    private void Restart() {
        score = 0;

        // 清除所有按键物体
        int childCount = transform.childCount;
        for(int i = 0; i < childCount; i++) {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        // keyCodes 重新赋值
        keyCodes.Clear();
        foreach(KeyCode key in backKeyCodes) {
            keyCodes.Add(key);
        }

        // 重新实例化按键
        InstantiateArrowKey();
    }

    // 实例化 backKeyCodes 中的按键
    private void InstantiateArrowKey() {
        foreach (KeyCode key in backKeyCodes) {
            GameObject go = Instantiate(ArrowKeyPrefab, transform);
            go.GetComponent<Image>().sprite = keySprites[ArrowKeyCodes.FindIndex(keyCode => keyCode == key)];
        }
    }

    private string GetKeyString(KeyCode key) {
        switch (key) {
            case KeyCode.UpArrow:
                return "上";
            case KeyCode.DownArrow:
                return "下";
            case KeyCode.LeftArrow:
                return "左";
            case KeyCode.RightArrow:
                return "右";
            case KeyCode.Space:
                return "空格";
            default:
                return "KeyCodeError";
        }
    }

}
