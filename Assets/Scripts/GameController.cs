using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

	public float playerSpecialEnergy = 0.0f;
	private float maxPlayerSpecialEnergy = 100.0f;

	public float energy = 100.0f;
	public float energyRegen = 20.0f;
	bool canRegen = true;

	public Slider lifeSlider;
	public Slider energySlider;

	public Image LifeSprite;

	public Image GlowSprite;

	public static GameController instance;


	void Awake() {
		instance = this;
	}
	// Use this for initialization
	void Start () {
		maxPlayerLife = playerLife;
		GlowSprite.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

		if(playerLife > maxPlayerLife)
			playerLife = maxPlayerLife;

		lifeSlider.value = playerLife; 
		energySlider.value = energy;

		if(playerLife <= 0) {

//			player.GetComponent<InputController>().disable = true):
		}

		if(energy <= 0) {
			player.GetComponent<InputController>().disable = true;
			canRegen=false;
			energy=0;
			Invoke("ReEnableEngine",1);
		}

		if(playerLife >= 300) {
			LifeSprite.color = new Color32(0 , 255 , 0 , 255);
		} 
			else if(playerLife < 300 && playerLife >= 150) {
			LifeSprite.color = new Color32(255 , 255 , 0 , 255);
		} else if(playerLife < 150 && playerLife >= 0) {
			LifeSprite.color = new Color32(255 , 0 , 0 , 255);
		}

		if(playerSpecialEnergy == maxPlayerSpecialEnergy) {
			GlowSprite.gameObject.SetActive(true);
			Debug.Log("Corrado Scelgo te!");
			playerSpecialEnergy = 0.0f;
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
