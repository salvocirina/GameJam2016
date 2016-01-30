using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	private Rigidbody rb;

	public float movSpeed = 25.0f;
	public float rotSpeed = 15.0f;
	public float rotShieldSpeed = 35.0f;
	public float rotRocketSpeed = 35.0f;

	public float gatlinShootForce = 15.0f;

	private float lastShoot = 0;

	public float gatlingShootingRate = 0.5f;

	public float minClampGatlin = -45.0f;
	public float maxClampGatlin = 45.0f;

	public string horizontalAxis;
	public string horizontalAimAxis;

	public string veritcalAxis;
	public string veritcalAimAxis;

	public string horizontalShieldAxis;
	public string veritcalShieldAxis;

	public string horizontalRocketAxis;
	public string veritcalRocketAxis;

	public string horizontalAimGatlinAxis;

	public GameObject bulletPrefab;
	public Transform gatlinLeftSpawnPoint;
	public Transform gatlinRightSpawnPoint;
	public Transform rocketSpawnPoint;
//	public string veritcalAimGatlinAxis;

	void Awake() {

		rb = GetComponent<Rigidbody>();

	}

	void Start () {
		horizontalAxis += gameObject.name;
		veritcalAxis += gameObject.name;

		horizontalAimAxis += gameObject.name;
		veritcalAimAxis += gameObject.name;

		horizontalShieldAxis += gameObject.transform.GetChild(1).name;
		veritcalShieldAxis += gameObject.transform.GetChild(1).name;

		horizontalRocketAxis += gameObject.transform.GetChild(2).name;
		veritcalRocketAxis += gameObject.transform.GetChild(2).name;

		horizontalAimGatlinAxis += gameObject.transform.GetChild(0).GetChild(0).name;
//		veritcalAimGatlinAxis += gameObject.transform.GetChild(0).GetChild(0).name;
	}
	
	// Update is called onceper frame
	void Update () {

		float moveH = Input.GetAxis(horizontalAxis) * movSpeed;
		float moveV = Input.GetAxis(veritcalAxis) * movSpeed;
		Move(moveH, moveV);

		float aimH = Input.GetAxis(horizontalAimAxis) * rotSpeed;
		float aimV = Input.GetAxis(veritcalAimAxis) * rotSpeed;
		Aim(aimH, aimV);

		float aimGatlinH = Input.GetAxis(horizontalAimGatlinAxis) * rotSpeed;
//		float aimGatlinV = Input.GetAxis(veritcalAimGatlinAxis) * rotSpeed;
		RotateGatlinGun(aimGatlinH);
		if(Input.GetButton("GatlinFire")) {
			if(((Time.time - lastShoot) > gatlingShootingRate)) {
				lastShoot = Time.time;
				GatlinShoot();
			}
		}

		float shieldH = Input.GetAxis(horizontalShieldAxis) * rotShieldSpeed;
		float shieldV = Input.GetAxis(veritcalShieldAxis) * rotShieldSpeed;
		Shield(shieldH, shieldV);

		float rocketH = Input.GetAxis(horizontalRocketAxis) * rotRocketSpeed;
		float rocketV = Input.GetAxis(veritcalRocketAxis) * rotRocketSpeed;
		RocketsAim(rocketH, rocketV);

		if(Input.GetButton("RocketFire") ) {
			if(((Time.time - lastShoot) > gatlingShootingRate)) {
				lastShoot = Time.time;
				RocketShoot();
			}
		}

	}

	void Aim(float xAxis , float zAxis) {
		Vector3 targetDir = new Vector3(xAxis , 0 , zAxis);
		
		if (targetDir.sqrMagnitude > 0.21f) {
			// rotation of the ship
			var rotation= Quaternion.LookRotation(targetDir, Vector3.up);
			transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, rotation, Time.deltaTime * rotShieldSpeed);

		}
	}

	void Move(float xAxis , float zAxis) {
		rb.velocity = new Vector3(xAxis , 0 , zAxis);
	}

	void RotateGatlinGun(float yAxis ) {

		transform.GetChild(0).GetChild(0).localEulerAngles = new Vector3(0, yAxis, 0);
		transform.GetChild(0).GetChild(1).localEulerAngles = new Vector3(0, yAxis , 0);
		yAxis = Mathf.Clamp(yAxis, -minClampGatlin, maxClampGatlin);

	}

	void GatlinShoot() {


		GameObject leftBullet = Instantiate (bulletPrefab , gatlinLeftSpawnPoint.position , gatlinLeftSpawnPoint.transform.rotation) as GameObject;
		Rigidbody bulletLeftRb = leftBullet.GetComponent<Rigidbody>();

		bulletLeftRb.AddForce(leftBullet.transform.up * gatlinShootForce);

		Destroy(leftBullet , 3.0f);

		GameObject rightBullet = Instantiate (bulletPrefab , gatlinRightSpawnPoint.position , gatlinRightSpawnPoint.transform.rotation) as GameObject;
		Rigidbody bulletRightRb = rightBullet.GetComponent<Rigidbody>();
		
		bulletRightRb.AddForce(rightBullet.transform.up * gatlinShootForce);
		
		Destroy(rightBullet , 3.0f);
	
	}

	void Shield(float xAxis , float zAxis) {

		Vector3 targetDir = new Vector3(xAxis , 0 , zAxis);

		if (targetDir.sqrMagnitude > 0.21f) {
			// rotation of the ship
			var rotation= Quaternion.LookRotation(targetDir, Vector3.up);
			transform.GetChild(1).rotation = Quaternion.Slerp(transform.GetChild(1).rotation, rotation, Time.deltaTime * rotShieldSpeed);
		}

	}

	void RocketsAim(float xAxis , float zAxis) {
		
		Vector3 targetDir = new Vector3(xAxis , 0 , zAxis);
		
		if (targetDir.sqrMagnitude > 0.21f) {
			// rotation of the ship
			var rotation= Quaternion.LookRotation(targetDir, Vector3.up);
			transform.GetChild(2).rotation = Quaternion.Slerp(transform.GetChild(2).rotation, rotation, Time.deltaTime * rotShieldSpeed);
		}
	}

	void RocketShoot() {

		GameObject bullet = Instantiate (bulletPrefab , rocketSpawnPoint.position , rocketSpawnPoint.transform.rotation) as GameObject;
		Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
		
		bulletRb.AddForce(bullet.transform.up * gatlinShootForce);
		
		Destroy(bullet , 3.0f);

	}

}
