using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    public GameObject movePos, backPlatform, spawnableParent, objects, wallsParent;

    public List<GameObject> spawnables, movingObjects, walls;

    public float fishSpeed;

    public int maxSpawnables; //this includes obstacles and collectibles

    Vector3 startPos, moveToPos;
    Renderer rend;

    int chanceToSpawnBigObstacle = 1;

    private void Start()
    {
        fishSpeed = 0.25f;
        maxSpawnables = 2;
        //adding each moving object to the list for later use for speed detection
        foreach (Transform item in objects.transform)
        {
            foreach (Transform child in item)
            {
                if (child.tag == "Platform" || child.tag == "Walls")
                {
                    movingObjects.Add(child.gameObject);
                }
            }
        }
        moveToPos = new Vector3(this.transform.position.x, this.transform.position.y, movePos.transform.position.z);
        //get's the size of the floor
        rend = this.GetComponent<Renderer>();
        startPos = backPlatform.transform.position;
        foreach (Transform spawnable in spawnableParent.transform) //if there are multiples objects, it adds it from one parent into a list (this is a prefab)
        {
            spawnables.Add(spawnable.gameObject);
        }

        foreach (Transform wall in wallsParent.transform)
        {
            walls.Add(wall.gameObject);
        }
    }
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, moveToPos, fishSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the platform has reached the end it will spawn new obstacles
        if(other.tag == "HitBox")
        {

            if(this.tag == "Platform") //only spawn obstacles on the floors
            {
                int spawnableCount;
                int canSpawnBig;
                foreach (Transform item in this.transform) //destorys the current collectibles on the platform
                {
                    Destroy(item.gameObject);
                }
                canSpawnBig = chanceToSpawnBigObstacle;//Random.Range(1, 101);
                spawnableCount = Random.Range(1, (maxSpawnables + 1));

                //will sometime spawn the big walls to avoid
                if (canSpawnBig == chanceToSpawnBigObstacle)
                {
                    SpawnBigWall();
                }
                else
                {
                    Debug.Log("spawn obstacle");
                    for (int i = 0; i < spawnableCount; i++) //can spawn multiple per platform for more difficult levels
                    {
                        SpawnSpawnables();
                    }
                }
            }
            this.transform.position = startPos;

            //when the back platform reaches the front "it has done a full lap", then increases the speed for each object
            if (this.gameObject == backPlatform)
            {
                foreach (var item in movingObjects)
                {
                    item.GetComponent<FloorController>().fishSpeed += 0.0025f;
                }
            }
        }
    }

    void SpawnBigWall()
    {
        int visibleObstacle; //index in the list of walls to spawn
        float spawnX = (rend.bounds.max.x / 2);
        float spawnZ = (rend.bounds.max.z / 2);
        Vector3 wallSpawn;
        GameObject currentWall;
        wallSpawn = new Vector3(spawnX, 0, spawnZ);
        visibleObstacle = Random.Range(0, walls.Count);
        currentWall = Instantiate(walls[visibleObstacle], wallSpawn, walls[visibleObstacle].transform.rotation * Quaternion.Euler(0f, 90f, 0f));
        currentWall.transform.parent = this.transform;
    }
    void SpawnSpawnables()
    {
        int visibleObstacle; //index in the list of spawnables to spawn
        float spawnableSpawnRandomX;
        float spawnableSpawnRandomZ;
        float spawnableSpawnRandomY;
        Vector3 spawnableSpawn;
        GameObject currentObstalce;
        spawnableSpawnRandomX = Random.Range(rend.bounds.min.x, rend.bounds.max.x);
        spawnableSpawnRandomZ = Random.Range(rend.bounds.min.z, rend.bounds.max.z);
        spawnableSpawnRandomY = Random.Range(0, 7);
        spawnableSpawn = new Vector3(spawnableSpawnRandomX, spawnableSpawnRandomY, spawnableSpawnRandomZ);
        visibleObstacle = Random.Range(0, spawnables.Count);
        currentObstalce = Instantiate(spawnables[visibleObstacle], spawnableSpawn, Quaternion.identity);
        currentObstalce.transform.parent = this.transform;
    }
}
