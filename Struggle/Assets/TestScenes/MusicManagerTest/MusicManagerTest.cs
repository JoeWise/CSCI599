using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {


	public AudioSource source;

	public AudioClip main_theme;
	public AudioClip light_variation;
	private float delay = 0;
	void Start() {
		
		source.clip = main_theme;
		source.Play ();
	}

	void Update() {

		//Change if condition for event
		if (playerCollider.pickedUp == true) {
			if (source.clip == main_theme) {
				source.clip = light_variation;
				source.loop = false;
				source.Play ();

			}

		}
		if((source.isPlaying == false) && (source.clip==light_variation)) {
			source.clip = main_theme;
			source.Play ();
		}

	}

}
