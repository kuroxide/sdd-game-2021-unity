using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPersistence : MonoBehaviour {
	static MusicPersistence instance = null;
	
	void Awake() {
		if (instance != null) {
			Destroy(transform.gameObject);
		}
		else {
			instance = this;
			DontDestroyOnLoad(transform.gameObject);
		}
	}
}
