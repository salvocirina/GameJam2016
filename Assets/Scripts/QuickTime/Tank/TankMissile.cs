using UnityEngine;
using System.Collections;

public class TankMissile : MonoBehaviour {

	Rigidbody objRigidbody;

	public float speed = 3.0f;

	public float destroyTime = 5.0f;
	float beginningTime;

	GameObject gameController;
	GameController controllerScript;

	public GameController.HitType damage = GameController.HitType.Weak;

	// Use this for initialization
	void Start () {
		objRigidbody = GetComponent<Rigidbody>();
		if (objRigidbody != null)
			objRigidbody.AddForce(transform.forward * speed);

		beginningTime = Time.time;

		gameController = GameObject.FindGameObjectWithTag("GameController");
		if (gameController != null)
			controllerScript = gameController.GetComponent<GameController>();
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

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (controllerScript != null)
				controllerScript.TakeHit(GameController.HitType.Weak);

			Explode();
		}

		if (other.gameObject.tag == "Shield")
		{
			Debug.Log ("shield");
			Explode();
		}
	}

	void Explode()
	{
		Destroy(this.gameObject);
	}
}