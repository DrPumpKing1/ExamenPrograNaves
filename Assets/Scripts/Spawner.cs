using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnRate = 2f;
    public float enemyDestroyTime = 3f;
    public GameObject enemyPrefab;
    [SerializeField] private float timer;

    void Update()
    {
        if(timer >= 0) timer -= Time.deltaTime;
        else
        {
            timer = 1 /spawnRate;

            Destroy(Instantiate(enemyPrefab, transform.position, Quaternion.identity), enemyDestroyTime);
        }
    }
}
