using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	private Rigidbody rb;
	public float movSpeed = 25.0f;
	public float rotSpeed = 15.0f;
	public string horizontalAxis;
	public string horizontalAimAxis;

	public string veritcalAxis;
	public string veritcalAimAxis;

	void Awake() {

		rb = GetComponent<Rigidbody>();
	}

	void Start () {
		horizontalAxis += gameObject.name;
		veritcalAxis += gameObject.name;
		horizontalAimAxis += gameObject.name;
		veritcalAimAxis += gameObject.name;
	}
	
	// Update is called onceper frame
	void Update () {

		float moveH = Input.GetAxis(horizontalAxis) * movSpeed;
		float moveV = Input.GetAxis(veritcalAxis) * movSpeed;
		Move(moveH, moveV);

		float aimH = Input.GetAxis(horizontalAimAxis) * rotSpeed;
		float aimV = Input.GetAxis(veritcalAimAxis) * rotSpeed;
		Aim(aimH, aimV);
	}

	void Aim(float xAxis , float zAxis) {
		Vector3 targetDir = new Vector3(xAxis , 0 , zAxis);
		Vector3 newDir = Vector3.RotateTowards(transform.GetChild(0).forward, targetDir , Time.deltaTime , 0.0f);
		transform.GetChild(0).rotation = 	Quaternion.LookRotation(newDir);
	}

	void Move(float xAxis , float zAxis) {
		rb.velocity = new Vector3(xAxis , 0 , zAxis);
	}

}
