using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	float minSideAngle = 76f;
	float maxSideAngle = 104f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	void OnCollisionEnter(Collision other) {
//		if(other.gameObject.tag == "Bullet") {
//			if(!InputController.instance.canDeflect) {
//				Destroy(other.gameObject);
//			} else if(InputController.instance.canDeflect) {
//				Vector3 collisionPoint = other.contacts[0].point;
//				collisionPoint.y = 0;
//				float collisionAngle = Angle(other.transform.forward, collisionPoint - other.transform.position);
//				//Debug.DrawRay(col.transform.position, (collisionPoint - col.transform.position) * 2f, Color.green, 5f);
//				float absoluteCollisionAngle = Mathf.Abs(collisionAngle);
//				
//				if (absoluteCollisionAngle < maxSideAngle && absoluteCollisionAngle > minSideAngle)
//				{
//					//D-C zone
//					//Side reflection, invert the result
//					//Debug.LogWarning("+++C zone collisionAngle:" + collisionAngle);
//					//transform.forward = -(transform.forward - 2 * (Vector3.Dot(transform.forward, col.transform.forward) * col.transform.forward));
//					Vector3 reflection = transform.forward - 2 * (Vector3.Dot(transform.forward, other.transform.forward) * other.transform.forward);
//					float product = 0;
//					if (collisionAngle < maxSideAngle && collisionAngle > minSideAngle)
//						product = Vector3.Dot(reflection, -other.transform.right);
//					else product = Vector3.Dot(reflection, other.transform.right);
//					transform.forward = (product > 0) ? reflection : -reflection;
//				}
//				else if ((absoluteCollisionAngle < minSideAngle))
//				{
//					//A zone
//					//Debug.LogWarning("*A zone collisionAngle:" + collisionAngle + ".");
//					float product = 0;
//					Vector3 reflection = transform.forward - 2 * (Vector3.Dot(transform.forward, other.transform.forward) * other.transform.forward);
//					if (collisionAngle < minSideAngle)
//						product = Vector3.Dot(reflection, other.transform.forward);
//					else product = Vector3.Dot(reflection, -other.transform.forward);
//					transform.forward = (product >= 0) ? reflection : -reflection;
//				}
//				else if ((absoluteCollisionAngle > maxSideAngle))
//				{
//					//B zone
//					float product = 0;
//					//Vector3 reflection = transform.forward - 2 * (Vector3.Dot(transform.forward, col.transform.forward) * col.transform.forward);
//					Vector3 reflection = transform.forward - 2 * (Vector3.Dot(transform.forward, other.transform.forward) * other.transform.forward);
//					//if (collisionAngle > maxSideAngle)
//					//    product = Vector3.Dot(reflection, col.transform.forward);
//					//else product = Vector3.Dot(reflection, -col.transform.forward);
//					transform.forward = (product >= 0) ? reflection : -reflection;
//					//transform.forward = reflection;
//				}
//			}
//		}
//	}

//	public static float Angle(Vector3 referenceForward, Vector3 target ){
//		// the vector perpendicular to referenceForward (90 degrees clockwise)
//		// (used to determine if angle is positive or negative)
//		Vector3 referenceRight= Vector3.Cross(Vector3.up, referenceForward);
//		
//		// Get the angle in degrees between 0 and 180
//		float angle = Vector3.Angle(target, referenceForward);
//		
//		// Determine if the degree value should be negative.  Here, a positive value
//		// from the dot product means that our vector is on the right of the reference vector   
//		// whereas a negative value means we're on the left.
//		float sign = Mathf.Sign(Vector3.Dot(target, referenceRight));
//		
//		return sign * angle;
//	}
}
