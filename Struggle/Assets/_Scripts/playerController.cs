﻿using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

    public float sprintMultiplier = 3.0F;
    public float jumpForce = 5.0f;
    public float speed = 3.0F;
    public float rotateSpeed = 3.0F;
    // public Rigidbody rb;
    CharacterController controller;
    Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start () {
        // Remove cursor, confine to game window
		Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        // transform.Rotate(0, (Input.GetAxis("Horizontal")  + Input.GetAxis("Left Thumb X")) * rotateSpeed, 0);
        // float curSpeed = speed * (Input.GetAxis("Vertical") + Input.GetAxis("Left Thumb Y"));

        moveDirection.x = (Input.GetAxis("Horizontal")  + Input.GetAxis("Left Thumb X"));
        moveDirection.z = (Input.GetAxis("Vertical")  + Input.GetAxis("Left Thumb Y"));

        if (Input.GetButton("Sprint"))
        {
            moveDirection.x *= sprintMultiplier;
            moveDirection.z *= sprintMultiplier;
        }

        // Debug.Log(rb.position.y);
        // if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        // if (Input.GetButtonDown("Jump") && !(rb.velocity.y > 2) && !(rb.velocity.y < -2))
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            Debug.Log("pressed Jump");
            // this.GetComponent<CharacterController>().AddForce(new Vector3(0, jumpForce, 0), ForceMode.Force);
            moveDirection.y = jumpForce;
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        // moveDirection += forward;
        // controller.SimpleMove(forward * curSpeed);
        controller.Move(moveDirection);
	}
}
