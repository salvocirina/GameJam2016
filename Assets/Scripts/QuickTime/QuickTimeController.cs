using UnityEngine;
using System.Collections;

public class QuickTimeController : MonoBehaviour {

	public enum AnimationType
	{
		Default,
		First,
		Second,
		Third,
		Four
	}

	[SerializeField]
	public QuickTimeElement[] quickTimeElements;

	public float timeNeeded = 3.0f;
	float beginningTime = 0.0f;

	public int elementsLenght = 10;

	[SerializeField]
	public QuickTimeElement[] playerZeroElem;
	public QuickTimeElement[] playerUnoElem;
	public QuickTimeElement[] playerTwoElem;
	public QuickTimeElement[] playerThreeElem;

//	[SerializeField]
//	public QuickTimeElement[][] playerElements;

	public UnityEngine.UI.Text text;

	public bool[] correct;

	bool ended = false;

	bool started = false;

	public QuickTimeSingle[] quickTimeSingles;

	public UnityEngine.Animator[] animators;

	public QuickTimeButtons quickTimeButtons;

	public GameObject inGameUI;
	public GameObject comboUI;

	public float waitingTimeBeforeCombo = 4.0f;
	public float waitingTimeAfterCombo = 2.0f;

	HugeExplosion hugeExplosion;

	float stoppingTime;

	//public UnityEngine.UI.Image successImage;
	public GameObject successImage;

	bool showSuccess = false;

	//if (

	// Use this for initialization
	void Start () {
//		beginningTime = Time.time;
//
//		GenerateElements();

		hugeExplosion = GetComponent<HugeExplosion>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.B))
			StartCoroutine(StartQuickTime());
			
		if (Input.GetKeyUp(KeyCode.F))
			hugeExplosion.Explode();


