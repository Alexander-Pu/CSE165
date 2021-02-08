using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    float highScore = float.MaxValue;

    [SerializeField]
    Stopwatch stopwatch;
    [SerializeField]
    Score highScoreObj;
    [SerializeField]
    Score currScoreObj;

    // Update is called once per frame
    void Update()
    {
        currScoreObj.setScore(stopwatch.getTime());
    }

    public void setHighScore() {
        highScore = Mathf.Min(highScore, stopwatch.getTime());
        highScoreObj.setScore(highScore);
    }
}
