using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieChecker : MonoBehaviour
{
    int layerMask;
    GameObject currentZombie;
    float secondsOnZombie;
    [SerializeField]
    Image radialProgressBar;

    private void Start() {
        layerMask = 1 << 8;
        secondsOnZombie = 0;
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask)) {
            GameObject hitZombie = hit.collider.gameObject;

            ZombieMask mask = hitZombie.GetComponent<ZombieMask>();

            if (!mask.isMasked()) {
                handleUnmaskedZombie(hitZombie, mask);
            } else {
                radialProgressBar.fillAmount = 0;
            }
        } else {
            radialProgressBar.fillAmount = 0;
            currentZombie = null;
        }
    }

    private void handleUnmaskedZombie(GameObject hitZombie, ZombieMask mask) {
        if (currentZombie != hitZombie) {
            currentZombie = hitZombie;
            secondsOnZombie = 0;
        }

        secondsOnZombie += Time.deltaTime;
        
        radialProgressBar.fillAmount = Mathf.Min(secondsOnZombie / 2f, 1);

        if (secondsOnZombie >= 2) {
            mask.DisplayMask();
        }
    }
}
