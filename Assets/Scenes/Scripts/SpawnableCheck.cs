using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableCheck : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Collectible" || other.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
    }
}
