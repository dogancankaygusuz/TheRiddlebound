using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject roadPrefab;  // Yol prefabý
    public GameObject wallPrefab;  // Duvar prefabý
    public Transform player;

    // Ayný anda kaç tane duvar ve yol olacak
    public int numberOfSegments = 5;
    public int numberOfWalls = 10;

    // Yol ve duvar prefab uzunluklarý
    public float segmentLength = 20f;  
    public float wallSpawnInterval = 5f;

    // Aktif yol ve duvar prefablarý
    private List<GameObject> activeRoads = new List<GameObject>();
    private List<GameObject> activeWalls = new List<GameObject>();

    // Son oluþturulan yol ve duvarýn Z pozisyonlarý 
    private float spawnZ = 0f;
    private float wallSpawnZ = 0f;

    void Start()
    {
        // Baþlangýçta 5 tane yol, 10 tane duvar prefabý oluþturduk.
        for (int i = 0; i < numberOfSegments; i++)
        {
            SpawnSegment();
        }
        for (int i = 0; i < numberOfWalls; i++)
        {
            SpawnWall();
        }
    }

    void Update()
    {
        // Player'ýn z pozisyonu öndeki prefablarýn z posizyonunun yakýnýna geldiyse
        // yeni prefablarý oluþtur ve önceki prefablardan sil.
        if (player.position.z > spawnZ - (numberOfSegments * segmentLength))
        {
            SpawnSegment();
            RemoveOldSegment();
        }
        if (player.position.z > wallSpawnZ - (numberOfWalls * wallSpawnInterval))
        {
            SpawnWall();
            RemoveOldWall();
        }
    }

    void SpawnSegment()
    {
        // Yeni bir yol oluþturma ve yeni yolun z posizyonunu atama
        GameObject road = Instantiate(roadPrefab, new Vector3(0, 0, spawnZ), Quaternion.identity);
        activeRoads.Add(road);
        spawnZ += segmentLength;
    }

    void SpawnWall()
    {
        // Yeni bir duvar oluþturma ve yeni yolun z posizyonunu atama
        GameObject walls = Instantiate(wallPrefab, new Vector3(0, 2, wallSpawnZ), Quaternion.identity);
        activeWalls.Add(walls);
        wallSpawnZ += wallSpawnInterval;
    }

    void RemoveOldSegment()
    {
        // Fazla sayýda yol prefabý varsa listenin baþýndaki elemaný sil.
        if (activeRoads.Count > numberOfSegments * 1.5)
        {
            Destroy(activeRoads[0]);
            activeRoads.RemoveAt(0);
        }
    }
    void RemoveOldWall()
    {
        // Fazla sayýda duvar prefabý varsa listenin baþýndaki elemaný sil.
        if (activeWalls.Count > numberOfSegments * 3)
        {
            Destroy(activeWalls[0]);
            activeWalls.RemoveAt(0);
        }
    }
}
