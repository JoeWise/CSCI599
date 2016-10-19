using UnityEngine;
using System.Collections;

// Handles lantern events. Light pickups and lantern flares.
public class LanternManager : MonoBehaviour {

	// GameObjects
	public Camera cam;
	public GameObject lanternCentre;
	public GameObject lantern;
	public GameObject lanternAnimShell;
	public ParticleSystem lightBurst;
	public ParticleSystem lightBurst2;
	public GameObject burstParticleContainer;

	// Light pickups
	public static bool pickingUpAudioTrigger = false; // Triggers music change in MusicManager
	public AudioClip pickupClip;

	// Crosshair raycast
	public float raycastRange = 5;
	public GameObject crosshairUI;

	// Burst mechanic
	public int numBurstParticles = 100; // Num particles for lantern burst

	// Private variables
	private GameObject pickingUp;	// Current light picking up
	private Light lanternLight = null;	// Light part of lantern
	private bool isFinished = false; // Is the game finished?
	private float burstRate = 50.0f; // Emission rate for lantern burst

	public static int numCollected = 0;

	// Use this for initialization
	void Start () {
		lanternLight = lantern.GetComponent<Light>();
		//lanternAnimShell.GetComponent<Animation>().Play ();

		// Set up audio for pickups
		/*GameObject[] pickUps = GameObject.FindGameObjectsWithTag ("PickUp");
		foreach (GameObject pu in pickUps) {
			pu.transform.parent.parent.GetComponent<AudioSource> ().playOnAwake = false;
			pu.transform.parent.parent.GetComponent<AudioSource> ().clip = pickupClip;
			//Debug.Log ("Found audio source");
		}*/

		//lightBurst.emission.rate = 0.0;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!isFinished) {
			RaycastHit hit;

			Vector3 fwd = cam.transform.TransformDirection (Vector3.forward);
			if (Physics.Raycast (cam.transform.position, fwd, out hit, raycastRange)) {
				if (hit.transform.CompareTag ("PickUp")) {
					crosshairUI.SetActive (true);

					// Allow click
					pickingUp = hit.transform.parent.gameObject;
					// if (Input.GetMouseButtonDown (0)) 
					if (Input.GetButton("Interact"))
					{
						hit.collider.enabled = false;
						pickUpLight (pickingUp);
						//playerCollider.animateLightPickupWind = true;
					}
				}
			} else {
				crosshairUI.SetActive (false);
				pickingUp = null;
				if (Input.GetButton("Interact")) {
				} // do the light stun thing
			}
			//pickingUpAudioTrigger = false;

			if (numCollected == 4) {
				Debug.Log ("You win!");
				isFinished = true;
				GameObject.FindGameObjectWithTag ("AmbientLight").GetComponent<Light> ().intensity = 0.9f;
			}
		}

		if (Input.GetMouseButtonDown (0)) {
			burstHandle ();
		}
	}

	void burstHandle() {
		burst ();
		//Invoke("burst", 0.1f); // for more than 1 ring
		//Invoke("burst", 0.2f);
		burst2 ();
	}

	void burst() {
		resetParticleRotation ();
		lightBurst.Emit (numBurstParticles);

	}

	void burst2() {
		resetParticleRotation ();
		lightBurst2.Emit (1);
	}

	void resetParticleRotation() {
		burstParticleContainer.transform.eulerAngles = new Vector3 (90.0f, 0.0f, 0.0f);
	}

	void pickUpLight(GameObject light) {
		//Debug.Log ("Picking up the thing\n");
		pickUpAnimation (light);
		pickingUpAudioTrigger = true;
		light.transform.parent.GetComponent<AudioSource>().Play();
		numCollected++;
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("\\o/");
	}

	void pickUpAnimation(GameObject light) {
		light.GetComponent<Animation>().Play ();

		Transform[] children = light.GetComponentsInChildren<Transform> ();
		foreach (Transform child in children) {
			if (child.gameObject.CompareTag ("Beacon")) {
				var temp = child.gameObject.GetComponent<ParticleSystem> ().emission;
				temp.rate = 0;
				//Debug.Log ("Found child");
			}
		}

		updateLantern (light);
	}

	void updateLantern(GameObject otherObject)
	{
		Color toAdd = otherObject.GetComponent<ParticleSystem> ().startColor;
		for (int i = 0; i < 3; i++)
		{
			//Debug.Log("Colour: " + i + ": " + toAdd[i]);
			if (toAdd[i] < 0.9) toAdd[i] = 0; // Remove secondary colours
		}
		Color toDecrease = new Color(0.0f, 0.0f, 0.0f);
		//lanternCentre.startColor -= toDecrease;
		//lanternCentre.startColor += toAdd / 3;
		//lanternLight.range *= 1.1f;
		//lanternLight.intensity *= 1.1f;
	}
}
