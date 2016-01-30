using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	private Rigidbody rb;

	public static InputController instance;

	public float movSpeed = 25.0f;
	private float runSpeed;
	public float rotSpeed = 15.0f;
	public float rotGatlinSpeed = 15.0f;
	public float rotShieldSpeed = 1.0f;
	public float rotRocketSpeed = 35.0f;

	public float gatlinShootForce = 15.0f;

	private float lastGatlinShoot = 0;
	private float lastRocketShoot = 0;

	public float gatlingShootingRate = 0.5f;
	public float rocketShootingRate = 0.5f;
	private float improvedrocketShootingRate;

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

	public bool canDeflect;
//	public string veritcalAimGatlinAxis;

	void Awake() {

		rb = GetComponent<Rigidbody>();
		instance = this;
	}

	void Start () {

		runSpeed = movSpeed * 2.0f;

		improvedrocketShootingRate = rocketShootingRate/2;

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



		if(Input.GetAxis("Run") > 0.1f){
			float moveH = Input.GetAxis(horizontalAxis) * runSpeed;
			float moveV = Input.GetAxis(veritcalAxis) * runSpeed;
			Move(moveH, moveV);
		} else {
			float moveH = Input.GetAxis(horizontalAxis) * movSpeed;
			float moveV = Input.GetAxis(veritcalAxis) * movSpeed;
			Move(moveH, moveV);
		}

		float aimH = Input.GetAxis(horizontalAimAxis) * rotSpeed;
		float aimV = Input.GetAxis(veritcalAimAxis) * rotSpeed;
		Aim(aimH, aimV);

		float aimGatlinH = Input.GetAxis(horizontalAimGatlinAxis) * rotGatlinSpeed;
//		float aimGatlinV = Input.GetAxis(veritcalAimGatlinAxis) * rotSpeed;
		RotateGatlinGun(aimGatlinH);
		if(Input.GetAxis("GatlinFire") > 0.1f) {
			if(((Time.time - lastGatlinShoot) > gatlingShootingRate)) {
				lastGatlinShoot = Time.time;
				GatlinShoot();
			}
		}

		float shieldH = Input.GetAxis(horizontalShieldAxis) * rotShieldSpeed;
		float shieldV = Input.GetAxis(veritcalShieldAxis) * rotShieldSpeed;
		Shield(shieldH, shieldV);

		float rocketH = Input.GetAxis(horizontalRocketAxis) * rotRocketSpeed;
		float rocketV = Input.GetAxis(veritcalRocketAxis) * rotRocketSpeed;
		RocketsAim(rocketH, rocketV);

		if(Input.GetAxis("RocketFire") > 0.1f) {
			if(((Time.time - lastRocketShoot) > rocketShootingRate)) {
				lastRocketShoot = Time.time;
				RocketShoot();
			}
		} else if (Input.GetAxis("ImprovedRocketFire") > 0.1f) {
			if(((Time.time - lastRocketShoot) > improvedrocketShootingRate)) {
				lastRocketShoot = Time.time;
				RocketShoot();
			}
		}

		EnableBouncingShield();

	}

	void Aim(float xAxis , float zAxis) {
		Vector3 targetDir = new Vector3(xAxis , 0 , zAxis);
		
		if (targetDir.sqrMagnitude > 0.21f) {
			// rotation of the ship
			var rotation= Quaternion.LookRotation(targetDir, Vector3.up);
			transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, rotation, Time.deltaTime * rotSpeed);

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
			transform.GetChild(2).rotation = Quaternion.Slerp(transform.GetChild(2).rotation, rotation, Time.deltaTime * rotRocketSpeed);
		}
	}

	void RocketShoot() {

		GameObject bullet = Instantiate (bulletPrefab , rocketSpawnPoint.position , rocketSpawnPoint.transform.rotation) as GameObject;
		Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
		
		bulletRb.AddForce(bullet.transform.up * gatlinShootForce);
		
		Destroy(bullet , 3.0f);

	}

	void EnableBouncingShield() {

		if(Input.GetAxis("BouncingShield") > 0.1f) {
			canDeflect = true;
		}
		else {
			canDeflect = false;
		}
	}

}
