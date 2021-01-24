using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool gameActive = true;
    [SerializeField]
    float timeElapsed = 0f;
    [SerializeField]
    Canvas gameOverScreen;
    [SerializeField]
    GameObject scoreText;
    [SerializeField]
    GameObject restartText;
    [SerializeField]
    Renderer ground;

    // Start is called before the first frame update
    void Start()
    {
        startState();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive) {
            timeElapsed += Time.deltaTime;
        }
    }

    public void endGame() {
        gameActive = false;
        Time.timeScale = 0;
        gameOverScreen.enabled = true;
        ground.enabled = false;
        scoreText.GetComponent<Score>().setScore(timeElapsed);

        StartCoroutine(setRestartTimer(5));
    }

    public void startState() {
        gameActive = true;
        timeElapsed = 0f;
        Time.timeScale = 1;
        gameOverScreen.enabled = false;
        ground.enabled = true;
    }

    public void restart() {
        startState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public IEnumerator setRestartTimer(int timeLeft) {
        restartText.GetComponent<Score>().setScore(timeLeft);

        if (timeLeft > 0) {
            yield return new WaitForSecondsRealtime(1);
            StartCoroutine(setRestartTimer(timeLeft - 1));
        } else {
            restart();
        }
    }
}
