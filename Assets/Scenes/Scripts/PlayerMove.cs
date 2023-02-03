using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject deathCanvas, platforms;
    public Score scoreScript;
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
            RaycastHit hit; //if you hit the obstacles straight on
            if (Physics.Raycast(transform.position, Vector3.forward, out hit, 1))
            {
                Death();
            }
            else
            {
                Stutter();
            }
        }
        if (other.tag == "Collectible")
        {
            Collect(other.gameObject);
        }
    }

    void Collect(GameObject collectible)
    {
        Destroy(collectible);
        scoreScript.score += 100;

        Debug.Log("collect");
    }

    void Stutter()
    {

    }

    void Death()
    {
        //making it so each platform and the player can't move when the player is dead
        deathCanvas.SetActive(true);
        foreach (Transform item in platforms.transform)
        {
            if(item.tag == "Platform")
            {
                item.GetComponent<FloorController>().enabled = false;
            }
        }
        this.enabled = false;
    }
}
