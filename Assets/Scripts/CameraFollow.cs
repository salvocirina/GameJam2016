using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {


	public GameObject target;
	public float offset;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate() {
		transform.position = new Vector3(target.transform.position.x  , transform.position.y , target.transform.position.z - offset);
	}
}
