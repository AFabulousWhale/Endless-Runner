using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public GameObject deathCanvas, platforms, hitObject;
    public TextMeshProUGUI score;
    public Score scoreScript;
    public Shark sharkScript;
    public Leaderboard leaderboard;

    public Image heart1, heart2;

    public int hitObstacleTimes = 0;

    public AudioSource mainMusic, deathMusic;

    public AudioClip death;

    void Update()
    {
        float xSpeed = Input.GetAxis("Horizontal") * 10f;
        float ySpeed = Input.GetAxis("Vertical") * 10f;
        this.transform.Translate(xSpeed*Time.deltaTime, ySpeed * Time.deltaTime, 0);
    }

    public void Stutter()
    {
        heart2.fillAmount = 0; //visually show you've lost 1 heart
        hitObstacleTimes++;
        sharkScript.sharkStartMoved = false;
        sharkScript.direction = "forwards"; //moves the shark forwards if you've stuttered
    }
    
    public void Death()
    {
        heart1.fillAmount = 0; //visually show you've lost 2 hearts
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
            score.text = $"Current Score: {scoreScript.score.ToString("0")}<br> High Score: {leaderboard.currentScores[0]}";
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
