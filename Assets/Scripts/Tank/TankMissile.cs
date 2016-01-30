using UnityEngine;
using System.Collections;

public class TankMissile : MonoBehaviour {

	Rigidbody objRigidbody;

	public float speed = 3.0f;

	public float destroyTime = 5.0f;
	float beginningTime;

	// Use this for initialization
	void Start () {
		objRigidbody = GetComponent<Rigidbody>();
		if (objRigidbody != null)
			objRigidbody.AddForce(transform.forward * speed);

		beginningTime = Time.time;
	}

	void Update()
	{
		if ((Time.time - beginningTime) > destroyTime)
			AutoDestroy();
	}

	public void SetSpeed(float _speed)
	{
		speed = _speed;
	}

	public void SetForwardDirection(Vector3 direction)
	{
		transform.forward = direction;
	}

	void AutoDestroy()
	{
		Destroy(this.gameObject);
	}
}