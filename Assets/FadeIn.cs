using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour {
	private bool Fade = false;
	private float FadeTime = 0f;
	[SerializeField] private float FadeIncrement = 0.02f;
	Color temp = new Color(255,255,255);

	void Update() {
		if (Input.anyKeyDown) {
			Fade = true;
		}
	}

	void FixedUpdate() {
		if (Fade) {
			temp.a = FadeTime;
			GetComponent<SpriteRenderer>().color = temp;
			FadeTime += FadeIncrement;
		}
	}
}
