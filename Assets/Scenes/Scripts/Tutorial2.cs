using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class Tutorial2 : MonoBehaviour
{
    //script will be on each trigger for the tutorial
    public GameObject UIToShow;
    public KeyCode keyToRelease;

    GameObject player;

    bool waitingForInput;

    private void Update()
    {
        if (waitingForInput)
        {
            if (Input.GetKey(keyToRelease))
            {
                Freeze();
            }
            else
            {
                Time.timeScale = 1;
                player.GetComponent<PlayerMovementTutorial>().shouldFall = true; //will make the player fall
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        UIToShow.SetActive(false);
        waitingForInput = false;
    }

    private void OnTriggerEnter(Collider other) //once player has collided it stops time
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            Freeze();
        }
    }

    void Freeze()
    {
        Time.timeScale = 0;
        UIToShow.SetActive(true);
        waitingForInput = true;
    }
}
