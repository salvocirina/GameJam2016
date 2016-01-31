using UnityEngine;
using System.Collections;

public class ChoiceScene : MonoBehaviour {

	public bool[] hasChosen;

	public Choices choices;

	public Color[] colors;

	public UnityEngine.UI.Image movementsSprite;
	public UnityEngine.UI.Image gunSprite;
	public UnityEngine.UI.Image cannonSprite;
	public UnityEngine.UI.Image shieldSprite;
	public UnityEngine.UI.Image bodySprite;

	public float timerMax = 3.0f;

	float beginningTime = 0.0f;

	public UnityEngine.UI.Text text;

	bool started = false;

	void Start()
	{
		text.enabled = false;
	}

	void Update () {
		if (choices != null)
		{

			if (!hasChosen[0] && GetButtonDown(0, "A"))
			{
				choices.movements = 0;
				movementsSprite.color = colors[0];
				hasChosen[0] = true;
			}

			if (!hasChosen[1] && GetButtonDown(1, "A"))
			{
				choices.movements = 1;
				movementsSprite.color = colors[1];
				hasChosen[1] = true;
			}

			if (!hasChosen[2] && GetButtonDown(2, "A"))
			{
				choices.movements = 2;
				movementsSprite.color = colors[2];
				hasChosen[2] = true;
			}

			if (!hasChosen[3] && GetButtonDown(3, "A"))
			{
				choices.movements = 3;
				movementsSprite.color = colors[3];
				hasChosen[3] = true;
			}



			if (!hasChosen[0] && GetButtonDown(0, "B"))
			{
				choices.shield = 0;
				shieldSprite.color = colors[0];
				bodySprite.color = colors[0];
				hasChosen[0] = true;
			}
			
			if (!hasChosen[1] && GetButtonDown(1, "B"))
			{
				choices.shield = 1;
				shieldSprite.color = colors[1];
				bodySprite.color = colors[1];
				hasChosen[1] = true;
			}
			
			if (!hasChosen[2] && GetButtonDown(2, "B"))
			{
				choices.shield = 2;
				shieldSprite.color = colors[2];
				bodySprite.color = colors[2];
				hasChosen[2] = true;
			}
			
			if (!hasChosen[3] && GetButtonDown(3, "B"))
			{
				choices.shield = 3;
				shieldSprite.color = colors[3];
				bodySprite.color = colors[3];
				hasChosen[3] = true;
			}



			if (!hasChosen[0] && GetButtonDown(0, "X"))
			{
				choices.gun = 0;
				gunSprite.color = colors[0];
				hasChosen[0] = true;
			}
			
			if (!hasChosen[1] && GetButtonDown(1, "X"))
			{
				choices.gun = 1;
				gunSprite.color = colors[1];
				hasChosen[1] = true;
			}
			
			if (!hasChosen[2] && GetButtonDown(2, "X"))
			{
				choices.gun = 2;
				gunSprite.color = colors[2];
				hasChosen[2] = true;
			}
			
			if (!hasChosen[3] && GetButtonDown(3, "X"))
			{
				choices.gun = 3;
				gunSprite.color = colors[3];
				hasChosen[3] = true;
			}



			if (!hasChosen[0] && GetButtonDown(0, "Y"))
			{
				choices.cannon = 0;
				cannonSprite.color = colors[0];
				hasChosen[0] = true;
			}
			
			if (!hasChosen[1] && GetButtonDown(1, "Y"))
			{
				choices.cannon = 1;
				cannonSprite.color = colors[1];
				hasChosen[1] = true;
			}
			
			if (!hasChosen[2] && GetButtonDown(2, "Y"))
			{
				choices.cannon = 2;
				cannonSprite.color = colors[2];
				hasChosen[2] = true;
			}
			
			if (!hasChosen[3] && GetButtonDown(3, "Y"))
			{
				choices.cannon = 3;
				cannonSprite.color = colors[3];
				hasChosen[3] = true;
			}
		}

		if (!started)
			ControlIfChosen();

		if (started)
		{
			text.enabled = true;
			text.text = Mathf.CeilToInt(-(Time.time - beginningTime - timerMax)).ToString();
		}

	}

	bool GetButtonDown(int player, string name) {
		return Rewired.ReInput.players.GetPlayer(player).GetButtonDown(name);
	}

	void ControlIfChosen()
	{
		for (int i = 0; i < hasChosen.Length; i++)
		{
			if (!hasChosen[i])
			{
				return;
			}
		}


		beginningTime = Time.time;
		started = true;

		StartCoroutine(Chosen());

	}

	IEnumerator Chosen()
	{

		//Debug.Log (diffTime);
		yield return new WaitForSeconds(timerMax);
		Application.LoadLevel(2);
	}
}
