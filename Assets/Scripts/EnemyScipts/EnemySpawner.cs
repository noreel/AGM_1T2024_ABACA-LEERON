using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    [SerializeField]private float minSpawnTime;
    [SerializeField]private float maxSpawnTime;
    [SerializeField]private float timeUntilSpawn;
    private GameObject player;

    private void Awake()
    {
        SetTimeUntilSpawn();
    }

    
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");


        if (player != null)
        {
            Debug.Log("NotNUll");
            timeUntilSpawn -= Time.deltaTime;

            if (timeUntilSpawn <= 0)
            {
                Instantiate(enemy, transform.position, Quaternion.identity);
                SetTimeUntilSpawn();
            }
        }

    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }

}
