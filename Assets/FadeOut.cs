using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour {
	private bool Fade = false;
	[SerializeField] private float FadeTime = 0.5f;
	Color temp = new Color(255,255,255);

	void Update() {
		if (Input.anyKeyDown) {
			Fade = true;
		}

		if (Fade) {
			temp.a = FadeTime;
			GetComponent<SpriteRenderer>().color = temp;
			FadeTime -= Time.deltaTime;
			if (FadeTime < 0f) {
				gameObject.SetActive(false);
			}
		}
	}
}
