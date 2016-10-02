using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour
{
	private Renderer rend;

	void Start()
	{
		rend = GetComponent<Renderer>();
		rend.material.color = Random.ColorHSV();
	}

	// Update is called once per frame
	void Update()
	{
		transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
	}
}
