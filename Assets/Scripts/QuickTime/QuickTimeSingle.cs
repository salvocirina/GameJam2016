using UnityEngine;
using System.Collections;

public class QuickTimeSingle : MonoBehaviour {

	[SerializeField]
	public QuickTimeElement[] quickTimeElements;

	public QuickTimeController quickTimeController;

	int actualElementIndex = 0;

	public int playerNumber = 0;

	void Start () {
		if (quickTimeController == null)
		{
			GameObject fatherGo = transform.parent.gameObject;
			if (fatherGo != null)
				quickTimeController = fatherGo.GetComponent<QuickTimeController>();
		}
	}

	void Update () {
		if (Input.GetKeyUp(KeyCode.N))
			Correctinput(true);
		else if (Input.GetKeyUp(KeyCode.M))
			Correctinput(false);
	}

	void NextElement()
	{
		actualElementIndex++;
	}

	void Correctinput(bool _correct)
	{
		//controlla se l'input dell'utente è coerente con il tasto premuto
		bool correct = _correct;

//		if (Input.GetKeyUp(KeyCode.N))
//			correct = true;
//		else if (Input.GetKeyUp(KeyCode.M))
//			correct = false;

		if (correct)
		{
			if (actualElementIndex == quickTimeElements.Length - 1)
			{
				if (quickTimeController != null)
					quickTimeController.SetCorrect(playerNumber);
			}
			else
			{
				NextElement();
			}
		}
		else
		{
			if (quickTimeController != null)
				quickTimeController.Fail();
		}
	}

	public void SetElements(QuickTimeElement[] _elements)
	{
		quickTimeElements = _elements;
	}
}
