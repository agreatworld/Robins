using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BagPreviewCardDetailsBackButton : MonoBehaviour, IPointerClickHandler {
	public void OnPointerClick(PointerEventData eventData) {
		if (GameGuide.Instance.isGameGuiding) {
			DialogueController.Instance.ShowDialogue();
			DialogueManager.Instance.UpdateDialogueStatus();
			DialogueManager.Instance.PlayNext();
		}
		BagPreviewCardDetails.Instance.Hide();
	}
}
