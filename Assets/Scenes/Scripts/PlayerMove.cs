using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    public GameObject deathCanvas, platforms, Shark;
    public TextMeshProUGUI currentScore;
    public Score scoreScript;

    bool sharkStartMoved = true;
    int hitObstacleTimes = 0;
    Vector3 sharkForwardPos, sharkBackwardsPos, moveToPos;
    float timer;
    // Update is called once per frame

    private void Start()
    {
        sharkForwardPos = Shark.transform.position;
        sharkBackwardsPos = new Vector3(Shark.transform.position.x, Shark.transform.position.y, Shark.transform.position.z - 1.5f);
    }

    void Update()
    {
        timer += 0.5f;
        float speed = Input.GetAxis("Horizontal") * 10f;
        this.transform.Translate(speed*Time.deltaTime, 0, 0);

        SharkCheck();
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
                if(hitObstacleTimes == 2) //if you hit an obstacle on the side twice (the shark is forwards and you hit an obstacle again)
                {
                    Death();
                }
                else
                {
                    Stutter();
                }
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
    }

    void Stutter()
    {
        hitObstacleTimes++;
        sharkStartMoved = false;
        MoveShark("forwards"); //moves the shark forwards if you've stuttered
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
        scoreScript.enabled = false;
        currentScore.text = scoreScript.score.ToString("Current Score: 0");
        this.enabled = false;
    }

    //shark movement depending on the player hitting the obstacles:

    void SharkCheck()
    {
        if (timer == 200) //moves the shark backwards after 5 seconds at the start of the round
        {
            hitObstacleTimes = 0; //resets the obstacles hit
            sharkStartMoved = false;
            MoveShark("backwards");
        }

        if (!sharkStartMoved)
        {
            Shark.transform.position = Vector3.MoveTowards(Shark.transform.position, moveToPos, 0.05f);
            if (Shark.transform.position == moveToPos)
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

    void MoveShark(string direction)
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
}
