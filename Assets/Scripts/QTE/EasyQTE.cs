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

    // 透明进入、消失
    private CanvasGroup canvasGroup;
    private float targetAlpha = 1f;
    private float alphaVelocity = 0;
    private float FadeTime = 0.2f;

    private void Start() {
        width = GetComponent<RectTransform>().rect.width;
        pointTransform = PointBtn.GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        // 按钮随机位置(anchoredPosition)
        float pointWidth = pointTransform.rect.width;
        Vector2 ranPos = new Vector2(Random.Range(width / 3, width - pointWidth / 2), 0);
        pointTransform.anchoredPosition = ranPos;

        // 按钮绑定事件
        PointBtn.onClick.AddListener(TryHit);

        // 透明化 Alpha = 0
        canvasGroup.alpha = 0f;

        // 计算生命周期并定时销毁
        duration = width / (Speed * 10);
        Destroy(gameObject, duration + 1f);
    }


    private void Update() {
        // 透明动画
        if (canvasGroup.alpha != targetAlpha) {
            float alpha = Mathf.SmoothDamp(canvasGroup.alpha, targetAlpha, ref alphaVelocity, FadeTime);
            canvasGroup.alpha = alpha;
            if (Mathf.Abs(alpha - targetAlpha) < 0.0001) {
                canvasGroup.alpha = targetAlpha;
            }
        }

        // EasyQTE 运行
        if (playing) {
            Vector3 hitPos = HitTransform.anchoredPosition;
            hitPos.x += Speed * 10 * Time.deltaTime;
            HitTransform.anchoredPosition = hitPos;

            // 移动到尾部
            if (hitPos.x >= width) {
                playing = false;
                score = 0;
                targetAlpha = 0f;
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

        // 透明化消失
        targetAlpha = 0f;
        FadeTime = 0.35f;

        print("EasyQTE 得分: " + score);
        Destroy(gameObject, 1f);
    }

}
