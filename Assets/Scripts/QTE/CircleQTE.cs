using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleQTE : QTE {

    public float Speed = 10;
    public int KeyCount = 5;
    public int PrizeScore = 10;
    public int PunishScore = -5;
    public int Offset = 10;

    public Text KeyText;
    public RectTransform BulletTranform;
    public RectTransform HitTransform;

    private float duration;         // 单个按键的最大时间
    private float rawWidth;         // 圆圈的初始半径
    private bool playing = false;
    private float timer = 0;


    private List<KeyCode> keyCodes = new List<KeyCode>();
    private List<KeyCode> ArrowKeyCodes = new List<KeyCode> {
            KeyCode.UpArrow,
            KeyCode.DownArrow,
            KeyCode.LeftArrow,
            KeyCode.RightArrow
        };


    private void Awake() {
        score = 0;
        rawWidth = BulletTranform.rect.width;
        duration = rawWidth / (Speed * 10);
    }

    private void Start() {
        for(int i = 0; i < KeyCount - 1; i++) {
            keyCodes.Add(
                ArrowKeyCodes[Random.Range(0, ArrowKeyCodes.Count - 1)]
                );
        }
        keyCodes.Add(KeyCode.Space);
    }


    private void Update() {
        if (playing && Input.anyKeyDown) {
            int tmpScore = TryHit();
            score += tmpScore;
        }

        // 切换按键的计时器
        if (timer > 0) {
            timer -= Time.deltaTime;
        }
        else if (timer < 0) {
            timer = 0;
            
            bool success = NextKeyQTE();    // 换下一个键
            if (!success) {
                Finish();   // 运行结束
            }
        }

        // CircleQTE 运行 圈缩小
        if (playing) {
            Vector2 size = BulletTranform.sizeDelta;    // width height
            size.x -= Speed * 10 * Time.deltaTime;
            size.y -= Speed * 10 * Time.deltaTime;
            BulletTranform.sizeDelta = size;
        }
    }


    public override float Play() {
        playing = true;
        timer = -1;     // 立即切换到第一个按键
        return duration * KeyCount;
    }

    private int TryHit() {
        timer = -1;   // 切换下一个按键

        // 检查按键
        KeyCode targetKeyCode = GetKeyCode(KeyText.text);
        if (!Input.GetKey(targetKeyCode)) {
            return PunishScore;
        }

        // 检查位置
        float width = BulletTranform.rect.width;
        float target = HitTransform.rect.width;
        if (Mathf.Abs(width - target) < Offset) {
            return PrizeScore;
        }
        else {
            return PunishScore;
        }
    }

    private bool NextKeyQTE() {
        if (keyCodes.Count == 0) {
            return false;
        }

        float randomWidth = Random.Range(rawWidth * 0.3f, rawWidth * 0.8f);
        BulletTranform.sizeDelta = new Vector2(rawWidth, rawWidth);     // 圈恢复大小
        HitTransform.sizeDelta = new Vector2(randomWidth, randomWidth); // 目标圈随机半径
        KeyText.text = GetKeyString(keyCodes[0]);
        timer = duration;

        keyCodes.RemoveAt(0);
        return true;
    }

    private void Finish() {
        Debug.Log("Finish CircleQTE with score : " + score);
        playing = false;
        Destroy(gameObject, .5f);
    }

    private KeyCode GetKeyCode(string key = null) {
        switch (key) {
            case "↑":
                return KeyCode.UpArrow;
            case "↓":
                return KeyCode.DownArrow;
            case "←":
                return KeyCode.LeftArrow;
            case "→":
                return KeyCode.RightArrow;
            default:
                return KeyCode.Space;
        }
    }

    private string GetKeyString(KeyCode key) {
        switch (key) {
            case KeyCode.UpArrow:
                return "↑";
            case KeyCode.DownArrow:
                return "↓";
            case KeyCode.LeftArrow:
                return "←";
            case KeyCode.RightArrow:
                return "→";
            case KeyCode.Space:
                return "Space";
            default:
                return "KeyCodeError";
        }
    }
}