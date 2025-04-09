using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obstaclePrefab; // Engel prefabý
    public Transform player;

    // Engel uzunlugu, araligi, sýklýgý, sayýsý, listesi, yeni konumu
    public float spawnDistance = 60f;
    public float minX = -1.5f, maxX = 1.5f;
    public float intervalZ = 10f;
    public int poolSize = 10;
    private List<GameObject> obstaclePool = new List<GameObject>();
    private float nextSpawnZ;

    void Start()
    {
        nextSpawnZ = player.position.z + spawnDistance;

        // Baslangicta 10 tane olusturma
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(obstaclePrefab, new Vector3(0, 0, -100), Quaternion.identity);
            obj.SetActive(false);
            obstaclePool.Add(obj);
        }
    }
    
    void Update()
    {
        // Engel uretmeye devam etmek icin
        if (player.position.z + spawnDistance >= nextSpawnZ)
        {
            SpawnObstacle();
            nextSpawnZ += intervalZ;
        }

        // Kullanýlmýs engelleri tek tek kontrol etme
        foreach (GameObject obj in obstaclePool)
        {
            if (obj.activeInHierarchy && obj.transform.position.z < player.position.z - 20f)
            {
                obj.SetActive(false);
            }
        }
    }

    // Random olarak engel olusturma metotu
    void SpawnObstacle()
    {
        GameObject obj = GetPooledObstacle();
        if (obj != null)
        {
            float randomX = Random.Range(minX, maxX);
            obj.transform.position = new Vector3(randomX, 0f, nextSpawnZ);
            obj.transform.rotation = Quaternion.Euler(0, 90, 0); // Döndür
            obj.SetActive(true);
        }
    }

    // Engelleri ekleme, elimizde engel yoksa yeni engel olusturma metotu
    GameObject GetPooledObstacle()
    {
        foreach (GameObject obj in obstaclePool)
        {
            if (!obj.activeInHierarchy)
                return obj;
        }
        GameObject newObstacle = Instantiate(obstaclePrefab, new Vector3(0, 0, -100), Quaternion.identity);
        newObstacle.SetActive(false);
        obstaclePool.Add(newObstacle);
        return newObstacle;
    }
}
