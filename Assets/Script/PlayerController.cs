using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public GUIText countText;
	public GUIText finalText;
	public GameObject Hearth;

	private int count;
	private Rigidbody myRigidBody;
	private bool gameOver;

	private IEnumerator ResetAfterSeconds(int seconds)
	{
		float pauseEndTime = Time.realtimeSinceStartup + seconds;
		while (Time.realtimeSinceStartup <= pauseEndTime)
		{
			finalText.text = (pauseEndTime - Time.realtimeSinceStartup).ToString("0");
			yield return null; // Attend un frame
		}
		ResetGame();
	}

	void Start()
	{
		myRigidBody = GetComponent<Rigidbody>();
		count = 0;
		gameOver = false;
		finalText.text = "";
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (!gameOver)
		{
			float moveHorizontal = Input.GetAxis("Horizontal");
			float moveVertical = Input.GetAxis("Vertical");

			Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
			myRigidBody.AddForce(movement * speed * Time.deltaTime);

			if (Input.GetKeyDown(KeyCode.R))
				ResetGame();
		}
	}

	void LateUpdate()
	{
		Hearth.transform.position = transform.position;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Pickup")
		{
			other.gameObject.SetActive(false);

			Hearth.GetComponent<Renderer>().material.color
				= other.GetComponent<Renderer>().material.color;

			count++;
			SetCountText();
		}		
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			gameOver = true;
			myRigidBody.velocity = Vector3.zero;
			StartCoroutine(ResetAfterSeconds(5));
		}
		else if (other.gameObject.tag == "BlackHole")
		{
			Vector3 heading = other.transform.position - transform.position;
			float distance = heading.magnitude;
			Vector3 direction = heading / distance / 2;

			gameOver = true;
			myRigidBody.velocity = Vector3.zero;
			transform.position = Vector3.Slerp(transform.position, other.transform.position, 20 * Time.deltaTime);
			
			myRigidBody.detectCollisions = false;
			StartCoroutine(ResetAfterSeconds(5));
		}
	}

	void ResetGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	void SetCountText()
	{
		countText.text = "Count : " + count.ToString();
		if (count >= 12)
		{
			finalText.text = "YOU WIN !";
		}
	}
}