using UnityEngine;
using System.Collections;

public class QuickTimeSingle : MonoBehaviour {

	[SerializeField]
	public QuickTimeElement[] quickTimeElements;

	public QuickTimeController quickTimeController;

	int actualElementIndex = 0;

	public int playerNumber = 0;

	bool active = false;

	void Start () {
		if (quickTimeController == null)
		{
			GameObject fatherGo = transform.parent.gameObject;
			if (fatherGo != null)
				quickTimeController = fatherGo.GetComponent<QuickTimeController>();
		}
	}

	void Update () {

		if (active)
		{
			if (GetButtonDown(playerNumber, "A"))
			{
				//Debug.Log ("d'accordo");
				if (quickTimeElements[actualElementIndex].button == "A")
					Correctinput(true);
				else
					Correctinput(false);
			}
			
			if (GetButtonDown(playerNumber, "B"))
			{
				if (quickTimeElements[actualElementIndex].button == "B")
					Correctinput(true);
				else
					Correctinput(false);
			}
			
			if (GetButtonDown(playerNumber, "X"))
			{
				if (quickTimeElements[actualElementIndex].button == "X")
					Correctinput(true);
				else
					Correctinput(false);
			}
			
			if (GetButtonDown(playerNumber, "Y"))
			{
				if (quickTimeElements[actualElementIndex].button == "Y")
					Correctinput(true);
				else
					Correctinput(false);
			}
		}

		    



//		if (Input.GetKeyUp(KeyCode.N))
//			Correctinput(true);
//		else if (Input.GetKeyUp(KeyCode.M))
//			Correctinput(false);
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
			if (quickTimeElements[actualElementIndex].button == "A")
				quickTimeController.DoAnimation(playerNumber, QuickTimeController.AnimationType.First);
			if (quickTimeElements[actualElementIndex].button == "B")
				quickTimeController.DoAnimation(playerNumber, QuickTimeController.AnimationType.Second);
			if (quickTimeElements[actualElementIndex].button == "X")
				quickTimeController.DoAnimation(playerNumber, QuickTimeController.AnimationType.Third);
			if (quickTimeElements[actualElementIndex].button == "Y")
				quickTimeController.DoAnimation(playerNumber, QuickTimeController.AnimationType.Four);

			quickTimeController.PressCorrectButton(playerNumber, actualElementIndex);

			if (actualElementIndex == quickTimeElements.Length - 1)
			{
				active = false;

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

	public void Active(bool enable = true)
	{
		active = enable;
	}

	public void Reset()
	{
		actualElementIndex = 0;
	}

	bool GetButtonDown(int player, string name) {
		return Rewired.ReInput.players.GetPlayer(player).GetButtonDown(name);
	}
}
