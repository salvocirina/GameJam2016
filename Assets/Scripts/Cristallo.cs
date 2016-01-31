using UnityEngine;
using System.Collections;

public class Cristallo : MonoBehaviour {

	public bool playerInside = false;
	public ParticleSystem particles;
	public ParticleSystem particles02;
	//GameController gameController;
	public int emissioneRate;

	void Start()
	{
		//emissioneRate = particles.emi
		particles.enableEmission = false;
		particles02.enableEmission = false;
	}

	void Update()
	{
		if (playerInside)
			GameController.instance.SetEnergy(100.0f);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			playerInside = true;
			particles.enableEmission = true;
			particles02.enableEmission = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			playerInside = false;
			particles.enableEmission = false;
			particles02.enableEmission = false;
		}
	}

}
