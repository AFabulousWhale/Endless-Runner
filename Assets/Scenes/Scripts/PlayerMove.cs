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

    private CharacterController controller;
    private Vector3 direction;
    public float yValue;
    public float forwardSpeed;

    public bool fillingHeart1, fillingHeart2; //heart1 is left, heart2 is right

    private void Start()
    {
        controller = GetComponent<CharacterController>(); //gets the player's character controller
    }
    void Update()
    {
        forwardSpeed += 0.001f;
        direction.z = forwardSpeed;
        direction.x = Input.GetAxis("Horizontal") * 10f;

        if(Input.GetKey(KeyCode.Space))
        {
            yValue += 0.025f;
            yValue = Mathf.Clamp(yValue, 0, 3);
        }
        else
        {
            yValue -=  0.025f;
            yValue = Mathf.Clamp(yValue, 0, 3);
        }
        //direction.y = Input.GetAxis("Vertical") * 10f;

        if(fillingHeart1)
        {
            Heart1Fill();
        }
        else if (fillingHeart2)
        {
            Heart2Fill();
        }
    }

    void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
        transform.position = new Vector3(transform.position.x, yValue, transform.position.z);
    }

    //health 0 - 50 fills first heart
    public void Heart1Fill()
    {
        ///if the heart isn't full
        if (heart1.fillAmount != 1)
        {
            heart1.fillAmount += 0.001f;
        }
        else
        {
            fillingHeart1 = false;
            fillingHeart2 = true; //fills the second heart right after
        }
    }

    //health 51 - 100 fills second heart
    public void Heart2Fill()
    {
        ///if the heart isn't full
        if (heart2.fillAmount != 1)
        {
            heart2.fillAmount += 0.001f;
        }
        else
        {
            fillingHeart2 = false;
        }
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
