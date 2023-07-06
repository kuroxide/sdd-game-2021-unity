using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform: MonoBehaviour {
	[SerializeField] private float m_LowestPosition;
	[SerializeField] private float m_HighestPosition;
	[SerializeField] private float m_Speed;
	[SerializeField] private float m_Lock;
	public Animator m_Animator;

	private bool m_Frozen = false;
	private int m_Direction = 1;
	[HideInInspector] public float m_TargetY;

	void Update() {
		if (Input.GetButtonDown("Freeze")) {
			m_Frozen = !m_Frozen;
			m_Animator.SetBool("Moving", !m_Frozen);
		}
	}

	void FixedUpdate() {
		if (!m_Frozen) {
			if (m_Lock != 0 && transform.position.y <= m_Lock) {
				m_Frozen = true;
			} else {
				if (transform.position.y >= m_HighestPosition) m_Direction = -1;
				if (transform.position.y <= m_LowestPosition) m_Direction = 1;
				m_TargetY = m_Speed * m_Direction;
			}
			transform.position = new Vector2(transform.position.x, transform.position.y + m_TargetY);
		} else {
			m_TargetY = 0;
		}
	}
}
