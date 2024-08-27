using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;

    public GameObject coinPrefab;

    public GameObject[] prefabs;
    public Transform[] spawnPoints;

    public float spawnInterval = 2f;
    public float minSpawnInterval = 0.8f;
    public float currentSpawnInterval;
    public float changeTime = 0.05f;
    private bool isSpawning;
    private float nextSpawnTime = 0f;

    private List<GameObject> spawnList = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        currentSpawnInterval -= Time.deltaTime * changeTime;
        if(currentSpawnInterval < minSpawnInterval)
            currentSpawnInterval = minSpawnInterval;

        if (isSpawning && Time.time >= nextSpawnTime)
        {
            nextSpawnTime = Time.time + currentSpawnInterval;
            SpawnObject();
        }
    }

    public void StartSpawn()
    {
        isSpawning = true;
        currentSpawnInterval = spawnInterval;
    }

    public void SpawnCoin(Vector3 pos)
    {
        pos.y += 0.5f;
        Spawn(coinPrefab, pos);
    }

    public void Stop()
    {
        isSpawning = false;
    }

    private void SpawnObject()
    {
        if (prefabs.Length > 0 && spawnPoints.Length > 0)
        {
            int randomPrefabIndex = Random.Range(0, prefabs.Length);
            int randomPointIndex = Random.Range(0, spawnPoints.Length);
            GameObject prefab = prefabs[randomPrefabIndex];
            Vector3 pos = spawnPoints[randomPointIndex].position;

            Spawn(prefab, pos);
        }
    }

    private void Spawn(GameObject prefab, Vector3 pos)
    {
        GameObject newObj = Instantiate(prefab, pos, Quaternion.identity, transform);
        spawnList.Add(newObj);
    }

    public void ClearSpawnedObjects()
    {
        foreach (var obj in spawnList)
        {
            if (obj != null)
                Destroy(obj.gameObject);
        }
    }
}
