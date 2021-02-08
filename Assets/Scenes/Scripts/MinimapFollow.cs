using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    [SerializeField]
    Transform head;
    [SerializeField]
    Transform body;

    // Update is called once per frame
    void Update()
    {
        transform.position = head.position + new Vector3(0, 500, 0);
        transform.forward = new Vector3(body.forward.x, 0, body.forward.z);
    }
}
