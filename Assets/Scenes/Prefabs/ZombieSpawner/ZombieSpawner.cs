using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    float spawnRate = 3f;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine() {
        bool left = Random.Range(0.0f, 1.0f) < 0.5f;

        Vector3 randomOffset = new Vector3(left ? -1.5f : 1.5f, 0, 0);

        Instantiate(prefab, this.transform.position + randomOffset, this.transform.rotation);

        yield return new WaitForSeconds(spawnRate);

        StartCoroutine(SpawnCoroutine());
    }
}
