using UnityEngine;
using System.Collections;

public class playerCollider : MonoBehaviour
{
	public GameObject player;
    public GameObject lantern;
    public GameObject victoryText;
    public float pushPower = 2.0F; // Push force of player
    public GameObject[] carpetHair;
    public float distTriggerCarpetHair = 2.0f; // Distance to start carpet hair animation
    public float maxAngle = 80; // Max angle of carpet hair tilt
	//public AudioClip pickupClip;
	public Animation pickupAnim;
	public float animationDelay = 3.3f; // Secs delay before lerping light towards player on pickup
	public float speed = 2.5f; // Speed at which pickup moves to player
	public float distWindEffect = 5f; // Distance for pickup wind effect
	public float blowOutTime = 0.5f; // Time for pickup wind effect
	public float blowBackTime = 5.0f; // Time for pickup wind effect cooldown
	public GameObject ambient;
	public static bool pickedUp = false;	//Is true if a light is picked up, false after pick up animation	
    private Light lanternLight = null;
	private bool finished = false;
	public AudioClip smallPickupAudio;
	public static bool animateLightPickupWind = false;
	public GameObject lanternCentre;

	public GameObject col;

    // Use this for initialization
    void Start()
    {
        lanternLight = lantern.GetComponent<Light>();
        //carpetHair = GameObject.FindGameObjectsWithTag("CarpetHair");

		// Set up audio for pickups
		/*GameObject[] pickUps = GameObject.FindGameObjectsWithTag ("PickUp");
		foreach (GameObject pu in pickUps) {
			pu.transform.parent.parent.GetComponent<AudioSource> ().playOnAwake = false;
			pu.transform.parent.parent.GetComponent<AudioSource> ().clip = pickupClip;
			//Debug.Log ("Found audio source");
		}*/
	

		/*// Disable "picked up" particle effect until collision activation
		GameObject[] pickedUpAnim = GameObject.FindGameObjectsWithTag ("PickedUp");
		foreach (GameObject pu in pickedUpAnim) {
			pu.GetComponent<ParticleSystem> ().enableEmission = false;
		}*/
    }

    // Update is called once per frame
    void Update()
    {
		Collider[] activeCarpetHairs = Physics.OverlapSphere (player.transform.position, distTriggerCarpetHair);
		for (int i = 0; i < activeCarpetHairs.Length; i++)
        {
			GameObject hair = activeCarpetHairs [i].gameObject;
			if(hair.transform.CompareTag("CarpetHair")) {
				//Debug.Log ("Found");
				Vector3 distance = transform.position - activeCarpetHairs[i].transform.position;
	            distance.y = 0; // Only look on x,z
	            float distanceMag = distance.magnitude;
	            if (distanceMag < distTriggerCarpetHair)
	            {
					adjustCarpetHair(hair, distanceMag);
	            }
				else activeCarpetHairs[i].transform.eulerAngles = new Vector3(0, 0, 0);
			}
        }
		if (animateLightPickupWind) {
			animateLightPickupWind = false;
			//StartCoroutine (lightPickupWindEffect ());
		}
    }

    // Animate carpet hairs
    void adjustCarpetHair(GameObject carpetHair, float distance)
    {
        //carpetHair.SetActive(false); // Testing
        float magnitude = ((distTriggerCarpetHair - distance) / distTriggerCarpetHair);
        // Non-linear
        magnitude *= magnitude;


        float xRatio, zRatio;
        xRatio = (carpetHair.transform.position.x - transform.position.x) / distance * maxAngle;
        zRatio = (carpetHair.transform.position.z - transform.position.z) / distance * maxAngle;

        //Debug.Log("Distance: " + distance + "Magnitude: " + magnitude + " Ratios " + xRatio + " " + zRatio);
        carpetHair/*[i]*/.transform.eulerAngles = new Vector3(zRatio * magnitude, 0, -xRatio * magnitude);// carpetHairAngles[i];

    }

