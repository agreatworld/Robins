using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTreeEvents : MonoBehaviour {
	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			DialogueManager.Instance.PlayNext();
		}
	}
}
