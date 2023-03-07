using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public PlayerMove playerMove;
    public GameObject player;

    public bool sharkStartMoved = true;
    public Vector3 sharkForwardPos, sharkBackwardsPos, moveToPos, playerPos;

    public string direction;

    private void Start()
    {
        direction = "forwards";
        StartCoroutine(timer());
    }
    void Update()
    {
        sharkForwardPos = new Vector3(player.transform.position.x, this.transform.position.y, -4.44f);
        sharkBackwardsPos = new Vector3(player.transform.position.x, this.transform.position.y, -11.81f);
        SharkCheck();
        FollowPlayer();

        if(direction == "backwards")
        {
            moveToPos = sharkBackwardsPos;
        }
        else if (direction == "forwards")
        {
            moveToPos = sharkForwardPos;
        }
    }
    //shark movement depending on the player hitting the obstacles:

    IEnumerator timer()
    {
        yield return new WaitForSeconds(5);
        playerMove.hitObstacleTimes = 0; //resets the obstacles hit
        sharkStartMoved = false;
        direction = "backwards";
    }

    public void SharkCheck()
    {

        if (!sharkStartMoved)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, moveToPos, 0.05f);
            if (this.transform.position == moveToPos)
            {
                //if the shark is facing forwards and has reached the destination then the timer will be reset but if the player hits another obstacle in that time then they die
                if (direction == "forwards")
                {
                    Debug.Log("moveback");
                    StartCoroutine(timer());
                }
                sharkStartMoved = true; //boolean is used to make sure the shark moving is called once it reached it's desired position
            }
        }
    }

    public void FollowPlayer()
    {
        playerPos = new Vector3(player.transform.position.x, moveToPos.y, moveToPos.z);
        this.transform.position = Vector3.MoveTowards(this.transform.position, playerPos, 0.05f);
    }
}
