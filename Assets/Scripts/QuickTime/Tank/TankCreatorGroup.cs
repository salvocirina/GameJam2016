using UnityEngine;
using System.Collections;

public class TankCreatorGroup : MonoBehaviour {

	TankCreator[] creators;

	bool spawned = false;
	float lastSpawn = Mathf.NegativeInfinity;
	public float timeToRespawn = 60.0f;

	// Use this for initialization
	void Start () {
		creators = GetComponentsInChildren<TankCreator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Spawn()
	{
		if (creators != null)
		{
			for (int i = 0; i < creators.Length; i++)
			{
				creators[i].SpawnTank();
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (!spawned && other.gameObject.tag == "Player" && ((Time.time - lastSpawn) > timeToRespawn))
		{
			Spawn();
			lastSpawn = Time.time;
		}
	}
}
