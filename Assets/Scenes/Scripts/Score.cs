using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]
    Text text;
    string originalText;

    private void Start() {
        originalText = text.text;
    }

    public void setScore(float score) {
        text.text = originalText + (int) score;
    }
}
