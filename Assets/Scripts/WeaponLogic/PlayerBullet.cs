using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Tank")
		{
			EnemyTank tank = other.gameObject.GetComponent<EnemyTank>();
			if (tank != null)
				tank.Hit(1.0f);
			
			Autodestroy();
		}
	}
	
	void Autodestroy()
	{
		Destroy(this.gameObject);
	}

}
