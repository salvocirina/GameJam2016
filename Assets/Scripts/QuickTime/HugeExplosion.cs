using UnityEngine;
using System.Collections;

public class HugeExplosion : MonoBehaviour {

	public float radius = 1000.0f;

	public float damage = 1000.0f;

	GameObject player;

	public GameObject explosionPrefab;

	public float diffX = 0.0f;
	public float diffZ = 116.0f;
	public float exactY = 92.0f;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

//	void Update()
//	{
//		if (Input.GetKeyUp(KeyCode.E))
//			Explode();
//	}

	public void Explode()
	{
		Debug.Log ("explosion");

		StartCoroutine(ExpodeDelay());



	}

	IEnumerator ExpodeDelay()
	{
		yield return new WaitForSeconds(0.5f);

		RaycastHit[] hits = Physics.SphereCastAll(player.transform.position, radius, player.transform.forward);
		
		for (int i = 0; i < hits.Length; i++)
		{
			
			if (hits[i].transform.gameObject.tag == "Helicopter")
			{
				Helicopter heli = hits[i].transform.gameObject.GetComponent<Helicopter>();
				if (heli != null)
					heli.Hit(damage);
			}
			
			if (hits[i].transform.gameObject.tag == "Tank")
			{
				EnemyTank tank = hits[i].transform.gameObject.GetComponent<EnemyTank>();
				if (tank != null)
					tank.Hit(damage);
			}
		}
		
		if (explosionPrefab != null)
		{
			Vector3 position = new Vector3(Camera.main.transform.position.x + diffX, exactY, Camera.main.transform.position.z + diffZ);
			Instantiate(explosionPrefab, position, Quaternion.identity);
		}
	}
}
