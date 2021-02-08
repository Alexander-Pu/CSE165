using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Stopwatch stopwatch;
    [SerializeField]
    ScoreTracker scoreTracker;
    [SerializeField]
    CharacterController player;
    [SerializeField]
    Transform head;
    [SerializeField]
    Transform spawnLocation;

    public void endGame() {
        scoreTracker.setHighScore();
        stopwatch.ResetTime();
        ResetPosition();
    }

    private void Start() {
        ResetPosition();
    }

    private void ResetPosition() {
        player.enabled = false;
        player.transform.position = spawnLocation.position;
        player.transform.rotation = Quaternion.Euler(0, 114.8f, 0);
        head.rotation = Quaternion.Euler(0, 114.8f, 0);
        player.enabled = true;
    }
}
