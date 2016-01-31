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

	public bool[] tastoScelto;

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

			if (!hasChosen[0] && GetButtonDown(0, "A") && !tastoScelto[0])
			{
				choices.movements = 0;
				tastoScelto[0] = true;
				movementsSprite.color = colors[0];
				hasChosen[0] = true;
			}

			if (!hasChosen[1] && GetButtonDown(1, "A") && !tastoScelto[0])
			{
				choices.movements = 1;
				tastoScelto[0] = true;
				movementsSprite.color = colors[1];
				hasChosen[1] = true;
			}

			if (!hasChosen[2] && GetButtonDown(2, "A") && !tastoScelto[0])
			{
				choices.movements = 2;
				tastoScelto[0] = true;
				movementsSprite.color = colors[2];
				hasChosen[2] = true;
			}

			if (!hasChosen[3] && GetButtonDown(3, "A") && !tastoScelto[0])
			{
				choices.movements = 3;
				tastoScelto[0] = true;
				movementsSprite.color = colors[3];
				hasChosen[3] = true;
			}



			if (!hasChosen[0] && GetButtonDown(0, "B") && !tastoScelto[1])
			{
				choices.shield = 0;
				tastoScelto[1] = true;
				shieldSprite.color = colors[0];
				bodySprite.color = colors[0];
				hasChosen[0] = true;
			}
			
			if (!hasChosen[1] && GetButtonDown(1, "B") && !tastoScelto[1])
			{
				choices.shield = 1;
				tastoScelto[1] = true;
				shieldSprite.color = colors[1];
				bodySprite.color = colors[1];
				hasChosen[1] = true;
			}
			
			if (!hasChosen[2] && GetButtonDown(2, "B") && !tastoScelto[1])
			{
				choices.shield = 2;
				tastoScelto[1] = true;
				shieldSprite.color = colors[2];
				bodySprite.color = colors[2];
				hasChosen[2] = true;
			}
			
			if (!hasChosen[3] && GetButtonDown(3, "B") && !tastoScelto[1])
			{
				choices.shield = 3;
				tastoScelto[1] = true;
				shieldSprite.color = colors[3];
				bodySprite.color = colors[3];
				hasChosen[3] = true;
			}



			if (!hasChosen[0] && GetButtonDown(0, "X") && !tastoScelto[2])
			{
				choices.gun = 0;
				tastoScelto[2] = true;
				gunSprite.color = colors[0];
				hasChosen[0] = true;
			}
			
			if (!hasChosen[1] && GetButtonDown(1, "X") && !tastoScelto[2])
			{
				choices.gun = 1;
				tastoScelto[2] = true;
				gunSprite.color = colors[1];
				hasChosen[1] = true;
			}
			
			if (!hasChosen[2] && GetButtonDown(2, "X") && !tastoScelto[2])
			{
				choices.gun = 2;
				tastoScelto[2] = true;
				gunSprite.color = colors[2];
				hasChosen[2] = true;
			}
			
			if (!hasChosen[3] && GetButtonDown(3, "X") && !tastoScelto[2])
			{
				choices.gun = 3;
				tastoScelto[2] = true;
				gunSprite.color = colors[3];
				hasChosen[3] = true;
			}



			if (!hasChosen[0] && GetButtonDown(0, "Y") && !tastoScelto[3])
			{
				choices.cannon = 0;
				tastoScelto[3] = true;
				cannonSprite.color = colors[0];
				hasChosen[0] = true;
			}
			
			if (!hasChosen[1] && GetButtonDown(1, "Y") && !tastoScelto[3])
			{
				choices.cannon = 1;
				tastoScelto[3] = true;
				cannonSprite.color = colors[1];
				hasChosen[1] = true;
			}
			
			if (!hasChosen[2] && GetButtonDown(2, "Y") && !tastoScelto[3])
			{
				choices.cannon = 2;
				tastoScelto[3] = true;
				cannonSprite.color = colors[2];
				hasChosen[2] = true;
			}
			
			if (!hasChosen[3] && GetButtonDown(3, "Y") && !tastoScelto[3])
			{
				choices.cannon = 3;
				tastoScelto[3] = true;
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
