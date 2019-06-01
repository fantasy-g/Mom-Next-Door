using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour {

    public float FadeTime = 0.35f;
    public Text Text;

    private Image background;
    private float targetAlpha = 0f;
    private float alphaVelocity = 0;

    private float timer = 0f;


    private void Start() {
        background = GetComponent<Image>();
        Fade(0f);   // 先隐身
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
        if (background.color.a != targetAlpha) {
            float alpha = Mathf.SmoothDamp(background.color.a, targetAlpha, ref alphaVelocity, FadeTime);
            Fade(alpha);
        }
    }

    public void Bub(string text,float time = 3f) {
        Text.text = text;
        timer = time;
    }

    // 辅助--调整物体及子物体Alpha透明度
    private void Fade(float alpha) {
        Color BGColor = background.color;
        Color TextColor = Text.color;
        BGColor.a = alpha;
        TextColor.a = alpha;
        background.color = BGColor;
        Text.color = TextColor;
    }

}
