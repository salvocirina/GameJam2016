using UnityEngine;
using System.Collections;

public class PlayerMissile : MonoBehaviour {

	public GameObject dustParticlesPrefab;

	//float time = 5.0f;
	//bool spawned = false;

	public float radius = 3.0f;

	public float helicopterHeight = 6;

	public GameObject helicopterPrefab;

	void Start()
	{
		if (helicopterPrefab != null)
		{
			Helicopter heli = helicopterPrefab.GetComponent<Helicopter>();
			if (heli != null)
				helicopterHeight = heli.helicopterHeight;
		}
	}

	void Particles()
	{
		if (dustParticlesPrefab != null)
		{
			Instantiate(dustParticlesPrefab, transform.position, Quaternion.identity);
		}
	}

	void Update()
	{
		if (transform.position.y > helicopterHeight)
			transform.position = new Vector3(transform.position.x, helicopterHeight, transform.position.z);
	}

	void Explode()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Helicopter")
		{
			Explode ();

			Particles();

			TakeEnemies(1);

			Autodestroy();
		}
	}

	void Autodestroy()
	{
		Destroy(this.gameObject);
	}

	void TakeEnemies(float lifePoints)
	{
		Vector3 p1 = transform.position;

		RaycastHit[] hits;

		//hits = Physics.SphereCastAll(p1, transform.forward, radius);
		hits = Physics.SphereCastAll(p1, radius, transform.forward);

		for (int i = 0; i < hits.Length; i++)
		{
			if (hits[i].transform.gameObject.tag == "Helicopter")
			{
				Helicopter heli = hits[i].transform.gameObject.GetComponent<Helicopter>();
				if (heli != null)
					heli.Hit(lifePoints);
			}
		}
	}
}
