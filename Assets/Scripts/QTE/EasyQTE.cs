using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasyQTE : QTE {

    public float Speed = 10;
    public int PrizeScore = 10;
    public int PunishScore = -5;

    public RectTransform HitTransform;
    public Button PointBtn;

    private float width;
    private float duration;
    private bool playing = false;
    private RectTransform pointTransform;


    private void Start() {
        width = GetComponent<RectTransform>().rect.width;
        pointTransform = PointBtn.GetComponent<RectTransform>();

        // 按钮随机位置(anchoredPosition)
        float pointWidth = pointTransform.rect.width;
        Vector2 ranPos = new Vector2(Random.Range(width / 3, width - pointWidth / 2), 0);
        pointTransform.anchoredPosition = ranPos;

        // 按钮绑定事件
        PointBtn.onClick.AddListener(TryHit);

        // 计算生命周期并定时销毁
        duration = width / (Speed * 10);
        Destroy(gameObject, duration + 1f);
    }


    private void Update() {
        if (playing) {
            Vector3 hitPos = HitTransform.anchoredPosition;
            hitPos.x += Speed * 10 * Time.deltaTime;
            HitTransform.anchoredPosition = hitPos;

            // 移动到尾部
            if (hitPos.x >= width) {
                playing = false;
                score = 0;
                print("EasyQTE 得分: " + score);
            }
        }
    }


    public override float Play() {
        playing = true;
        return duration;
    }


    public void TryHit() {
        Speed = 0;

        float hitX = HitTransform.anchoredPosition.x;
        float pointX = pointTransform.anchoredPosition.x;
        float pointWidth = pointTransform.rect.width;

        if (hitX > pointX - pointWidth / 2 && hitX < pointX + pointWidth / 2) {
            score = PrizeScore;
        }
        else {
            score = PunishScore;
        }

        print("EasyQTE 得分: " + score);
        Destroy(gameObject, 1f);
    }

}
