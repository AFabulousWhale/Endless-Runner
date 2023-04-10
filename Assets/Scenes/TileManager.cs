using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpawn = 18;
    public float tileLength = 40;
    public int tileNumber = 5;
    private List<GameObject> activeTiles = new List<GameObject>();
    Vector3 spawnPos;

    public Transform playerTransform;
    void Start()
    {
        SpawnTile(0); //spawns an empty one when the player starts the game
        for (int i = 0; i < tileNumber; i++)
        {
            SpawnTile(Random.Range(0,tilePrefabs.Length)); //picks a random tile to spawn
        }
    }

    void Update()
    {
        if(playerTransform.position.z -45 > zSpawn-(tileNumber * tileLength)) //spawns a new tile when the player gets to close to the end of the generation
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length)); //picks a random tile to spawn
            DeleteTile();
        }
    }

    public void SpawnTile(int tileIndex)
    {
        spawnPos = new Vector3(-6, -2, zSpawn);
        GameObject newTile = Instantiate(tilePrefabs[tileIndex], spawnPos, transform.rotation);
        zSpawn += tileLength;
        activeTiles.Add(newTile);
    }

    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
