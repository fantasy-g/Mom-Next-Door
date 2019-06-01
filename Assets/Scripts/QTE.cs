using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTE : MonoBehaviour {

    protected int score = 404;

    public virtual float Play() {
        return 0;
    }

    public virtual int GetScore() {
        return score;
    }

    public virtual Button GetButton() {
        return null;
    }

}
