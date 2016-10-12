using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {


	public AudioSource source;

	public AudioClip main_theme;
	public AudioClip light_variation;
	void Start() {
		
		source.clip = main_theme;
		source.Play ();
	}

	void Update() {

		//Change if condition for event
		if (LanternManager.pickingUpAudioTrigger) {
			if (source.clip == main_theme) {
				source.clip = light_variation;
				source.loop = false;
				source.Play ();

			}
			LanternManager.pickingUpAudioTrigger = false;
		}
		if((source.isPlaying == false) && (source.clip==light_variation)) {
			source.clip = main_theme;
			source.Play ();
		}
			

	}

}
