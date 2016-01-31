using UnityEngine;
using System.Collections;

public class SuccessImage : MonoBehaviour {

	public UnityEngine.UI.Image successImage;

	// Use this for initialization
	void Start () {
		successImage = GetComponent<UnityEngine.UI.Image>();
	}
	
	// Update is called once per frame
	void Update () {
		successImage.fillAmount += Time.unscaledDeltaTime * 0.5f;

		if (successImage.fillAmount == 1.0f)
		{
			Time.timeScale = 1.0f;
			Destroy(this);
		}
	}
}
