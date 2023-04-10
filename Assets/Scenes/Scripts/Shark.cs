using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shark : MonoBehaviour
{
    public Image heart;
    public PlayerMove playerMove;
    public GameObject player;

    public bool sharkStartMoved = true;
    public Vector3 sharkForwardPos, sharkBackwardsPos, moveToPos, playerPos;

    public string direction;

    bool canfill;

    private void Start()
    {
        canfill = true;
        direction = "forwards";
    }
    void Update()
    {
        sharkForwardPos = new Vector3(player.transform.position.x, player.transform.position.y, -4.44f);
        sharkBackwardsPos = new Vector3(player.transform.position.x, player.transform.position.y, -11.81f);
        SharkCheck();
        FollowPlayer();

        if(canfill)
        {
            FillHeart();
        }

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

    void ResetShark()
    {
        playerMove.hitObstacleTimes = 0; //resets the obstacles hit
        sharkStartMoved = false;
        direction = "backwards";
    }

    void FillHeart()
    {
        ///if the heart isn't full
        if (heart.fillAmount != 1)
        {
            heart.fillAmount += 0.001f;
        }
        else
        {
            canfill = false;
            ResetShark();
        }
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
                    canfill = true;
                }
                sharkStartMoved = true; //boolean is used to make sure the shark moving is called once it reached it's desired position
            }
        }
    }

    public void FollowPlayer()
    {
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y, moveToPos.z);
        this.transform.position = Vector3.MoveTowards(this.transform.position, playerPos, 0.05f);
    }
}
