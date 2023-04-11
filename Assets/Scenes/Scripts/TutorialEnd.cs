using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnd : MonoBehaviour
{
    public GameObject UIToShow;
    private void OnTriggerEnter(Collider other) //once player has collided it stops time
    {
        if (other.tag == "Player")
        {
            Time.timeScale = 0;
            UIToShow.SetActive(true);
        }
    }
}
