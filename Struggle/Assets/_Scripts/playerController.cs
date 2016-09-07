using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

    public float forwSpeed = 3.0F;
    public float backSpeed = 1.0F;
    public float straffeSpeed = 2.0F;
    public float sprintMultiplier = 3.0F;

	// Use this for initialization
	void Start () {
        // Remove cursor, confine to game window
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
        float move = Input.GetAxis("Vertical");
        float straffe = Input.GetAxis("Horizontal");

        float vertSpeed = forwSpeed;
        float horiSpeed = straffeSpeed;
        if (move < 0) vertSpeed = backSpeed;

        if (Input.GetButton("Sprint"))
        {
            vertSpeed *= sprintMultiplier;
            horiSpeed *= sprintMultiplier;
        }

        move *= vertSpeed * Time.deltaTime;
        straffe *= horiSpeed * Time.deltaTime;

        //Debug.Log(Input.GetAxis("Vertical"), gameObject);

        this.transform.Translate(straffe, 0, move);

        
	}
}
