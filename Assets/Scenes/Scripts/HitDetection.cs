using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    AudioSource collision; //will be a different sound depending on if the object is a collectible or obstacle
    public AudioClip soundEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            collision = other.GetComponent<AudioSource>();
            collision.clip = soundEffect;
            collision.Play();
            if (this.tag == "Collectible")
            {
                other.GetComponent<PlayerMove>().scoreScript.score += 100;
                Destroy(this.gameObject);
            }
            if (this.tag == "Obstacle")
            {
                other.GetComponent<PlayerMove>().lives--;
            }
        }
    }
}
