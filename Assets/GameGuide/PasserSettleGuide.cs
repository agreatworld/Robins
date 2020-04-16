using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PasserSettleGuide : MonoBehaviour, IPointerClickHandler {

	private bool clicked = false;

	[HideInInspector]
	public static PasserSettleGuide Instance;

	private void Awake() {
		Instance = this;
	}



	public void OnPointerClick(PointerEventData eventData) {
		if (!clicked) {
			clicked = true;
			AvesSettleManager.Instance.gameObject.GetComponent<Canvas>().sortingOrder = 0;
			GameGuide.Instance.guideMask.SetActive(false);
			DialogueManager.Instance.UpdateDialogueStatus();
			DialogueController.Instance.ShowDialogue();
			DialogueManager.Instance.PlayNext();
			Destroy(this);
		}
	}
}
