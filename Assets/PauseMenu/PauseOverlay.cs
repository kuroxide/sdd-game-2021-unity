using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOverlay: MonoBehaviour {
	public GameObject PauseMenu;
	
	bool m_Paused = false;

	void Start() {
		PauseMenu.SetActive(false);
		Time.timeScale = 1f;
	}

	void Update() {
		if (Input.GetButtonDown("Pause") && m_Paused) {
			PauseMenu.SetActive(false);
			Time.timeScale = 1f;
			m_Paused = !m_Paused;
		} else if (Input.GetButtonDown("Pause") && !m_Paused) {
			PauseMenu.SetActive(true);
			Time.timeScale = 0f;
			m_Paused = !m_Paused;
		}
	}
}
