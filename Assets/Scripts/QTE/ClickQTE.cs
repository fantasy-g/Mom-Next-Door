using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickQTE : QTE {

    public float MaxDuration = 8f;
    public float DownSpeed = 0.5f;
    public float UpSpeed = 150f;
    public int PrizeScore = 10;
    public int PunishScore = -5;
    public KeyCode Key;

    public RectTransform PointTransform;

    private float rawHeight;
    private bool playing = false;
    private float timer = 0;


    private void Awake() {
        rawHeight = GetComponent<RectTransform>().rect.height;
    }

    private void Update() {

        // 销毁自身倒计时
        if (timer > 0) {
            timer -= Time.deltaTime;
        }
        else if (timer < 0) {
            timer = 0;
            Finish();
        }

        if (playing) {
            // 本次 Update 后 Point 的目标高度
            float height = PointTransform.rect.height;
            height -= DownSpeed * Time.deltaTime;      // height 先自减

            // 若按下按键则上升
            if (Input.GetKeyDown(Key)) {
                height += UpSpeed * 10 * Time.deltaTime;
            }

            // 高度修改赋值
            height = height > rawHeight ? rawHeight : height;
            height = height < 0 ? 0 : height;
            PointTransform.sizeDelta = new Vector2(0, height);

            // 判断输赢
            if (height == rawHeight) {
                score = PrizeScore;
                Finish();
            }
            else if (height == 0) {
                score = PunishScore;
                Finish();
            }
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

    private void Finish() {
        Debug.Log("Finish ClickQTE with score : " + score);
        ExecuteEvents.Execute<IPointerClickHandler>(
            GetComponent<Button>().gameObject, 
            new PointerEventData(EventSystem.current), 
            ExecuteEvents.pointerClickHandler
            );
        Destroy(gameObject, 0.1f);
    }

}
