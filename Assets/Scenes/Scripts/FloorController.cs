using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    public GameObject movePos, backPlatform, spawnableParent;

    public List<GameObject> spawnables;

    public float fishSpeed = 0.07f;

    public int maxSpawnables = 2; //this includes obstacles and collectibles

    Vector3 startPos, moveToPos;
    Renderer rend;

    private void Start()
    {
        moveToPos = new Vector3(this.transform.position.x, this.transform.position.y, movePos.transform.position.z);
        rend = this.GetComponent<Renderer>();
        startPos = backPlatform.transform.position;
        foreach (Transform spawnable in spawnableParent.transform) //if there are multiples objects, it adds it from one parent into a list (this is a prefab)
        {
            spawnables.Add(spawnable.gameObject);
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
                foreach (Transform item in this.transform) //destorys the current collectibles on the platform
                {
                    Destroy(item.gameObject);
                }

                spawnableCount = Random.Range(1, (maxSpawnables + 1));
                for (int i = 0; i < spawnableCount; i++) //can spawn multiple per platform for more difficult levels
                {
                    SpawnSpawnables();
                }
            }
            this.transform.position = startPos;
            fishSpeed += 0.005f;
        }
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
