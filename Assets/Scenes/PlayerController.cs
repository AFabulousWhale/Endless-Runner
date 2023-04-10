using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>(); //gets the player's character controller
    }

    private void Update()
    {
        direction.z = forwardSpeed;
    }

    void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }
}
