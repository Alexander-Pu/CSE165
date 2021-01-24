using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialDistanceChecker : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;

    void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "Selectable") {
            ZombieMask zombieMask = other.gameObject.GetComponent<ZombieMask>();
            if (zombieMask) {
                if (!zombieMask.isMasked()) {
                    gameManager.endGame();
                }
            }
        }
    }
}
