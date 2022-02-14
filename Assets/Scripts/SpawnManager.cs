using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> ridePrefab;
    private float startDelay = 0.5f;
    private float repeatRate = 1.5f;
    private GameManager gameManager;

    void Start()
    {
        InvokeRepeating("InstantiatingRides", startDelay, repeatRate);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    private void InstantiatingRides() // For the spawning of different Rides
    {
        if (gameManager.gameStop == false)
        {
            int rideToSpawn = Random.Range(0, ridePrefab.Count);
            Vector3 spawnPos = new Vector3(25f, 0.5f, Random.Range(-3, 4));
            Instantiate(ridePrefab[rideToSpawn], spawnPos, ridePrefab[rideToSpawn].transform.rotation);
        }

    }
}
