using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoadSceneOnEnter : MonoBehaviour {
	[SerializeField] private int SceneToLoad = 0;
	void OnTriggerEnter2D(Collider2D m_Collision) {
		SceneManager.LoadScene(SceneToLoad);
	}
}
