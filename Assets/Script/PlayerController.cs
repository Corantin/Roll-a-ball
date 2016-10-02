using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public GUIText countText;
	public GUIText finalText;
	private int count;
	private Rigidbody myRigidBody;

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
		finalText.text = "";
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		myRigidBody.AddForce(movement * speed * Time.deltaTime);

		if (Input.GetKeyDown(KeyCode.R))
			ResetGame();
	}

	void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.tag == "Pickup")
		{
			other.gameObject.SetActive(false);

			other.GetComponent<Renderer>().material.color
				= GetComponent<Renderer>().material.color;

			count++;
			SetCountText();
		}
		else if (other.gameObject.tag == "BlackHole")
		{
			StartCoroutine(ResetAfterSeconds(5));
			Time.timeScale = 0f;
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