//		if (Input.GetKeyUp(KeyCode.F))
//			DoAnimation(0, AnimationType.First);
//
//		if (Input.GetKeyUp(KeyCode.G))
//			DoAnimation(0, AnimationType.Second);
//
//		if (Input.GetKeyUp(KeyCode.R))
//			DoAnimation(0, AnimationType.Third);
//
//		if (Input.GetKeyUp(KeyCode.T))
//			DoAnimation(0, AnimationType.Four);

		if (started)
			HandleQuickTime();

		HandleSuccessImage();
	}

	void GenerateElements()
	{
//		playerElements = new QuickTimeElement[4][];
//		for (int i = 0; i < playerElements.Length; i++)
//		{
//			playerElements[i] = new QuickTimeElement[elementsLenght];
//
//			for (int j = 0; j < playerElements[i].Length; j++)
//			{
//				int elemIndex = Random.Range(0, quickTimeElements.Length - 1);
//				playerElements[i][j] = quickTimeElements[elemIndex];
//			}
//		}

		playerZeroElem = new QuickTimeElement[elementsLenght];
		playerUnoElem = new QuickTimeElement[elementsLenght];
		playerTwoElem = new QuickTimeElement[elementsLenght];
		playerThreeElem = new QuickTimeElement[elementsLenght];

		for (int i = 0; i < playerZeroElem.Length; i++)
		{
			int elemIndex = Random.Range(0, quickTimeElements.Length);
			playerZeroElem[i] = quickTimeElements[elemIndex];
		}

		for (int i = 0; i < playerUnoElem.Length; i++)
		{
			int elemIndex = Random.Range(0, quickTimeElements.Length);
			playerUnoElem[i] = quickTimeElements[elemIndex];
		}

		for (int i = 0; i < playerTwoElem.Length; i++)
		{
			int elemIndex = Random.Range(0, quickTimeElements.Length);
			playerTwoElem[i] = quickTimeElements[elemIndex];
		}

		for (int i = 0; i < playerThreeElem.Length; i++)
		{
			int elemIndex = Random.Range(0, quickTimeElements.Length);
			playerThreeElem[i] = quickTimeElements[elemIndex];
		}
	}

	void SetQuickTimeSingleELements()
	{

		if (quickTimeSingles[0] != null)
		{
			quickTimeSingles[0].SetElements(playerZeroElem);
			quickTimeSingles[0].Reset();
			quickTimeSingles[0].Active();
		}

		if (quickTimeSingles[1] != null)
		{
			quickTimeSingles[1].SetElements(playerUnoElem);
			quickTimeSingles[1].Reset();
			quickTimeSingles[1].Active();
		}

		if (quickTimeSingles[2] != null)
		{
			quickTimeSingles[2].SetElements(playerTwoElem);
			quickTimeSingles[2].Reset();
			quickTimeSingles[2].Active();
		}

		if (quickTimeSingles[3] != null)
		{
			quickTimeSingles[3].SetElements(playerThreeElem);
			quickTimeSingles[3].Reset();
			quickTimeSingles[3].Active();
		}

	}

	public void BeginQuickTime()
	{
		StartCoroutine(StartQuickTime());
	}

	IEnumerator StartQuickTime()
	{
		successImage.SetActive(false);

		ended = false;

		SwitchUI(false);
		
		quickTimeButtons.EnableButtonsGraphic(false);
		
		GenerateElements();

		text.enabled = false;

		yield return new WaitForSeconds(waitingTimeBeforeCombo);

		text.enabled = true;

		Time.timeScale = 0.0f;

		quickTimeButtons.EnableButtonsGraphic(true);

		SetButtonSprites();

		//EnableUI(false);

		//SwitchCameras(false);
		//Debug.Log ("inizio ");

		for (int i = 0; i < quickTimeSingles.Length; i++)
		{
			if (quickTimeSingles[i] != null)
				quickTimeSingles[i].Active(true);
		}

		beginningTime = Time.unscaledTime;



		started = true;

		for (int i = 0; i < correct.Length; i++)
			correct[i] = false;

		SetQuickTimeSingleELements();
	}

	void HandleQuickTime()
	{
		float remainingTime = -(Time.unscaledTime - beginningTime - timeNeeded);

		float remainingCut = Round(remainingTime, 2);

		int remainingInt = Mathf.CeilToInt(remainingTime);

		if (text != null)
			text.text = remainingInt.ToString();

		if (remainingTime < 0.0f && !ended)
		{
			remainingTime = 0.0f;
			ended = true;
			started = false;
			//Debug.Log ("finito");
			HandleEnd();
		}
	}

	public static float Round(float value, int digits)
	{
		float mult = Mathf.Pow(10.0f, (float)digits);
		return Mathf.Round(value * mult) / mult;
	}

	public void SetCorrect(int i)
	{
		if (i < quickTimeElements.Length)
		{
			correct[i] = true;
		}
	}

	void HandleEnd()
	{
		bool finalCorrect = true;
		for (int i = 0; i < correct.Length; i++)
		{
			if (!correct[i])
			{
				finalCorrect = false;
				break;
			}
		}

		if (finalCorrect)
			//StartCoroutine( WinConsequences());
			WinConsequences();
		else
			LoseConsequences();
	}

	public void Fail()
	{
		LoseConsequences();
	}

	void WinConsequences()
	{
		Debug.Log("Inizio Vinto quick time event");

		//EnableUI(true);
		//SwitchCameras(true);
		for (int i = 0; i < quickTimeSingles.Length; i++)
		{
			if (quickTimeSingles[i] != null)
				quickTimeSingles[i].Active(false);
	    }

		successImage.SetActive(true);

		//showSuccess = true;

		//yield return new WaitForSeconds(3.0f);

		Invoke("LateCons", 1.0f);
	}

	void LateCons()
	{
		//Time.timeScale = 1.0f;
		
		SwitchUI(true);
		Debug.Log("Vinto quick time event");
		
		if (hugeExplosion != null)
			hugeExplosion.Explode();
		
		//showSuccess = false;
		

	}

	void HandleSuccessImage()
	{
//		if (showSuccess)
//		{
//			successImage.fillAmount += Time.unscaledDeltaTime * 0.5f;
//		}
	}


	void LoseConsequences()
	{
		Time.timeScale = 1.0f;

		//EnableUI(true);
		//SwitchCameras(true);
		for (int i = 0; i < quickTimeSingles.Length; i++)
		{
			if (quickTimeSingles[i] != null)
				quickTimeSingles[i].Active(false);
		}
		SwitchUI(true);
		Debug.Log("Fallito quick time event");
	}

	public void DoAnimation(int player, AnimationType type)
	{
		//Debug.Log (player + " "  + type);

		if (animators[player] != null)
		{
			if (type == AnimationType.First)
			{
				animators[player].SetBool("Anim1", true);
				animators[player].SetBool("Anim2", false);
				animators[player].SetBool("Anim3", false);
				animators[player].SetBool("Anim4", false);
			}

			if (type == AnimationType.Second)
			{
				animators[player].SetBool("Anim1", false);
				animators[player].SetBool("Anim2", true);
				animators[player].SetBool("Anim3", false);
				animators[player].SetBool("Anim4", false);
			}

			if (type == AnimationType.Third)
			{
				animators[player].SetBool("Anim1", false);
				animators[player].SetBool("Anim2", false);
				animators[player].SetBool("Anim3", true);
				animators[player].SetBool("Anim4", false);
			}

			if (type == AnimationType.Four)
			{
				animators[player].SetBool("Anim1", false);
				animators[player].SetBool("Anim2", false);
				animators[player].SetBool("Anim3", false);
				animators[player].SetBool("Anim4", true);
			}
		}
	}


	void SwitchUI(bool inGame = true)
	{
		if (inGameUI != null && comboUI != null)
		{
			inGameUI.SetActive(inGame);
			comboUI.SetActive(!inGame);
		}
	}

	void SetButtonSprites()
	{
		if (quickTimeButtons != null)
		{
			//Debug.Log ("adsasdasd");
			Sprite[] _sprites = new Sprite[elementsLenght];

			for (int i = 0; i < playerZeroElem.Length; i++)
			{
				//Debug.Log ("adsasdasd");
				_sprites[i] = playerZeroElem[i].buttonSprite;
			}

			quickTimeButtons.SetPlayerImages(0, _sprites);


			_sprites = new Sprite[elementsLenght];
			
			for (int i = 0; i < playerUnoElem.Length; i++)
			{
				_sprites[i] = playerUnoElem[i].buttonSprite;
			}
			
			quickTimeButtons.SetPlayerImages(1, _sprites);


			_sprites = new Sprite[elementsLenght];
			
			for (int i = 0; i < playerTwoElem.Length; i++)
			{
				_sprites[i] = playerTwoElem[i].buttonSprite;
			}
			
			quickTimeButtons.SetPlayerImages(2, _sprites);


			_sprites = new Sprite[elementsLenght];
			
			for (int i = 0; i < playerThreeElem.Length; i++)
			{
				_sprites[i] = playerThreeElem[i].buttonSprite;
			}
			
			quickTimeButtons.SetPlayerImages(3, _sprites);
		}
	}

	public void PressCorrectButton(int playerNumber, int spriteNumber)
	{
		quickTimeButtons.PressCorrectButton(playerNumber, spriteNumber, GetUnpressedSprite(playerNumber, spriteNumber));
	}

	Sprite GetUnpressedSprite(int playerNumber, int spriteNumber)
	{
		if (playerNumber == 0)
		{
			return playerZeroElem[spriteNumber].buttonSpriteDisabled;
		}

		if (playerNumber == 1)
		{
			return playerUnoElem[spriteNumber].buttonSpriteDisabled;
		}

		if (playerNumber == 2)
		{
			return playerTwoElem[spriteNumber].buttonSpriteDisabled;
		}

		if (playerNumber == 3)
		{
			return playerThreeElem[spriteNumber].buttonSpriteDisabled;
		}

		return new Sprite();
	}
}
