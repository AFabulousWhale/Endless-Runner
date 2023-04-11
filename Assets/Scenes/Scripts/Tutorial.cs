using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class Tutorial : MonoBehaviour
{
    //script will be on each trigger for the tutorial
    public GameObject UIToShow;
    public KeyCode keyToPress1, keyToPress2;

    bool waitingForInput;

    private void Update()
    {
        if(waitingForInput)
        {
            if(Input.GetKey(keyToPress1) || Input.GetKey(keyToPress2))
            {
                Time.timeScale = 1;
            }
            else
            {
                Freeze();
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
        if(other.tag == "Player")
        {
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
