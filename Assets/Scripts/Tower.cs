using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	GameObject player;
	public float rotatingSpeed = 3.0f;

	public float distanceToShoot = 300.0f;

	public float shootingRatio = 1.0f;
	float lastShoot = Mathf.NegativeInfinity;

	public GameObject bulletPrefab;
	public GameObject gun;
	public Transform shootingPoint;

	public float life = 20.0f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = player.transform.position - transform.position;


		//la torretta non deve piegarsi
		//direction = new Vector3(direction.x, 0.0f, direction.z);

		gun.transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * rotatingSpeed * 10000);

		float distance = Vector3.Distance(player.transform.position, transform.position);

		if (distance < distanceToShoot)
		{
			ShootHandle();
		}

	}

	void ShootHandle()
	{
		if ((Time.time - lastShoot) > shootingRatio)
		{
			//shootingPoint.transform.forward = player.transform.position - transform.position;

			//Debug.Log ("shoot");
			if (bulletPrefab != null && shootingPoint != null)
				Instantiate(bulletPrefab, shootingPoint.transform.position, shootingPoint.transform.rotation);
			lastShoot = Time.time;
		}
			
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

}
