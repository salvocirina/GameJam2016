﻿using UnityEngine;
using System.Collections;

public class Helicopter : MonoBehaviour {

	GameObject player;

	public float distance = 3.0f;

	public float moveSpeed = 4.0f;

	public float rotationSpeed = 10.0f;

	public GameObject missilePrefab;
	
	public GameObject point;
	
	public float shootingRate = 0.5f;
	
	float lastShoot = 0;

	public float startRotationTime = 0.0f;
	float startTime;

	bool distanceReached = false;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");

		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (!distanceReached)
		{
			Vector3 objPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
//			if (Vector3.Distance(transform.position, objPosition) > distance)
//			{
				transform.position = Vector3.MoveTowards(transform.position, objPosition, Time.deltaTime*moveSpeed);
//			}

			if (Vector3.Distance(transform.position, objPosition) <= distance)
			    distanceReached = true;
		}


		transform.forward = player.transform.position - transform.position;

		if (distanceReached)
		{
			if ((Time.time - startTime) > startRotationTime)
				transform.RotateAround(player.transform.position, Vector3.up, Time.deltaTime * rotationSpeed);

			transform.SetParent(player.transform);
		}



		if (((Time.time - lastShoot) > shootingRate) && PlayerInSight())
		{
			lastShoot = Time.time;
			Shoot();
		}
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
}