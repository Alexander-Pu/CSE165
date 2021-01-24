using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDespawner : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Selectable") {
            Destroy(other.gameObject);
        }
    }
}
