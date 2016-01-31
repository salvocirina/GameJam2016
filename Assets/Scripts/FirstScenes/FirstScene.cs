using UnityEngine;
using System.Collections;

public class FirstScene : MonoBehaviour {
	
	void Update () {
		if (GetButtonDown(0, "A") || GetButtonDown(1, "A") || GetButtonDown(2, "A") || GetButtonDown(3, "A"))
			ChangeScene();
	}

	void ChangeScene()
	{
		Application.LoadLevel(1);
	}

	bool GetButtonDown(int player, string name) {
		return Rewired.ReInput.players.GetPlayer(player).GetButtonDown(name);
	}

}
