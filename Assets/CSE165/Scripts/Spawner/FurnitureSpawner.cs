using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject primaryFurniture;
    [SerializeField]
    private GameObject secondaryFurniture;
    [SerializeField]
    private Transform player;

    private GameObject heldGameObject = null;

    void Update()
    {
        // Spawn
        bool aPress = OVRInput.GetDown(OVRInput.Button.Two);
        bool bPress = OVRInput.GetDown(OVRInput.Button.One);

        // aPress is dominant in case of tie
        if (aPress)
        {
            SpawnFurniture(primaryFurniture);
            return;
        } else if (bPress)
        {
            SpawnFurniture(secondaryFurniture);
            return;
        }

        // Release if either button is released. Simplifies logic
        bool aRelease = OVRInput.GetUp(OVRInput.Button.Two);
        bool bRelease = OVRInput.GetUp(OVRInput.Button.One);
        if (aRelease || bRelease)
        {
            ReleaseFurniture();
        }
    }

    private void SpawnFurniture(GameObject prefab)
    {
        if (null == heldGameObject)
        {
            heldGameObject = Instantiate(prefab);
            heldGameObject.transform.SetParent(transform);
            heldGameObject.transform.position = transform.position;
            heldGameObject.transform.LookAt(player);

            Rigidbody rigidBody = heldGameObject.GetComponent<Rigidbody>();
            if (null != rigidBody)
            {
                rigidBody.isKinematic = true;
            }
        }
    }

    private void ReleaseFurniture()
    {
        if (null != heldGameObject)
        {
            Rigidbody rigidBody = heldGameObject.GetComponent<Rigidbody>();
            if (null != rigidBody)
            {
                rigidBody.isKinematic = false;
            }
            heldGameObject.transform.SetParent(null);
            heldGameObject = null;
        }
    }
}
