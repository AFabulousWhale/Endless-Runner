using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    public GameObject deathCanvas, platforms;
    public TextMeshProUGUI score;
    public Score scoreScript;
    public Shark sharkScript;
    public Leaderboard leaderboard;

    public int hitObstacleTimes = 0;

    public bool hitOnce;

    void Update()
    {
        float speed = Input.GetAxis("Horizontal") * 10f;
        this.transform.Translate(speed*Time.deltaTime, 0, 0);

        HitCheck();
    }

    void HitCheck()
    {
        RaycastHit hit; //if you hit the obstacles straight on
        Debug.DrawRay(transform.position, Vector3.forward, Color.green, 1);
        Debug.DrawRay(transform.position, Vector3.right, Color.red, 1);
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 1))
        {
            if(hit.transform.gameObject.tag == "Obstacle")
            {
                Death();
            }
            else if (hit.transform.gameObject.tag == "Collectible")
            {
                Collect(hit.transform.gameObject);
            }
            hitOnce = true;
        }
        else if (Physics.Raycast(transform.position, Vector3.right, out hit, 1) || Physics.Raycast(transform.position, Vector3.left, out hit, 1))
        {
            if(hit.transform.gameObject.tag == "Obstacle")
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
            hitOnce = true;
        }
        else
        {
            hitOnce = false;
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
        sharkScript.sharkStartMoved = false;
        sharkScript.MoveShark("forwards"); //moves the shark forwards if you've stuttered
    }

    void Death()
    {
        //making it so each platform and the player can't move when the player is dead
        deathCanvas.SetActive(true);
        foreach (Transform item in platforms.transform)
        {
            foreach (Transform child in item)
            {
                if(child.tag == "Platform" || child.tag == "Walls")
                {
                    child.GetComponent<FloorController>().enabled = false;
                }
            }
        }
        scoreScript.enabled = false;
        leaderboard.CompareScore(scoreScript.score);
        if(leaderboard.currentScores[0] != scoreScript.score)
        {
            score.text = $"Current Score: {scoreScript.score.ToString()}<br> High Score: {leaderboard.currentScores[0]}";
        }
        else
        {
            score.text = $"NEW HIGH SCORE: {leaderboard.currentScores[0]}";
        }
        leaderboard.OnDeath();

        this.enabled = false;
    }
}
