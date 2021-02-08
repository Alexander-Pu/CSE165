using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    float timeElapsed = 0;
    bool stopWatchEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        ResetTime();
    }

    private void Update() {
        if (stopWatchEnabled) {
            timeElapsed += Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other) {
        stopWatchEnabled = true;
    }

    public void ResetTime() {
        stopWatchEnabled = false;
        timeElapsed = 0;
    }

    public float getTime() {
        return timeElapsed;
    }
}
