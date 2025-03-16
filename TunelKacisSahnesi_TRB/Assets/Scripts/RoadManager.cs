using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject roadPrefab;  // Yol prefab�
    public GameObject wallPrefab;  // Duvar prefab�
    public Transform player;

    // Ayn� anda ka� tane duvar ve yol olacak
    public int numberOfSegments = 5;
    public int numberOfWalls = 10;

    // Yol ve duvar prefab uzunluklar�
    public float segmentLength = 20f;  
    public float wallSpawnInterval = 5f;

    // Aktif yol ve duvar prefablar�
    private List<GameObject> activeRoads = new List<GameObject>();
    private List<GameObject> activeWalls = new List<GameObject>();

    // Son olu�turulan yol ve duvar�n Z pozisyonlar� 
    private float spawnZ = 0f;
    private float wallSpawnZ = 0f;

    void Start()
    {
        // Ba�lang��ta 5 tane yol, 10 tane duvar prefab� olu�turduk.
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
        // Player'�n z pozisyonu �ndeki prefablar�n z posizyonunun yak�n�na geldiyse
        // yeni prefablar� olu�tur ve �nceki prefablardan sil.
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
        // Yeni bir yol olu�turma ve yeni yolun z posizyonunu atama
        GameObject road = Instantiate(roadPrefab, new Vector3(0, 0, spawnZ), Quaternion.identity);
        activeRoads.Add(road);
        spawnZ += segmentLength;
    }

    void SpawnWall()
    {
        // Yeni bir duvar olu�turma ve yeni yolun z posizyonunu atama
        GameObject walls = Instantiate(wallPrefab, new Vector3(0, 2, wallSpawnZ), Quaternion.identity);
        activeWalls.Add(walls);
        wallSpawnZ += wallSpawnInterval;
    }

    void RemoveOldSegment()
    {
        // Fazla say�da yol prefab� varsa listenin ba��ndaki eleman� sil.
        if (activeRoads.Count > numberOfSegments * 1.5)
        {
            Destroy(activeRoads[0]);
            activeRoads.RemoveAt(0);
        }
    }
    void RemoveOldWall()
    {
        // Fazla say�da duvar prefab� varsa listenin ba��ndaki eleman� sil.
        if (activeWalls.Count > numberOfSegments * 3)
        {
            Destroy(activeWalls[0]);
            activeWalls.RemoveAt(0);
        }
    }
}
