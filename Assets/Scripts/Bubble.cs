using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour {

    public float FadeTime = 0.35f;
    public Text Text;

    private CanvasGroup canvasGroup;
    private float targetAlpha = 0f;
    private float alphaVelocity = 0;

    private float timer = 0f;


    private void Start() {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    private void Update() {
        // 定时
        if (timer > 0) {
            timer -= Time.deltaTime;
            targetAlpha = 1f;
        }
        else {
            targetAlpha = 0f;
        }

        // 淡化动画
        if (canvasGroup.alpha != targetAlpha) {
            float alpha = Mathf.SmoothDamp(canvasGroup.alpha, targetAlpha, ref alphaVelocity, FadeTime);
            canvasGroup.alpha = alpha;
            if (Mathf.Abs(alpha - targetAlpha) < 0.0001) {
                canvasGroup.alpha = targetAlpha;
            }
        }
    }

    public void Bub(string text,float time = 3f) {
        Text.text = text;
        timer = time;
    }

}
