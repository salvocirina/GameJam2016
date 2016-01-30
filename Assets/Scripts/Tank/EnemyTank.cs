﻿using UnityEngine;
using System.Collections;

public class EnemyTank : MonoBehaviour {

	public GameObject cannon;

	GameObject player;

	public Transform startingPoint;
	Vector3 startingPointV3;
	public Vector3 endingPoint;

	bool arrived = false;
	public float movingSpeed = 2.0f;

	public GameObject missilePrefab;

	public GameObject point;

	public float shootingRate = 0.5f;

	float lastShoot = 0;

	bool playerNear = false;
	public float playerDistanceToShoot = 6.0f;



	// Use this for initialization
	void Start () {
//		if (startingPoint != null)
//		{
//			if (startingPointV3 == null)
//				startingPointV3 = startingPoint.transform.position;
//			transform.position = startingPointV3;
//		}

		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (cannon != null)
		{
			Vector3 direction = transform.position - player.transform.position;
			cannon.transform.forward = direction;
		}

		if (!arrived && endingPoint != null){
			transform.position = Vector3.MoveTowards(transform.position, endingPoint, Time.deltaTime * movingSpeed);

			if (transform.position == endingPoint)
				arrived = true;
		}

		if (((Time.time - lastShoot) > shootingRate) && PlayerInSight() && PlayerNearEvaluation())
		{
			lastShoot = Time.time;
			Shoot();
		}
	}

	public void SetStartingPoint(Vector3 startingPoint)
	{
		startingPointV3 = startingPoint;
	}

//	public void SetVariables(Transform _begin, Transform _end)
//	{
//		startingPoint = _begin;
//		endingPoint = _end;
//	}

	public void SetEndingPosition(Vector3 _end)
	{
		endingPoint = _end;
	}

	void Shoot()
	{
		if (missilePrefab != null && point != null)
		{
			GameObject missile = (GameObject) Instantiate(missilePrefab, point.transform.position, point.transform.rotation) as GameObject;
		}
	}

	//TODO
	bool PlayerInSight()
	{
		return true;
	}

	bool PlayerNearEvaluation()
	{
		//Debug.Log (Vector3.Distance(transform.position, player.transform.position));

		if ((Vector3.Distance(transform.position, player.transform.position) < playerDistanceToShoot))
		    return true;
		else
			return false;

	}
}