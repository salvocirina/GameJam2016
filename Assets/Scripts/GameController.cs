using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public enum HitType
	{
		Default,
		Weak,
		Normal,
		Strong
	}

	float playerLife = 1.0f;

	public UnityEngine.UI.Slider lifeSlider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TakeHit(HitType type)
	{
		if (type == HitType.Weak)
		{
			playerLife -= 0.02f;
		}

		if (type == HitType.Normal)
		{
			playerLife -= 0.05f;
		}

		if (type == HitType.Strong)
		{
			playerLife -= 0.2f;
		}

		lifeSlider.value = playerLife;
	}
}
