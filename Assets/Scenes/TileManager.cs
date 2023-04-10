using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpawn = 18;
    public float tileLength = 40;
    public int tileNumber = 3;
    Vector3 spawnPos;
    void Start()
    {
        SpawnTile(2); //spawns an empty one when the player starts the game
        for (int i = 0; i < tileNumber; i++)
        {
            SpawnTile(Random.Range(0,tilePrefabs.Length)); //picks a random tile to spawn
        }
    }

    void Update()
    {
        
    }

    public void SpawnTile(int tileIndex)
    {
        spawnPos = new Vector3(-6, -2, zSpawn);
        Instantiate(tilePrefabs[tileIndex], spawnPos, transform.rotation);
        zSpawn += tileLength;
    }
}
