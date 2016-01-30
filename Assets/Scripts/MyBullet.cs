using UnityEngine;
using System.Collections;

public class MyBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other ) {
		if(other.gameObject.tag == "Tank") {
			other.GetComponent<EnemyTank>().life -= 1.0f;
		}

		if ( other.gameObject.tag == "Helicopter") {
			Debug.Log("elicottero");
			other.GetComponent<Helicopter>().life = -1.0f;
		}
	}
}
