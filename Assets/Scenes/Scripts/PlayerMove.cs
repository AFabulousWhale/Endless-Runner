using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public GameObject deathCanvas;
    public TextMeshProUGUI score;
    public Score scoreScript;
    public Shark sharkScript;
    public Leaderboard leaderboard;

    public Image heart1, heart2; //heart1 is left, heart2 is right

    public AudioSource mainMusic, deathMusic;

    public AudioClip death;

   public AudioSource collision; //will be a different sound depending on if the object is a collectible or obstacle
    public AudioClip obstacleSFX, collectibleSFX;

    private CharacterController controller;
    private Vector3 direction;
    public float yValue;
    public float forwardSpeed;

    public int lives = 2;

    private void Start()
    {
        controller = GetComponent<CharacterController>(); //gets the player's character controller
    }
    void Update()
    {
        if (!Cinematic.playingCinematic) //waits until cinematic is done before playing game
        {
            if (forwardSpeed != 30)
            {
                forwardSpeed += 0.001f;
            }
            direction.z = forwardSpeed;
            direction.x = Input.GetAxis("Horizontal") * 10f;

            if (Input.GetKey(KeyCode.Space))
            {
                yValue += (0.0025f * (forwardSpeed));
                yValue = Mathf.Clamp(yValue, 0, 3);
            }
            else
            {
                yValue -= (0.0025f * (forwardSpeed));
                yValue = Mathf.Clamp(yValue, 0, 3);
            }

            if (lives == 1)
            {
                heart2.fillAmount = 0;
            }
            else if (lives == 0)
            {
                Death();
            }
        }
    }

    void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
        transform.position = new Vector3(transform.position.x, yValue, transform.position.z);
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Collectible")
        {
            scoreScript.score += 100;
            Destroy(hit.gameObject);
            collision.clip = collectibleSFX;
            collision.Play();
        }
        if (hit.transform.tag == "Obstacle")
        {
            hit.transform.tag = "Untagged";
            hit.collider.enabled = false;
            lives--;
            collision.clip = obstacleSFX;
            collision.Play();
        }
    }
}
