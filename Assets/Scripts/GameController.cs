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

	public GameObject player;

	public float playerLife = 400.0f;
	private float maxPlayerLife;

	public float energy = 100.0f;
	public float energyRegen = 20.0f;
	bool canRegen = true;

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

		if(energy <= 0) {
			player.GetComponent<InputController>().disable = true;
			canRegen=false;
			energy=0;
			Invoke("ReEnableEngine",1);
		}
		if(canRegen && energy <= 100.0f)
		StartCoroutine(RegenEnergy());
	}

	IEnumerator RegenEnergy(){
		yield return new WaitForSeconds(1);
		if(energy <= 80)
			energy+=energyRegen;
		else if(energy >= 80)
			energy = 100.0f;
		StopAllCoroutines();
	
	}

	void ReEnableEngine(){
		player.GetComponent<InputController>().disable = false;
		canRegen=true;
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

	}
}
