using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject deathCanvas, platforms;
    // Update is called once per frame
    void Update()
    {
        float speed = Input.GetAxis("Horizontal") * 10f;
        this.transform.Translate(speed*Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Obstacle")
        {
            Death();
        }
    }

    void Death()
    {
        deathCanvas.SetActive(true);
        foreach (Transform item in platforms.transform)
        {
            if(item.tag == "Platform")
            {
                item.GetComponent<FloorMove>().enabled = false;
            }
        }
        this.enabled = false;
    }
}
