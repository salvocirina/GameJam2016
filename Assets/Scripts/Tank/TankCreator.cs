using UnityEngine;
using System.Collections;

public class TankCreator : MonoBehaviour {

	public GameObject tankPrefab;
	public Transform startingPosition;
	public Transform endingPosition;
	public int tankNumber = 10;

	float actualDiff = 0.0f;

	public float xDiff = 2.0f;

	public float spawnTime = 1.0f;
	float lastSpawn = 0.0f;

	void Start () {
		
	}

	void Update () {
//		if ((Time.time - lastSpawn) > spawnTime)
//		{
//			Vector3 direction = endingPosition.transform.position - startingPosition.transform.position;
//			Vector3 opposite = new Vector3(direction.z, direction.y, direction.x).normalized;
//			Vector3 diff = opposite * Random.Range(0.0f, xDiff);
//
//
//			SpawnTank(diff);
//			lastSpawn = Time.time;
//		}
	}

	public void SpawnTank()
	{
		if (tankPrefab != null)
		{
			GameObject tank = (GameObject) Instantiate(tankPrefab, startingPosition.transform.position, Quaternion.identity) as GameObject;
			tank.GetComponent<EnemyTank>().SetEndingPosition(endingPosition.transform.position);
		}
	}
}
