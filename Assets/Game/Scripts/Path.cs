using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    GroundSpawner groundSpawner;
    public GameObject coinPrefab;
    public GameObject spikePrefab;
    public GameObject[] rocks = new GameObject[4];
    public GameObject[] grass = new GameObject[2];
    public GameObject[] obstaclePrefab = new GameObject[4];


    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        SpawnSpike();
        SpawnObstacle();
        SpawnCoins();
        SpawnRocks();
        SpawnGrass();
    }

    void OnTriggerExit(Collider other)
    {
        groundSpawner.SpawnTitle();
        Destroy(gameObject, 2);
    }


    void Update()
    {
        
    }

    public void SpawnSpike()
    {
        //random position
        int spikeSpawnIndex = Random.Range(1, 4);
        Transform spawnSPoint = transform.GetChild(spikeSpawnIndex).transform;

        Instantiate(spikePrefab, spawnSPoint.position, Quaternion.identity, transform);
    }

    public void SpawnObstacle()
    {
        // position
        Transform spawnOPoint = transform.GetChild(4).transform;
        int index = Random.Range(0, 4);
        Instantiate(obstaclePrefab[index], spawnOPoint.position, Quaternion.identity, transform);
    }

    public void SpawnCoins()
    {
        int coinAmount = 3;
        for (int i = 0; i < coinAmount; i++)
        {
            GameObject temp = Instantiate(coinPrefab, transform);
            temp.transform.position = GetRandomPointCoin(GetComponent<Collider>());
        }
    }

    public void SpawnRocks()
    {
        int rockAmount = 2;
        for (int i = 0; i < rockAmount; i++)
        {
            int index = Random.Range(0, 4);
            GameObject tempr = Instantiate(rocks[index], transform);
            tempr.transform.position = GetRandomPoint(GetComponent<Collider>());
        }
    }

    public void SpawnGrass()
    {
        int grassAmount = 3;
        for (int i = 0; i < grassAmount; i++)
        {
            int index = Random.Range(0, 2);
            GameObject tempg = Instantiate(grass[index], transform);
            tempg.transform.position = GetRandomPointGrass(GetComponent<Collider>());
        }
    }

    Vector3 GetRandomPointCoin(Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x + 0.5f, collider.bounds.max.x - 0.5f),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );
        if(point != collider.ClosestPoint(point))
        {
            point = GetRandomPointCoin(collider);
        }

        point.y = 1;

        return point;
    }

    Vector3 GetRandomPoint(Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x + 0.5f, collider.bounds.max.x - 0.5f),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );
        if (point != collider.ClosestPoint(point))
        {
            point = GetRandomPoint(collider);
        }

        point.y = -0.05f;

        return point;
    }

    Vector3 GetRandomPointGrass(Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x + 1.8f, collider.bounds.max.x - 1.7f),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );
        if (point != collider.ClosestPoint(point))
        {
            point = GetRandomPointGrass(collider);
        }

        point.y = 0.25f;

        return point;
    }
}
