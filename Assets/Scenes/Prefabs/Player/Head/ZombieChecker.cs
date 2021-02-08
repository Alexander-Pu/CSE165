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
    [SerializeField]
    Image reticle;

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
                reticle.color = Color.green;
            } else {
                radialProgressBar.fillAmount = 0;
                reticle.color = Color.white;
            }
        } else {
            radialProgressBar.fillAmount = 0;
            currentZombie = null;
            reticle.color = Color.white;
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
