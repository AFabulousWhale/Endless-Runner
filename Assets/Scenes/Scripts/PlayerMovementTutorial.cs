using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTutorial : MonoBehaviour
{
    public AudioSource mainMusic;

    public AudioSource collision; //will be a different sound depending on if the object is a collectible or obstacle
    public AudioClip collectibleSFX;

    private CharacterController controller;
    private Vector3 direction;
    public float yValue;
    public float forwardSpeed;

    public bool shouldFall; //will be enabled when the tutorial is at that part

    private void Start()
    {
        Time.timeScale = 1;
        controller = GetComponent<CharacterController>(); //gets the player's character controller
    }
    void Update()
    {
        if (!Cinematic.playingCinematic) //waits until cinematic is done before playing game
        {
            forwardSpeed += 0.001f;
            direction.z = forwardSpeed;
            direction.x = Input.GetAxis("Horizontal") * 10f;

            if (shouldFall)
            {
                yValue -= 0.025f;
                yValue = Mathf.Clamp(yValue, 0, 3);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                yValue += 0.025f;
                yValue = Mathf.Clamp(yValue, 0, 3);
            }
        }
    }

    void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
        transform.position = new Vector3(transform.position.x, yValue, transform.position.z);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Collectible")
        {
            Destroy(hit.gameObject);
            collision.clip = collectibleSFX;
            collision.Play();
        }
    }
}
