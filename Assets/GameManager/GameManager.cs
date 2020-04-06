using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private void Start() {
		GameGuide.Instance.LoadGameGuide("PlotScripts/GameGuide/起始引导.txt");
	}

}
