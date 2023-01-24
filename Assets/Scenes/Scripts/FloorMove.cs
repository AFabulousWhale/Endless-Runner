using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMove : MonoBehaviour
{
    public GameObject movePos, backPlatform, obstacleParent;

    public List<GameObject> obstacles;

    public float fishSpeed = 0.07f;

    public int maxObstacles = 2;

    Vector3 startPos;
    Renderer rend;
    private void Start()
    {
        rend = this.GetComponent<Renderer>();
        startPos = backPlatform.transform.position;
        foreach (Transform obstacle in obstacleParent.transform)
        {
            obstacles.Add(obstacle.gameObject);
        }
    }
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, movePos.transform.position, fishSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "HitBox")
        {
            int obstacleCount;
            foreach (Transform item in this.transform)
            {
                Destroy(item.gameObject);
            }

            obstacleCount = Random.Range(1, (maxObstacles + 1));
            for (int i = 0; i < obstacleCount; i++) //can spawn multiple per platform for more difficult levels
            {
                SpawnObstacles();
            }
            this.transform.position = startPos;
        }
    }

    void SpawnObstacles()
    {
        int visibleObstacle;
        float obstacleSpawnRandomX;
        float obstacleSpawnRandomZ;
        Vector3 obstacleSpawn;
        GameObject currentObstalce;
        obstacleSpawnRandomX = Random.Range(rend.bounds.min.x, rend.bounds.max.x);
        obstacleSpawnRandomZ = Random.Range(rend.bounds.min.z, rend.bounds.max.z);
        obstacleSpawn = new Vector3(obstacleSpawnRandomX, 0, obstacleSpawnRandomZ);
        visibleObstacle = Random.Range(0, obstacles.Count);
        currentObstalce = Instantiate(obstacles[visibleObstacle], obstacleSpawn, Quaternion.identity);
        currentObstalce.transform.parent = this.transform;
    }
}
