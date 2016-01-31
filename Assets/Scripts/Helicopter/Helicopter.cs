﻿using UnityEngine;
using System.Collections;

public class Helicopter : MonoBehaviour {

	GameObject player;

	public float distance = 3.0f;

	public float moveSpeed = 4.0f;

	public float rotationSpeed = 10.0f;

	public bool isStarDestroyer;

	public GameObject missilePrefab;
	
	public GameObject[] point;
	
	public float shootingRate = 1.5f;

	public float playerDistanceToShoot =6-0f;
	
	float lastShoot = 0;

	public float startRotationTime = 0.0f;
	float startTime;

	bool distanceReached = false;

	public float life = 2.0f;

	public float helicopterHeight = 5.0f;

    private bool rotateAndranslate = false;
    private float last_distance_from_player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");

		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        

        if (PlayerNearEvaluation())
		{
            Vector3 objPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
		 if (!distanceReached)
		{

                //			if (Vector3.Distance(transform.position, objPosition) > distance)
                //			{
                transform.Translate((player.transform.position - transform.position) * 0.8f * Time.deltaTime);
                Vector3 temp1 = player.transform.position;
                Vector3 temp2 = transform.position;
                temp1.y = 0;
                temp2.y = 0;
                last_distance_from_player = Vector3.Distance(temp1, temp2);

                //transform.position = Vector3.MoveTowards(transform.position, objPosition, Time.deltaTime*moveSpeed);

                //			}

                //if (!isStarDestroyer)

                //{
                //if ((Time.time - startTime) > startRotationTime)
                //       transform.RotateAround(player.transform.position, Vector3.up, Time.deltaTime * rotationSpeed);
                //}



                if (Vector3.Distance(transform.position, objPosition) <= distance)
			    distanceReached = true;
		}

            

            

		if (distanceReached)
		{
                rotateAndranslate = true;
				if (!isStarDestroyer)
				
				{
                    if (!isStarDestroyer)

                    {
                        //if ((Time.time - startTime) > startRotationTime)
                        //transform.RotateAround(player.transform.position, Vector3.up, Time.deltaTime * rotationSpeed);
                        transform.position = player.transform.position;
                        transform.Rotate(0, Time.deltaTime * rotationSpeed, 0);
                        transform.Translate(0, 0, -last_distance_from_player, Space.Self);
                    }
                    //if  ((Time.time - startTime) > startRotationTime)
                    //transform.RotateAround(player.transform.position, Vector3.up, Time.deltaTime * rotationSpeed);
                }

				else if (isStarDestroyer)
				{
				
						Vector3 direction = transform.position - player.transform.position;
						this.transform.forward = -direction;

				}

			if (Vector3.Distance(transform.position, objPosition) > distance)
				distanceReached = false;


//			transform.SetParent(player.transform);

		}
            transform.forward = player.transform.position - transform.position;
            transform.position = new Vector3(transform.position.x, player.transform.position.y + helicopterHeight, transform.position.z);

		if (((Time.time - lastShoot) > shootingRate) && PlayerInSight())
		{
			lastShoot = Time.time;
			Shoot();
			}}
	}

	void Shoot()
	{
//		if (missilePrefab != null && point != null)
//		{
		for (int i = 0 ; i < point.Length; i++) {
			GameObject missile = (GameObject) Instantiate(missilePrefab, point[i].transform.position, point[i].transform.rotation) as GameObject;
		}
	}
	
	//TODO
	bool PlayerInSight()
	{
		return true;
	}

	void Kill()
	{
		GameController.instance.playerSpecialEnergy += 1.0f;
		Destroy(this.gameObject);
	}

	public void Hit(float lifePoints)
	{
		life -= lifePoints;
		ControlIfDie();
	}

	void ControlIfDie()
	{
		if (life <= 0.0f)
			Kill ();
	}

	bool PlayerNearEvaluation()
	{

		if ((Vector3.Distance(transform.position, player.transform.position) < playerDistanceToShoot))
			return true;
		else
			return false;
		Debug.Log (Vector3.Distance(transform.position, player.transform.position));

	}

	void OnTriggerEnter( Collider other) {
		if(other.gameObject.tag == "Bullet")
			Hit (1);
	}
}
