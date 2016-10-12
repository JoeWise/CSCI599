using UnityEngine;
using System.Collections;

public class LightPickupManager : MonoBehaviour {

	public static bool pickingUpAudioTrigger = false;
	public Camera cam;
	public float raycastRange = 5;
	public GameObject crosshairUI;

	private GameObject pickingUp;

	// Use this for initialization
	void Start () {

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
	}

	void pickUpLight(GameObject light) {
		Debug.Log ("Picking up the thing\n");

	}
}
