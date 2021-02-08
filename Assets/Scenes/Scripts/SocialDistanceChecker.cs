using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialDistanceChecker : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;

    void OnTriggerEnter(Collider other) {
        gameManager.endGame();
    }
}
