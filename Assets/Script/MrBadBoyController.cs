using UnityEngine;
using System.Collections;
using System.Timers;

public class MrBadBoyController : MonoBehaviour {

	// Object à instancier
	public GameObject prefab;

	public Transform firstMarker;
	public Transform secondMarker;
	public float speed;
	public float dropFrequence;

	private Transform destination;
	private float timer = 0.0f;	

	void Start()
	{
		ChangeDestination();
    }

	public void ChangeDestination()
	{
		// Change the destination
		if (destination == firstMarker)
		{
			destination = secondMarker;
		}
		else
		{
			destination = firstMarker;
		}
	}

	void Update()
	{
		// Se déplace vers la deuxieme destination en fonction de t
		transform.position = Vector3.Lerp(firstMarker.position,
										  secondMarker.position,
										  speed * timer);

		transform.rotation = Quaternion.Lerp(transform.rotation,
										destination.rotation,
										speed * 4.0f * Time.deltaTime);

		if (destination == firstMarker)
		{	// Aller au premier marqueur
			timer = Mathf.Clamp(timer - Time.deltaTime, 0.0f, 1.0f / speed);
		}
		else // Aller au deuxième marqueur
		{
			timer = Mathf.Clamp(timer + Time.deltaTime, 0.0f, 1.0f / speed);
		}

		if(transform.position == destination.position)
			ChangeDestination();

		// Check pour le drop d'item au bout de 3 secondes
		dropFrequence -= Time.deltaTime;
		if (dropFrequence < 0)
		{
			dropFrequence = 3;
			Instantiate(prefab,transform.position,new Quaternion(45,45,45,45));
		}
	}
}
