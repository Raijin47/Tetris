using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public Transform[] spawnPoints;

    public float spawnInterval = 2f;

    private bool isSpawning;
    private float nextSpawnTime = 0f;

    private List<GameObject> spawnList = new List<GameObject>();

    private void Update()
    {
        if (isSpawning && Time.time >= nextSpawnTime)
        {
            nextSpawnTime = Time.time + spawnInterval;
            SpawnObject();
        }
    }

    public void StartSpawn()
    {
        isSpawning = true;
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

            GameObject newObj = Instantiate(prefab, pos, Quaternion.identity, transform);
            spawnList.Add(newObj);
        }
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