	// Physics for collision (testing) - may be needed later but currently unused
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3F)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;
    }

    // Collect light source. Should probably not be here.
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUpMini"))
        {
            GameObject otherObject = other.transform.gameObject;
			other.gameObject.transform.parent.gameObject.GetComponent<AudioSource> ().Play ();
			other.isTrigger = false;
            other.enabled = false;
            //other.gameObject.SetActive(false);

            playPickupAnim(otherObject.transform.parent.gameObject);
        }
        //if (other.gameObject.CompareTag("PickUp"))
        //{
            //Debug.Log("Collision Detected");

			//GameObject otherObject = other.transform.gameObject;

			// Remove trigger
			//other.isTrigger = false;
			//other.enabled = false;
			//pickedUp = true;
			// Change player lantern accordingly
            //updateLantern(otherObject);

			// Play sound
			//otherObject.transform.parent.parent.GetComponent<AudioSource>().Play();
			// climax at ~3.4 seconds

			// Play animation
			//playPickupAnim(otherObject.transform.parent.gameObject);

			// Disable lighe source and attached particle effects - REPLACE WITH ANIM
            //otherObject.SetActive(false);

			// Enable particle effect for picked up space

			/*GameObject par = otherObject.transform.parent.parent.gameObject;
			Transform[] children = par.GetComponentsInChildren<Transform> ();
			foreach (Transform child in children) {
				if (child.gameObject.CompareTag ("PickedUp")) {
					child.gameObject.GetComponent<ParticleSystem>().enableEmission = true;
					//Debug.Log ("Found child");
				}
			}*/

			//Debug.Log ("Pickups Left: " + GameObject.FindGameObjectsWithTag ("PickUp").Length);
            //if (GameObject.FindGameObjectsWithTag("PickUp").Length == 1)
            //{
                //Debug.Log("All lights collected");
				//finished = true;
			//Debug.Log ("You win");
                //victoryText.SetActive(true);
            //}
            // change lantern colour
        //}

    }
	/*
    void updateLantern(GameObject otherObject)
    {
        Light colorLight = otherObject.GetComponent<Light>();
        Color colorObject = colorLight.color;
        for (int i = 0; i < 3; i++)
        {
            //Debug.Log("Colour: " + i + ": " + colorObject[i]);
            if (colorObject[i] < 0.8) colorObject[i] = 0; // Remove secondary colours
        }

        lanternLight.color += colorObject / 2;
        lanternLight.range *= 1.1f;
        //lanternLight.intensity *= 1.1f;
    }*/
	
	void playPickupAnim(GameObject pickup) {
//		pickup.GetComponent<Animation>().Play ();
		//currentLight = pickup;
		StartCoroutine(moveLightToPlayer (pickup));
		//StartCoroutine (lightPickupWindEffect ());
	}

	IEnumerator moveLightToPlayer(GameObject pickup) {
		if (pickup != null) {
			//yield return new WaitForSeconds (3.4f);
			float startTime = Time.time;
			Vector3 posA = pickup.transform.position;
			Vector3 playerToCam = new Vector3 (0, 4.0f, 0); // Player =/= camera -> need to adjust
			while (Vector3.Distance(pickup.transform.position,transform.position-playerToCam) > 0.1f) {
				pickup.transform.position = Vector3.Lerp (posA, transform.position-playerToCam, (Time.time - startTime)*speed);
				yield return null;
			}

			// If game over
			if (finished) {
				victoryText.SetActive (true);
				Light ambientLight = ambient.GetComponent<Light>();

				ambientLight.intensity += 0.35f;
				finished = !finished;
			}
			pickup.SetActive (false);
		}
		pickedUp = false;
	}


	// Wind effect on surrounding carpet hairs
	IEnumerator lightPickupWindEffect() {
		yield return new WaitForSeconds (3.2f);

		// Using distTriggerCarpetHair (from player movement pushing carpet hairs aside) to produce wind
		float start = distTriggerCarpetHair;
		float max = distWindEffect;
		float startTime = Time.time;


		while (Time.time - startTime < blowOutTime + blowBackTime) {
			if (Time.time - startTime < blowOutTime) {
				distTriggerCarpetHair = Mathf.Lerp (start, max, (Time.time - startTime));
			} else {
				distTriggerCarpetHair = max;
				distTriggerCarpetHair = Mathf.Lerp (max, start, (Time.time - startTime - blowOutTime));
			}
			//Debug.Log ("New step effect range: " + distTriggerCarpetHair);
			yield return null;
		}

	}

}
