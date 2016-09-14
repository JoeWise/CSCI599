﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

	public GameObject pausePanel;
	public bool isPaused;
	public GameObject first_person_camera;

	// Use this for initialization
	void Start () {
		isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isPaused) {
			PauseGame (true);
		} else {
			PauseGame (false);
		}
		if(Input.GetButtonDown ("Pause")) {
			TogglePause ();
	
		}
	}

	void PauseGame (bool state) {
		if (state) {
			Cursor.lockState = CursorLockMode.None;
			Time.timeScale = 0.0f;
		} 
		else {
			Cursor.lockState = CursorLockMode.Locked;
			Time.timeScale = 1.0f;
		}
		
		pausePanel.SetActive (state);
	}

	public void TogglePause () {
		isPaused = !isPaused;
	}

	public void QuitGame () {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif 
	}
}
