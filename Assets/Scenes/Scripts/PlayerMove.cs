using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    public GameObject deathCanvas, platforms, hitObject;
    public TextMeshProUGUI score;
    public Score scoreScript;
    public Shark sharkScript;
    public Leaderboard leaderboard;

    public int hitObstacleTimes = 0;

    public AudioSource mainMusic, deathMusic;

    public AudioClip death;

    void Update()
    {
        float speed = Input.GetAxis("Horizontal") * 10f;
        this.transform.Translate(speed*Time.deltaTime, 0, 0);
    }

    public void Stutter()
    {
        hitObstacleTimes++;
        sharkScript.sharkStartMoved = false;
        sharkScript.MoveShark("forwards"); //moves the shark forwards if you've stuttered
    }
    
    public void Death()
    {
        this.GetComponent<AudioSource>().clip = death;
        this.GetComponent<AudioSource>().Play();
        mainMusic.Stop();
        deathMusic.Play();
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
        sharkScript.enabled = false;
        this.enabled = false;
    }
}
