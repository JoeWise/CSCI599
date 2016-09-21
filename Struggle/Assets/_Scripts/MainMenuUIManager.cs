using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuUIManager : MonoBehaviour {

	public GameObject mainPanel;

	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}
	public void QuitGame () {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif 
	}

	public void startGame () {
		Application.LoadLevel("Basic001");
	}
}
