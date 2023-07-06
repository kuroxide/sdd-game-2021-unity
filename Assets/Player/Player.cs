using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	[SerializeField] private float m_MoveSpeed = 3f;
	[SerializeField] private float m_JumpForce = 6f;
	[SerializeField] private float m_DashForce = 15f;
	[SerializeField] private float m_DashTime = 0.1f;
	public Animator m_Animator;
	public Rigidbody2D m_Rigidbody2D;

	private float moveX, m_DashTimer;
	private bool m_Grounded, m_Dash, m_Dashing, m_Jump, m_DoubleJumped;

	void Start() {
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		m_DashTimer = m_DashTime;
	}

	void OnTriggerEnter2D(Collider2D m_Collision) {
		if (m_Collision.gameObject.name == "LevelChange") {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}

		if (m_Collision.gameObject.tag == "Killzone") {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		if (m_Collision.gameObject.tag == "Crystal") {
			m_Collision.gameObject.SetActive(false);
			m_DashTimer = m_DashTime;
			m_DoubleJumped = false;
		}
	}

	void OnTriggerStay2D(Collider2D m_Collision) {
		m_Grounded = true;
		m_Dashing = false;
		m_DashTimer = m_DashTime;
		m_Animator.SetBool("Jumping", false);
		m_Animator.SetBool("Falling", false);
		m_DoubleJumped = false;
		if (m_Collision.gameObject.tag == "MovingPlatform") {
			MovingPlatform script = m_Collision.gameObject.GetComponent<MovingPlatform>();
			transform.position = new Vector2(transform.position.x, transform.position.y + script.m_TargetY);
		}
	}

	void OnTriggerExit2D(Collider2D m_Collision) {
		m_Grounded = false;
	}

	void Update() {
		moveX = Input.GetAxisRaw("Horizontal");
		m_Animator.SetFloat("Speed", Mathf.Abs(moveX));
		m_Animator.SetBool("Grounded", m_Grounded);

		if (Input.GetButtonDown("Jump")) {
			m_Jump = true;
		}

		if (Input.GetButtonDown("Dash")) {
			m_Dash = true;
		}

		if (Input.GetButtonDown("Restart")) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	void FixedUpdate() {
		m_Rigidbody2D.velocity = new Vector2(moveX * m_MoveSpeed, m_Rigidbody2D.velocity.y);

		if (moveX < 0) transform.eulerAngles = Vector3.up * 180;
		else if (moveX > 0) transform.eulerAngles = Vector3.zero;

		if (m_Grounded && m_Jump) {
			m_Rigidbody2D.velocity = new Vector2(0, m_JumpForce);
		} else if (!m_DoubleJumped && m_Jump) {
			m_Rigidbody2D.velocity = new Vector2(0, m_JumpForce);
			m_DoubleJumped = true;
		}

		if (!m_Grounded && m_DashTimer > 0f && m_Dash) {
			m_Dashing = true;
		}

		if (m_Dashing) {
			m_Rigidbody2D.velocity = new Vector2(moveX*m_DashForce, 0);
			m_DashTimer -= Time.fixedDeltaTime;
			if (m_DashTimer < 0f) {
				m_Dashing = false;
			}
		}

		m_Jump = false;
		m_Dash = false;

		if (m_Rigidbody2D.velocity.y < 0f) {
			m_Animator.SetBool("Jumping", false);
			m_Animator.SetBool("Falling", true);
		}

		if (m_Rigidbody2D.velocity.y > 0f) {
			m_Animator.SetBool("Jumping", true);
			m_Animator.SetBool("Falling", false);
		}
	}
}
