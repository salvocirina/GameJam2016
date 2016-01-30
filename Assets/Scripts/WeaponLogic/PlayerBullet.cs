using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {


	void Start() {

		GetComponent<SphereCollider>().enabled = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Tank")
		{
			EnemyTank tank = other.gameObject.GetComponent<EnemyTank>();
			if (tank != null)
				tank.Hit(1.0f);
			
			Autodestroy();
		}

		if (other.gameObject.tag == "Helicopter")
		{
			Helicopter helicopter = other.gameObject.GetComponent<Helicopter>();
			if (helicopter != null) {

				helicopter.Hit(1.0f);
				GetComponent<SphereCollider>().transform.gameObject.SetActive(true);
			}
			
			Autodestroy();
		}
	}
	
	void Autodestroy()
	{
		Destroy(this.gameObject,.3f);
	}

}
