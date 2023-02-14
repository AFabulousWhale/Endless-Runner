using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public PlayerMove playerMove;
    public GameObject player;
    float timer;

    public bool sharkStartMoved = true;
    Vector3 sharkForwardPos, sharkBackwardsPos, moveToPos, playerPos;

    private void Start()
    {
        sharkForwardPos = this.transform.position;
        sharkBackwardsPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1.5f);
    }

    void Update()
    {
        SharkCheck();
        FollowPlayer();
        timer += 0.5f;
    }
    //shark movement depending on the player hitting the obstacles:

    public void SharkCheck()
    {
        if (timer == 200) //moves the shark backwards after 5 seconds at the start of the round
        {
            playerMove.hitObstacleTimes = 0; //resets the obstacles hit
            sharkStartMoved = false;
            MoveShark("backwards");
        }

        if (!sharkStartMoved)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, moveToPos, 0.05f);
            if (this.transform.position == moveToPos)
            {
                //if the shark is facing forwards and has reached the destination then the timer will be reset but if the player hits another obstacle in that time then they die
                if (moveToPos == sharkForwardPos)
                {
                    timer = 0;
                }
                sharkStartMoved = true; //boolean is used to make sure the shark moving is called once it reached it's desired position
            }
        }
    }

    public void MoveShark(string direction)
    {
        switch(direction)
        {
            case "forwards":
                moveToPos = sharkForwardPos;
                break;
            case "backwards":
                moveToPos = sharkBackwardsPos;
                break;
        }
    }

    public void FollowPlayer()
    {
        playerPos = new Vector3(player.transform.position.x, this.transform.position.y, this.transform.position.z);
        this.transform.position = Vector3.MoveTowards(this.transform.position, playerPos, 0.05f);
    }
}
