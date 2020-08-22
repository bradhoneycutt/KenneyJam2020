using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    public float enemyRate = 5f;
    float nextEnemy = 1f;
    public float spawnDistance = 20f;
    public float proximtySpawn = 25f;

    public int directionX = 0;
    public int directionY = 0;

    private GameObject player;



    //todo change to array of prefabs to select from ...
    public GameObject[] enemyPrefab;

    private void Start()
    {
        player = GameObject.Find("Player");
    }


    // Update is called once per frame
    void Update()
    {

        if (player == null)
        {
            player = GameObject.Find("Player");
        }

        if (player != null)
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);
            //  Debug.Log(dist);
            nextEnemy -= Time.deltaTime;
            if (nextEnemy <= 0 && (player != null && dist < proximtySpawn))
            {
                nextEnemy = enemyRate;
                enemyRate *= 0.9f;
                SpawnEnemy();
            }
        }

    }

    void SpawnEnemy()
    {
        var obj = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], transform.position, Quaternion.identity);

        obj.GetComponent<ProjectileMovement>().SetDirection(directionX, directionY);


    }

}
