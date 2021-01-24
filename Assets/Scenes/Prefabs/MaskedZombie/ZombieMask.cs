using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMask : MonoBehaviour
{
    bool masked;
    AttachableObject mask;

    // Start is called before the first frame update
    void Start()
    {
        masked = false;
        mask = this.gameObject.GetComponentInChildren<AttachableObject>();
    }

    public void DisplayMask() {
        masked = true;
        mask.SetDisplay(true);
    }
    public bool isMasked() {
        return masked;
    }
}
