using UnityEngine;
using System.Collections;

public class InsideCubeController : MonoBehaviour {

	public GameObject player;

	// Update is called once per frame
	void LateUpdate()
	{
		transform.position = player.transform.position;
	}
}
