using UnityEngine;
using System.Collections;

public class QuickTimeButtons : MonoBehaviour {

	public UnityEngine.UI.Image[] player0;
	public UnityEngine.UI.Image[] player1;
	public UnityEngine.UI.Image[] player2;
	public UnityEngine.UI.Image[] player3;

	public void SetPlayerImages(int playerNumber, Sprite[] sprites)
	{
		if (playerNumber == 0)
		{
			if (player0.Length == sprites.Length)
			{
				for (int i = 0; i < player0.Length; i++)
				{
					//Debug.Log ("in");
					player0[i].sprite = sprites[i];
				}
			}
		}

		if (playerNumber == 1)
		{
			if (player1.Length == sprites.Length)
			{
				for (int i = 0; i < player1.Length; i++)
				{
					player1[i].sprite = sprites[i];
				}
			}
		}

		if (playerNumber == 2)
		{
			if (player2.Length == sprites.Length)
			{
				for (int i = 0; i < player2.Length; i++)
				{
					player2[i].sprite = sprites[i];
				}
			}
		}

		if (playerNumber == 3)
		{
			if (player3.Length == sprites.Length)
			{
				for (int i = 0; i < player3.Length; i++)
				{
					player3[i].sprite = sprites[i];
				}
			}
		}
	}

	public void PressCorrectButton(int playerNumber, int spriteNumber, Sprite sprite)
	{
		if (playerNumber == 0)
		{
			player0[spriteNumber].sprite = sprite;
		}
		
		if (playerNumber == 1)
		{
			player1[spriteNumber].sprite = sprite;
		}
		
		if (playerNumber == 2)
		{
			player2[spriteNumber].sprite = sprite;
		}
		
		if (playerNumber == 3)
		{
			player3[spriteNumber].sprite = sprite;
		}
	}

	public void EnableButtonsGraphic(bool enable = true)
	{
		for (int i = 0; i < player0.Length; i++)
		{
			if (player0[i] != null)
				player0[i].enabled = enable;
		}

		for (int i = 0; i < player1.Length; i++)
		{
			if (player1[i] != null)
				player1[i].enabled = enable;
		}

		for (int i = 0; i < player2.Length; i++)
		{
			if (player2[i] != null)
				player2[i].enabled = enable;
		}

		for (int i = 0; i < player3.Length; i++)
		{
			if (player3[i] != null)
				player3[i].enabled = enable;
		}
	}
}
