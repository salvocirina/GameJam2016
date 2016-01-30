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

//	public string horizontalAxis;
//	public string horizontalAimAxis;
//
//	public string veritcalAxis;
//	public string veritcalAimAxis;
//
//	public string horizontalShieldAxis;
//	public string veritcalShieldAxis;
//
//	public string horizontalRocketAxis;
//	public string veritcalRocketAxis;
//
//	public string horizontalAimGatlinAxis;

	public GameObject bulletPrefab;
	public Transform[] gatlinLeftSpawnPoint;
	public Transform[] gatlinRightSpawnPoint;
	public Transform[] rocketSpawnPoint;

	public bool canDeflect;
//	public string veritcalAimGatlinAxis;

	void Awake() {

		rb = GetComponent<Rigidbody>();
		instance = this;
	}

	void Start () {

		runSpeed = movSpeed * 2.0f;

		improvedrocketShootingRate = rocketShootingRate/2;

//		horizontalAxis += gameObject.name;
//		veritcalAxis += gameObject.name;
//
//		horizontalAimAxis += gameObject.name;
//		veritcalAimAxis += gameObject.name;
//
//		horizontalShieldAxis += gameObject.transform.GetChild(1).name;
//		veritcalShieldAxis += gameObject.transform.GetChild(1).name;
//
//		horizontalRocketAxis += gameObject.transform.GetChild(2).name;
//		veritcalRocketAxis += gameObject.transform.GetChild(2).name;
//
//		horizontalAimGatlinAxis += gameObject.transform.GetChild(0).GetChild(0).name;
//		veritcalAimGatlinAxis += gameObject.transform.GetChild(0).GetChild(0).name;
	}
	
	// Update is called onceper frame
	void Update () {
		Debug.Log("Rewired shoot: " + Rewired.ReInput.players.GetPlayer(0).GetButton("Shoot"));
		Debug.Log("Rewired h axis left: " + GetAxis(1, "IperShoot"));

//			float moveH = GetAxis(0,"LeftRotationH") * runSpeed;
//			float moveV = GetAxis(0,"LeftRotationV") * runSpeed;
//			Move(moveH, moveV);
		if(GetAxis(0,"ShootIper") > 0.1f){
			float moveH = GetAxis(0,"LeftRotationH") * runSpeed;
			float moveV = GetAxis(0,"LeftRotationV") * runSpeed;
			Move(moveH, moveV);
		} else {
			float moveH = GetAxis(0,"LeftRotationH") * movSpeed;
			float moveV = GetAxis(0,"LeftRotationV") * movSpeed;
			Move(moveH, moveV);
		}

		float aimH = GetAxis(0,"RightRotationH") * rotSpeed;
		float aimV = GetAxis(0,"RightRotationV") * rotSpeed;
		Aim(aimH, aimV);

		float aimGatlinH = GetAxis(1, "RightRotationH") * rotGatlinSpeed;
		RotateGatlinGun(aimGatlinH);

		if(GetAxis(1,"Shoot") > 0.1f) {
			if(((Time.time - lastGatlinShoot) > gatlingShootingRate)) {
				lastGatlinShoot = Time.time;
				GatlinShoot();
			}
		} else if(GetAxis(1,"ShootIper") > 0.1f) {
			if(((Time.time - lastGatlinShoot) > gatlingShootingRate)) {
				lastGatlinShoot = Time.time;
				IperGatlinShoot();
			}
		}

		float shieldH = GetAxis(2, "RightRotationH") * rotShieldSpeed;
		float shieldV = GetAxis(2,"RightRotationV") * rotShieldSpeed;
		Shield(shieldH, shieldV);

		if(GetButtonDown(2,"ShootIper")) {
			GameController.instance.playerLife += 50.0f;
			GameController.instance.energy -= 10.0f;
		}

		float rocketH = GetAxis(3, "RightRotationH") * rotRocketSpeed;
		float rocketV = GetAxis(3,"RightRotationV") * rotRocketSpeed;
		RocketsAim(rocketH, rocketV);

		if(GetAxis(3,"Shoot") > 0.1f) {
			if(((Time.time - lastRocketShoot) > rocketShootingRate)) {
				lastRocketShoot = Time.time;
				RocketShoot();
			}
		} else if(GetAxis(3,"ShootIper") > 0.1f) {
			if(((Time.time - lastRocketShoot) > improvedrocketShootingRate)) {
				lastRocketShoot = Time.time;
				IperRocketShoot();
			}
		}

		/*
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
		 */

	}

	bool GetButton(int player, string name) {
		return Rewired.ReInput.players.GetPlayer(player).GetButton(name);
	}

	bool GetButtonDown(int player, string name) 
	{
		return Rewired.ReInput.players.GetPlayer(player).GetButtonDown(name);
	}

	float GetAxis(int player, string name) {
		return Rewired.ReInput.players.GetPlayer(player).GetAxis(name);
	}

	void Aim(float xAxis , float zAxis) {
		Vector3 targetDir = new Vector3(xAxis , 0 , zAxis);
		
		if (targetDir.sqrMagnitude > 0.21f) {
			// rotation of the ship
			var rotation= Quaternion.LookRotation(targetDir, Vector3.up);
			transform.GetChild(1).rotation = Quaternion.Slerp(transform.GetChild(1).rotation, rotation, Time.deltaTime * rotSpeed);

		}
	}

	void Move(float xAxis , float zAxis) {
		rb.velocity = new Vector3(xAxis , 0 , zAxis);

		Vector3 targetDir = new Vector3(xAxis , 0 , zAxis);
		
		if (targetDir.sqrMagnitude > 0.21f) {
			// rotation of the ship
			var rotation= Quaternion.LookRotation(targetDir, Vector3.up);
			transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, rotation, Time.deltaTime * rotSpeed);
			
		}

		GameController.instance.energy -= 1.0f/Time.deltaTime;
	}


	void RotateGatlinGun(float yAxis ) {

		transform.GetChild(1).GetChild(0).localEulerAngles = new Vector3(0, yAxis, 0);
		transform.GetChild(1).GetChild(1).localEulerAngles = new Vector3(0, yAxis , 0);
		yAxis = Mathf.Clamp(yAxis, -minClampGatlin, maxClampGatlin);

	}

	void GatlinShoot() {


		GameObject leftBullet = Instantiate (bulletPrefab , gatlinLeftSpawnPoint[0].position , gatlinLeftSpawnPoint[0].transform.rotation) as GameObject;
		Rigidbody bulletLeftRb = leftBullet.GetComponent<Rigidbody>();

		bulletLeftRb.AddForce(leftBullet.transform.up * gatlinShootForce);

		Destroy(leftBullet , 3.0f);

		GameObject rightBullet = Instantiate (bulletPrefab , gatlinRightSpawnPoint[0].position , gatlinRightSpawnPoint[0].transform.rotation) as GameObject;
		Rigidbody bulletRightRb = rightBullet.GetComponent<Rigidbody>();
		
		bulletRightRb.AddForce(rightBullet.transform.up * gatlinShootForce);
		
		Destroy(rightBullet , 3.0f);

		GameController.instance.energy -= 1.0f/gatlingShootingRate;
	
	}

	void IperGatlinShoot() {
		
		for(int i = 0 ; i < gatlinLeftSpawnPoint.Length; i++) {
			GameObject leftBullet = Instantiate (bulletPrefab , gatlinLeftSpawnPoint[i].position , gatlinLeftSpawnPoint[i].transform.rotation) as GameObject;
			Rigidbody bulletLeftRb = leftBullet.GetComponent<Rigidbody>();
			
			bulletLeftRb.AddForce(leftBullet.transform.up * gatlinShootForce);
			
			Destroy(leftBullet , 3.0f);

			GameController.instance.energy -= 10.0f/gatlingShootingRate;

		}

		for(int i = 0 ; i < gatlinRightSpawnPoint.Length; i++) {
			GameObject rightBullet = Instantiate (bulletPrefab , gatlinRightSpawnPoint[i].position , gatlinRightSpawnPoint[i].transform.rotation) as GameObject;
			Rigidbody bulletRightRb = rightBullet.GetComponent<Rigidbody>();
			
			bulletRightRb.AddForce(rightBullet.transform.up * gatlinShootForce);
			
			Destroy(rightBullet , 3.0f);
		}
		
	}

	void Shield(float xAxis , float zAxis) {

		Vector3 targetDir = new Vector3(xAxis , 0 , zAxis);

		if (targetDir.sqrMagnitude > 0.21f) {
			// rotation of the ship
			var rotation= Quaternion.LookRotation(targetDir, Vector3.up);
			transform.GetChild(2).rotation = Quaternion.Slerp(transform.GetChild(2).rotation, rotation, Time.deltaTime * rotShieldSpeed);
		}

	}

	void RocketsAim(float xAxis , float zAxis) {
		
		Vector3 targetDir = new Vector3(xAxis , 0 , zAxis);
		
		if (targetDir.sqrMagnitude > 0.21f) {
			// rotation of the ship
			var rotation= Quaternion.LookRotation(targetDir, Vector3.up);
			transform.GetChild(1).GetChild(2).rotation = Quaternion.Slerp(transform.GetChild(1).GetChild(2).rotation, rotation, Time.deltaTime * rotRocketSpeed);
			transform.GetChild(1).GetChild(3).rotation = Quaternion.Slerp(transform.GetChild(1).GetChild(3).rotation, rotation, Time.deltaTime * rotRocketSpeed);
		}
	}

	void RocketShoot() {

		for ( int i = 0; i < rocketSpawnPoint.Length ; i ++ ) {
			GameObject bullet = Instantiate (bulletPrefab , rocketSpawnPoint[i].position , rocketSpawnPoint[i].transform.rotation) as GameObject;
			Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
			
			bulletRb.AddForce(bullet.transform.up * gatlinShootForce);
			
			Destroy(bullet , 3.0f);

		}

		GameController.instance.energy -= 1.0f/rocketShootingRate;

	}

	void IperRocketShoot() {

		for ( int i = 0; i < rocketSpawnPoint.Length ; i ++ ) {

			GameObject bullet = Instantiate (bulletPrefab , rocketSpawnPoint[i].position , rocketSpawnPoint[i].transform.rotation) as GameObject;
			Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
			
			bulletRb.AddForce(bullet.transform.up * gatlinShootForce);
			
			Destroy(bullet , 3.0f);
		
		}

		GameController.instance.energy -= 10.0f/rocketShootingRate;
		
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
