using UnityEngine;
using System.Collections;

public class playerCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Collect light source. Should probably not be here.
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            Debug.Log("Collision Detected");

            other.gameObject.SetActive(false);
            // change lantern colour
        }
    }
}
