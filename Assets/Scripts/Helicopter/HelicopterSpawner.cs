using UnityEngine;
using System.Collections;

public class HelicopterSpawner : MonoBehaviour {

	public GameObject helicopterPrefab;

	public float timeToSpawn = 3.0f;
	float lastSpawn = Mathf.NegativeInfinity;

	public Transform startingPosition;

	bool spawn = false;

	public int numberToSpawn = 10;
	int spawnedNumber = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (spawn)
			StartSpawn();
	}

	void StartSpawn()
	{
		if (spawnedNumber < numberToSpawn)
		{
			if ((Time.time - lastSpawn) > timeToSpawn)
			{
				Instantiate(helicopterPrefab, startingPosition.transform.position, Quaternion.identity);
				lastSpawn = Time.time;

				spawnedNumber++;
			}


		}

	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
			spawn = true;
	}
}
