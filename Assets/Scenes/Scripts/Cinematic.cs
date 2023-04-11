using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic : MonoBehaviour
{
    public static bool playingCinematic;

    public GameObject fish, shark;
    // Start is called before the first frame update
    void Start()
    {
        playingCinematic = true;
        StartCoroutine(WaitforAnim());
    }

    IEnumerator WaitforAnim()
    {
        yield return new WaitForSeconds(2.7f);
        StopCinematic();
    }

    void StopCinematic()
    {
        playingCinematic = false;

        Camera.main.GetComponent<Animator>().enabled = false;
        fish.GetComponent<Animator>().enabled = false;
        shark.GetComponent<Animator>().enabled = false;
    }
}
