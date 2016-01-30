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

	public float playerLife = 400.0f;
	private float maxPlayerLife;

	public UnityEngine.UI.Slider lifeSlider;

	public static GameController instance;


	void Awake() {
		instance = this;
	}
	// Use this for initialization
	void Start () {
		maxPlayerLife = playerLife;
	}
	
	// Update is called once per frame
	void Update () {

		if(playerLife > maxPlayerLife)
			playerLife = maxPlayerLife;

		lifeSlider.value = playerLife;
	}

	public void TakeHit(HitType type)
	{
		if (type == HitType.Weak)
		{
			playerLife -= 1f;
		}

		if (type == HitType.Normal)
		{
			playerLife -= 10f;
		}

		if (type == HitType.Strong)
		{
			playerLife -= 20f;
		}

//		lifeSlider.value = playerLife;
	}
}
