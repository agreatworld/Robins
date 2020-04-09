using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasserSettleGuide : MonoBehaviour {

	private bool clicked = false;

	[HideInInspector]
	public static PasserSettleGuide Instance;

	[HideInInspector]
	public bool getReadyForSettleDown = false;

	private void Awake() {
		Instance = this;
	}

	private void OnMouseUpAsButton() {
		if (!clicked) {
			clicked = true;
			GameGuide.Instance.guideMask.SetActive(false);
			GetComponent<SpriteRenderer>().sortingOrder = 3;
			HighLightAllSubMaps();
			DialogueManager.Instance.UpdateDialogueStatus();
			DialogueController.Instance.ShowDialogue();
			DialogueManager.Instance.PlayNext();
		}
	}

	private void HighLightAllSubMaps() {
		var infoArray = MapUIController.Instance.GetInfoArray();
		foreach(var info in infoArray) {
			info.mouseEvents.HighLightSubMap();
		}
	}

	public void ResetAllSubMaps() {
		var infoArray = MapUIController.Instance.GetInfoArray();
		foreach (var info in infoArray) {
			info.mouseEvents.ResetMaterial();
		}
	}
}
