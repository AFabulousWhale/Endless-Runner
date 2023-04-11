using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shark : MonoBehaviour
{
    public GameObject Player;
    public PlayerMove playerMove;
    private CameraFollow followScript;

    public Vector3 backOffset, fwdOffset;

    private void Start()
    {
        followScript = this.GetComponent<CameraFollow>();
        fwdOffset = transform.position - Player.transform.position;
        backOffset.z = (fwdOffset.z - 30); //this will make the shark behind the player
    }
    void Update()
    {
        //if on 1 life shark is there
        if (!Cinematic.playingCinematic)
        {
            if (playerMove.lives <= 1)
            {
                followScript.offset.z = fwdOffset.z;
            }
            else
            {
                followScript.offset.z = backOffset.z;
            }
        }
    }
}
