using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

	public bool bigBullet;

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

		if (other.gameObject.tag == "Tower")
		{
			Tower tower = other.gameObject.GetComponent<Tower>();
			if (tower != null) {
				
				tower.Hit(1.0f);
			}
			
			Autodestroy();
		}
	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.P))
			Autodestroy();
	}

	void Autodestroy()
	{
		if(bigBullet) {
			GameObject childGo = transform.GetChild(0).gameObject;
			childGo.SetActive(true);
			childGo.transform.SetParent(null);
		} 

		Destroy(this.gameObject,.3f);
	}

}
