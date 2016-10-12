using UnityEngine;
using System.Collections;

// Handles lantern events. Light pickups and lantern flares.
public class LanternManager : MonoBehaviour {

	// GameObjects
	public Camera cam;

	// Light pickups
	public static bool pickingUpAudioTrigger = false; // Triggers music change in MusicManager
	public AudioClip pickupClip;
	public float animationDelay = 3.3f; // Secs delay before lerping light towards player on pickup
	public float speed = 2.5f; // Speed at which pickup moves to player

	// Crosshair raycast
	public float raycastRange = 5;
	public GameObject crosshairUI;

	// Private variables
	private GameObject pickingUp;	// Current light picking up
	private Light lanternLight = null;	// Light part of lantern

	// Use this for initialization
	void Start () {
		// Set up audio for pickups
		/*GameObject[] pickUps = GameObject.FindGameObjectsWithTag ("PickUp");
		foreach (GameObject pu in pickUps) {
			pu.transform.parent.parent.GetComponent<AudioSource> ().playOnAwake = false;
			pu.transform.parent.parent.GetComponent<AudioSource> ().clip = pickupClip;
			//Debug.Log ("Found audio source");
		}*/
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;

		Vector3 fwd = cam.transform.TransformDirection (Vector3.forward);
		if (Physics.Raycast (cam.transform.position, fwd, out hit, raycastRange)) {
			if (hit.transform.CompareTag ("PickUp")) {
				crosshairUI.SetActive (true);

				// Allow click
				pickingUp = hit.transform.parent.gameObject;
				if (Input.GetMouseButtonDown (0))
					pickUpLight (pickingUp);
			}
		} else {
			crosshairUI.SetActive (false);
			pickingUp = null;
			if (Input.GetMouseButtonDown (0)) {
			} // do the light stun thing
		}
		//pickingUpAudioTrigger = false;
	}

	void pickUpLight(GameObject light) {
		//Debug.Log ("Picking up the thing\n");
		pickUpAnimation (light);
		pickingUpAudioTrigger = true;
		light.transform.parent.GetComponent<AudioSource>().Play();
	}

	void pickUpAnimation(GameObject light) {
		// Find dust particles
		Transform[] children = light.GetComponentsInChildren<Transform> ();
		foreach (Transform child in children) {
			//Debug.Log ("There exists a child");
			if (child.gameObject.CompareTag ("LightPickUpDust")) {
				ParticleSystem par = child.gameObject.GetComponent<ParticleSystem> ();
				par.GetComponent<Animation>().Play ();

				//Debug.Log ("Found child");
			}
		}

	}
}
