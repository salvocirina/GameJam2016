using UnityEngine;
using System.Collections;

public class Choices : MonoBehaviour {

	public int shield;
	public int cannon;
	public int gun;
	public int movements;

	void Awake () {
		DontDestroyOnLoad(this.gameObject);
	}
}
