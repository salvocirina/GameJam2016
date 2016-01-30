using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	private Rigidbody rb;

	public float movSpeed = 25.0f;
	public float rotSpeed = 15.0f;
	public float rotShieldSpeed = 35.0f;

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



	public string horizontalAimGatlinAxis;

	public GameObject bulletPrefab;
	public Transform spawnPoint;
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

		if(Input.GetAxis("GatlinFire") > 0.1f) {
			if(((Time.time - lastShoot) > gatlingShootingRate)) {
				lastShoot = Time.time;
				GatlinShoot();
			}
		}

		float shieldH = Input.GetAxis(horizontalShieldAxis);// * rotShieldSpeed;
		float shieldV = Input.GetAxis(veritcalShieldAxis);// * rotShieldSpeed;
		Shield(shieldH, shieldV);

	}

	void Aim(float xAxis , float zAxis) {
		Vector3 targetDir = new Vector3(xAxis , 0 , zAxis);
		Vector3 newDir = Vector3.RotateTowards(transform.GetChild(0).forward, targetDir , Time.deltaTime , 0.0f);
		transform.GetChild(0).rotation = 	Quaternion.LookRotation(newDir);
	}

	void Move(float xAxis , float zAxis) {
		rb.velocity = new Vector3(xAxis , 0 , zAxis);
	}

	void RotateGatlinGun(float yAxis ) {

		transform.GetChild(0).GetChild(0).localEulerAngles = new Vector3(0, yAxis, 0);
		yAxis = Mathf.Clamp(yAxis, -minClampGatlin, maxClampGatlin);

	}

	void GatlinShoot() {

		GameObject bullet = Instantiate (bulletPrefab , spawnPoint.position , spawnPoint.transform.rotation) as GameObject;
		Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

		bulletRb.AddForce(bullet.transform.up * gatlinShootForce);

		Destroy(bullet , 5.0f);
	
	}

	void Shield(float xAxis , float zAxis) {

//		Vector3 targetDir = new Vector3(xAxis , 0 , zAxis);
//		Vector3 newDir = Vector3.RotateTowards(transform.GetChild(1).forward, targetDir , Time.deltaTime , 0.0f);
//		transform.GetChild(1).rotation = 	Quaternion.LookRotation(newDir);
//		transform.GetChild(1).forward = Vector3.Normalize(new Vector3(xAxis, 0f, zAxis));
		if(zAxis < 0.1f && xAxis < 0.1 && zAxis > -0.1 && xAxis > -0.1) 
			return;

		float tetha = Mathf.Atan2(zAxis,xAxis);
		Debug.Log(tetha);
		transform.GetChild(1).eulerAngles = new Vector3(0 , -tetha *rotShieldSpeed, 0 );

	}

}
