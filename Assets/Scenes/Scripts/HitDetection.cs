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
                if (other.GetComponent<PlayerMove>().fillingHeart1) //if get hit whilst first heart filling
                {
                    other.GetComponent<PlayerMove>().Death();
                }
                if (other.GetComponent<PlayerMove>().fillingHeart2) //if you get hit whilst the heart is filling
                {
                    if (other.GetComponent<PlayerMove>().heart2.fillAmount > 0.1) //if the heart has some fill
                    {
                        other.GetComponent<PlayerMove>().heart1.fillAmount = other.GetComponent<PlayerMove>().heart2.fillAmount; //sets the first heart fill to the second
                        other.GetComponent<PlayerMove>().heart2.fillAmount = 0;
                        other.GetComponent<PlayerMove>().fillingHeart2 = false; //stop filling the second heart
                        other.GetComponent<PlayerMove>().fillingHeart1 = true;
                    }
                    else
                    {
                        other.GetComponent<PlayerMove>().Death();
                    }
                }
                if (!other.GetComponent<PlayerMove>().fillingHeart1 && !other.GetComponent<PlayerMove>().fillingHeart2) //if the player is on 2 lives (full) but gets hit
                {
                    other.GetComponent<PlayerMove>().heart2.fillAmount = 0;
                    other.GetComponent<PlayerMove>().fillingHeart2 = true; //fills the second heart (one on right)
                }
            }
        }
    }
}
