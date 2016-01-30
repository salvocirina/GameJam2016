using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

	public GameObject lifeImage;
	RectTransform lifeTransform;

	// Use this for initialization
	void Start () {
		lifeTransform = lifeImage.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetLife(float life)
	{

	}
}
