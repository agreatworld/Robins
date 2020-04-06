using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcessor : MonoBehaviour {

	public static GameProcessor Instance;

	private void Awake() {
		Instance = this;
	}

	public void RoundStarts() {
		ShowShop();

	}

	private void ShowShop() {

	}


	public void RoundEnds() {

	}

}
