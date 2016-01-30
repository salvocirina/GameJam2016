using UnityEngine;
using System.Collections;

public class QuickTimeController : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
//		beginningTime = Time.time;
//
//		GenerateElements();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.B))
			StartQuickTime();

		if (started)
			HandleQuickTime();
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
			int elemIndex = Random.Range(0, quickTimeElements.Length - 1);
			playerZeroElem[i] = quickTimeElements[elemIndex];
		}

		for (int i = 0; i < playerUnoElem.Length; i++)
		{
			int elemIndex = Random.Range(0, quickTimeElements.Length - 1);
			playerUnoElem[i] = quickTimeElements[elemIndex];
		}

		for (int i = 0; i < playerTwoElem.Length; i++)
		{
			int elemIndex = Random.Range(0, quickTimeElements.Length - 1);
			playerTwoElem[i] = quickTimeElements[elemIndex];
		}

		for (int i = 0; i < playerThreeElem.Length; i++)
		{
			int elemIndex = Random.Range(0, quickTimeElements.Length - 1);
			playerThreeElem[i] = quickTimeElements[elemIndex];
		}
	}

	void SetQuickTimeSingleELements()
	{

		if (quickTimeSingles[0] != null)
		{
			quickTimeSingles[0].SetElements(playerZeroElem);
		}

		if (quickTimeSingles[1] != null)
		{
			quickTimeSingles[1].SetElements(playerUnoElem);
		}

		if (quickTimeSingles[2] != null)
		{
			quickTimeSingles[2].SetElements(playerTwoElem);
		}

		if (quickTimeSingles[3] != null)
		{
			quickTimeSingles[3].SetElements(playerThreeElem);
		}

	}

	void StartQuickTime()
	{
		Time.timeScale = 0.0f;

		GenerateElements();

		beginningTime = Time.unscaledTime;

		text.enabled = true;

		started = true;

		for (int i = 0; i < correct.Length; i++)
			correct[i] = false;

		SetQuickTimeSingleELements();
	}

	void HandleQuickTime()
	{
		float remainingTime = -(Time.unscaledTime - beginningTime - timeNeeded);

		float remainingCut = Round(remainingTime, 2);

		if (text != null)
			text.text = remainingCut.ToString();

		if (remainingTime < 0.0f && !ended)
		{
			remainingTime = 0.0f;
			ended = true;
			started = false;
			Debug.Log ("finito");
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
		Debug.Log("Vinto quick time event");
	}

	void LoseConsequences()
	{
		Debug.Log("Fallito quick time event");
	}
}
